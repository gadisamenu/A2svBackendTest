using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Contracts.Persistance;
using Domain.Recipes;
using Application.Features.Recipes.Dtos;

namespace Application.Features.Auth.Commands
{
    public class CreateRecipeCommand : IRequest<ErrorOr<BaseResponse<RecipeDto>>>
    {
        public string UserId;
        public CreateRecipeDto createRecipeDto;
    }

    public class CreateRecipeCommandHandler
        : IRequestHandler<CreateRecipeCommand, ErrorOr<BaseResponse<RecipeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRecipeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<RecipeDto>>> Handle(
            CreateRecipeCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(command.UserId);

            var recipe = new Recipe {
              Title = command.createRecipeDto.Title,
                Instructions = command.createRecipeDto.Instructions,
                Ingredients = command.createRecipeDto.Ingredients,
                PreparationTime = (int) command.createRecipeDto.PreparationTime,
                Owner = user.Value
            };

            var recipeRes = await _unitOfWork.RecipeRepository.AddAsync(recipe);

            await _unitOfWork.SaveAsync();

            return new BaseResponse<RecipeDto>(){
                Message="Recipe created successfully",
                Value=_mapper.Map<RecipeDto>(recipeRes)
            };
        }
    }
}
