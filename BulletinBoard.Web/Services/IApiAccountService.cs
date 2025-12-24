using BulletinBoard.Core.Models;

namespace BulletinBoard.Web.Services
{
    public interface IApiAccountService
    {
        Task<(string Token, UserModel User)> LoginAsync(LoginModel model);
        Task<UserModel> RegisterAsync(RegisterModel model);
    }
}
