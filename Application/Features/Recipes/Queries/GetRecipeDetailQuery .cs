using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Recipes.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Auth.Queries
{

public class GetRecipeDetailQuery : IRequest<ErrorOr<BaseResponse<RecipeDetailDto>>>
{
    public long Id {get; set;}
}

public class GetRecipeDetailQueryHandler: IRequestHandler<GetRecipeDetailQuery, ErrorOr<BaseResponse<RecipeDetailDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRecipeDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<RecipeDetailDto>>> Handle(
            GetRecipeDetailQuery query,
            CancellationToken cancellationToken
        )    
        {
            var recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(query.Id);

            

            return new BaseResponse<RecipeDetailDto>(){
                Message="User Detail fetched successfully",
                Value=_mapper.Map<RecipeDetailDto>(recipe)
            };
        }
        }
}
