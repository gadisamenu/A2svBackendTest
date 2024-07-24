using ErrorOr;
using MediatR;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Common.Errors;

namespace Application.Features.Auth.Commands
{
    public class DeleteRecipeCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public long Id { get; set; }
    }


    public class DeleteRecipeCommandHandler
        : IRequestHandler<DeleteRecipeCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRecipeCommandHandler(
            
            IUnitOfWork unitOfWork
            )
        {
  
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            DeleteRecipeCommand command,
            CancellationToken cancellationToken
        )
        {
            var recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(command.Id);
            if (recipe == null) return ErrorFactory.NotFound("Recipe","Recipe not found");

            _unitOfWork.RecipeRepository.DeleteAsync(recipe);

            await _unitOfWork.SaveAsync();

            return new BaseResponse<Unit>(){
                Message="Recipe Deletedsuccessfully",
                Value= Unit.Value
            };

        }
    }
}
