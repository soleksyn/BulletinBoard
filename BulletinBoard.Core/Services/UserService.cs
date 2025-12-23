using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net;
using System.Security.Claims;
using System.Text;
using BulletinBoard.Core.Constants;
using BulletinBoard.Core.Enums;
using BulletinBoard.Core.Interfaces;
using BulletinBoard.Core.Models;
using BulletinBoard.Data.DTOs;
using BulletinBoard.Data.Interfaces;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace BulletinBoard.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> RegisterAsync(RegisterModel model)
        {
            var existingUser = await _userRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
            {
                throw new Exception(ErrorConstants.UserAlreadyExists);
            }

            var userDto = new UserDto
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = BC.HashPassword(model.Password),
                RoleId = (int)UserRole.User,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            var id = await _userRepository.CreateAsync(userDto);
            return await GetByIdAsync(id);
        }

        public async Task<UserModel> LoginAsync(LoginModel model)
        {
            var userDto = await _userRepository.GetByEmailAsync(model.Email);
            if (userDto == null || !BC.Verify(model.Password, userDto.PasswordHash))
            {
                throw new Exception(ErrorConstants.InvalidEmailOrPassword);
            }

            return MapToModel(userDto);
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var userDto = await _userRepository.GetByIdAsync(id);
            return userDto != null ? MapToModel(userDto) : null;
        }

        public string GenerateJwtToken(UserModel user, string secretKey, string issuer, string audience)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel MapToModel(UserDto dto)
        {
            return new UserModel
            {
                Id = dto.Id,
                Username = dto.Username,
                Email = dto.Email,
                Role = (UserRole)dto.RoleId,
                CreatedDate = dto.CreatedDate,
                IsActive = dto.IsActive
            };
        }
    }
}
