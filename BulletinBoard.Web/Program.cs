using BulletinBoard.Web.Models;
using BulletinBoard.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register ApiSettings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register HttpClient and ApiServices
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpClient<IApiAnnouncementService, ApiAnnouncementService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl);
    }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });

    builder.Services.AddHttpClient<IApiCategoryService, ApiCategoryService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl);
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
        client.BaseAddress = new Uri(baseUrl);
    });
    builder.Services.AddHttpClient<IApiCategoryService, ApiCategoryService>(client =>
    {
        var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");
        client.BaseAddress = new Uri(baseUrl);
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Announcements}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
