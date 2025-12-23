using System.Net.Http.Json;
using BulletinBoard.Core.Constants;
using BulletinBoard.Core.Models;

namespace BulletinBoard.Web.Services
{
    public class ApiAccountService : IApiAccountService
    {
        private readonly HttpClient _httpClient;

        public ApiAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(string Token, UserModel User)> LoginAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return (result.Token, result.User);
            }

            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? ErrorConstants.LoginFailed);
        }

        public async Task<UserModel> RegisterAsync(RegisterModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/register", model);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserModel>();
            }

            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            throw new Exception(error?.Message ?? ErrorConstants.RegistrationFailed);
        }

        private class LoginResponse
        {
            public string Token { get; set; }
            public UserModel User { get; set; }
        }

        private class ErrorResponse
        {
            public string Message { get; set; }
        }
    }
}
