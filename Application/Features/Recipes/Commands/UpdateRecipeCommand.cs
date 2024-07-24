using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Recipes.Dtos;
using Application.Common.Errors;

namespace Application.Features.Auth.Commands
{
    public class UpdateRecipeCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public UpdateRecipeDto updateRecipeDto;
        public string userId;
    }

    public class UpdateRecipeCommandHandler
        : IRequestHandler<UpdateRecipeCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRecipeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            UpdateRecipeCommand command,
            CancellationToken cancellationToken
        )
        {
           var recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(command.updateRecipeDto.Id);

            if (recipe == null) return ErrorFactory.NotFound("Recipe","Recipe not found");

            recipe.Title = command.updateRecipeDto.Title;
            recipe.Ingredients = command.updateRecipeDto.Ingredients;
            recipe.Instructions = command.updateRecipeDto.Instructions;
            recipe.PreparationTime = (int) command.updateRecipeDto.PreparationTime;

            _unitOfWork.RecipeRepository.UpdateAsync(recipe);

            await _unitOfWork.SaveAsync();

            return new BaseResponse<Unit>(){
                Message="Recipe Updated successfully",
                Value= Unit.Value
            };
        }
    }
}
