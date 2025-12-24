using BulletinBoard.Data.DTOs;

namespace BulletinBoard.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(UserDto user);
        Task<UserDto> GetByEmailAsync(string email);
        Task<UserDto> GetByIdAsync(int id);
    }
}
