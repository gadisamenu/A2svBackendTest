using Application.Features.Auth.Dtos;
using Domain;
using ErrorOr;
using MediatR;

namespace Application.Contracts.Persistance;

public interface IUserRepository
{
	Task<ErrorOr<LoginResponseDto>> Login(LoginDto loginDto);
	Task<ErrorOr<AppUser>> Register(RegisterDto registerDto);
	Task<ErrorOr<AppUser>> GetUserByIdAsync(string Id);
	Task<ErrorOr<Unit>> DeleteUserAsync(string email);
	Task<ErrorOr<Unit>> UpdateUser(UpdateUserDto updateUserDto,string userId);
}