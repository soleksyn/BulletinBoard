using BulletinBoard.Web.Models;
using BulletinBoard.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.Cookie.Name = "BulletinBoard.Auth";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        // This ensures the cookie is not persisted after the browser is closed
        options.SlidingExpiration = true;
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<JwtAuthorizationHandler>();

// Register ApiSettings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register HttpClient and ApiServices
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpClient<IApiAnnouncementService, ApiAnnouncementService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl!);
    }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    }).AddHttpMessageHandler<JwtAuthorizationHandler>();

    builder.Services.AddHttpClient<IApiCategoryService, ApiCategoryService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl!);
    }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    }).AddHttpMessageHandler<JwtAuthorizationHandler>();

    builder.Services.AddHttpClient<IApiAccountService, ApiAccountService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl!);
    }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });
}
else
{
    builder.Services.AddHttpClient<IApiAnnouncementService, ApiAnnouncementService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl!);
    }).AddHttpMessageHandler<JwtAuthorizationHandler>();

    builder.Services.AddHttpClient<IApiCategoryService, ApiCategoryService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl!);
    }).AddHttpMessageHandler<JwtAuthorizationHandler>();

    builder.Services.AddHttpClient<IApiAccountService, ApiAccountService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl!);
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Announcements}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
