using Bogus;
using Domain;
using ErrorOr;
using System.Data;
using Application.Common.Errors;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Contracts.Services;
using Application.Contracts.Persistance;
using Application.Features.Auth.Dtos;
using MediatR;


namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;
        private static readonly Faker _faker = new();

        public UserRepository(
            AppDbContext dbContext,
            UserManager<AppUser> userManager,
            IJwtService jwtService
           )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<ErrorOr<AppUser>> Register(RegisterDto registerDto)
        {
            var existing_user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existing_user != null)
                return existing_user;

            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            user = (await _dbContext.Users.AddAsync(user)).Entity;

            if (await _dbContext.SaveChangesAsync() == 0)
                throw new DbAccessException($"Unable to save user to database");

            await _userManager.AddToRoleAsync(user, "User");

            if (await _dbContext.SaveChangesAsync() == 0)
                throw new DbAccessException($"Unable to add role to user in database");

            return user;
        }

        public async Task<List<AppRole>> GetUserRolesAsync(AppUser user)
        {
            return (await _userManager.GetRolesAsync(user)).Select(role => new AppRole { Name = role }).ToList();
        }

        public async Task<ErrorOr<LoginResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return ErrorFactory.NotFound("User", $"User with public address `{loginDto.Email}` not found");
            }

           

            var roles = await _userManager.GetRolesAsync(user);

            _dbContext.Entry(user).State = EntityState.Modified;

            var UpdateResult = await _dbContext.SaveChangesAsync();
            if (UpdateResult == 0)
            {
                throw new DbAccessException($"Unable to update user nonce");
            }

            var tokenInfo = _jwtService.GenerateToken(user, roles);
            return new LoginResponseDto
            {
                Id = user.Id,
                AccessToken = tokenInfo.Item1,
                ExpireInDays = Math.Round(tokenInfo.Item2 / (60 * 24), 2)
            };
        }

        

        public async Task<ErrorOr<AppUser>> GetUserByIdAsync(string Id)
        {
           var user = await _userManager.Users.FirstOrDefaultAsync(x =>x.Id == Id);
           if (user == null) return ErrorFactory.NotFound("Users","User not found");
           return user;
        }

        public async Task<ErrorOr<Unit>> DeleteUserAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x =>x.Email == email);
            if (user == null) return ErrorFactory.NotFound("Users","User not found");

            await _userManager.DeleteAsync(user);
            return Unit.Value;
        }

        public async Task<ErrorOr<Unit>> UpdateUser(UpdateUserDto updateUserDto,string userId){
            var user = await _userManager.Users.FirstOrDefaultAsync(x =>x.Id == userId);
            if (user == null) return ErrorFactory.NotFound("Users","User not found");

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;

            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }

    }
}
