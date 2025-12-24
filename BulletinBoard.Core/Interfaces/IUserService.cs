using BulletinBoard.Core.Models;

namespace BulletinBoard.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> RegisterAsync(RegisterModel model);
        Task<UserModel> LoginAsync(LoginModel model);
        Task<UserModel> GetByIdAsync(int id);
        string GenerateJwtToken(UserModel user, string secretKey, string issuer, string audience);
    }
}
