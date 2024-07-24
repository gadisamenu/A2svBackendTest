using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Auth.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Auth.Queries
{

public class GetUserDetailQuery : IRequest<ErrorOr<BaseResponse<UserDetailDto>>>
{
    public string Id {get; set;}
}

public class GetUserDetailQueryHandler: IRequestHandler<GetUserDetailQuery, ErrorOr<BaseResponse<UserDetailDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<UserDetailDto>>> Handle(
            GetUserDetailQuery query,
            CancellationToken cancellationToken
        )    
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(query.Id);

            if (user.IsError) return user.Errors;

            return new BaseResponse<UserDetailDto>(){
                Message="User Detail fetched successfully",
                Value=_mapper.Map<UserDetailDto>(user.Value)
            };
        }
        }
}
