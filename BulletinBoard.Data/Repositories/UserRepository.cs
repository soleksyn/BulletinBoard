using BulletinBoard.Data.DTOs;
using BulletinBoard.Data.Interfaces;

namespace BulletinBoard.Data.Repositories
{
    public class UserRepository : DapperRepositoryBase, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<int> CreateAsync(UserDto user)
        {
            var parameters = new
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                RoleId = user.RoleId
            };

            return await GetObjectAsync<int>("[dbo].[sp_CreateUser]", parameters);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var parameters = new { Email = email };
            return await GetObjectAsync<UserDto>("[dbo].[sp_GetUserByEmail]", parameters);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var parameters = new { Id = id };
            return await GetObjectAsync<UserDto>("[dbo].[sp_GetUserById]", parameters);
        }
    }
}
