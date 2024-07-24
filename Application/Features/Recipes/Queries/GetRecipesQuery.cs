using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Recipes.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Auth.Queries
{

public class GetRecipesQuery : IRequest<ErrorOr<BaseResponse<List<RecipeDto>>>>
{
}

public class GetRecipesQueryHandler: IRequestHandler<GetRecipesQuery, ErrorOr<BaseResponse<List<RecipeDto>>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRecipesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<List<RecipeDto>>>> Handle(
            GetRecipesQuery query,
            CancellationToken cancellationToken
        )    
        {
            var user = await _unitOfWork.RecipeRepository.GetAllAsync(1,10);


            return new BaseResponse<List<RecipeDto>>(){
                Message="Recipe Detail fetched successfully",
                Value=_mapper.Map<List<RecipeDto>>(user)
            };
        }
        }
}
