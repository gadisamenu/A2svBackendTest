using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Features.Comments.Dtos;
using AutoMapper;
using ErrorOr;
using MediatR;

namespace Application.Features.Auth.Queries
{

public class GetCommentsQuery : IRequest<ErrorOr<BaseResponse<CommentDto>>>
{
}

public class GetCommentsQueryHandler: IRequestHandler<GetCommentsQuery, ErrorOr<BaseResponse<CommentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCommentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<CommentDto>>> Handle(
            GetCommentsQuery query,
            CancellationToken cancellationToken
        )    
        {
            var Comment = await _unitOfWork.CommentRepository.GetAllAsync();

        
            return new BaseResponse<CommentDto>(){
                Message="Comments  fetched successfully",
                Value=_mapper.Map<CommentDto>(Comment)
            };
        }
        }
}
