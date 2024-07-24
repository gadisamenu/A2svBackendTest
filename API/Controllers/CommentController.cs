using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Commands;
using Application.Features.Auth.Dtos;
using Application.Contracts;
using Application.Features.Auth.Queries;
using Application.Features.Comments.Dtos;


namespace API.Controllers
{
    public class Comment : BaseController
    {
        public Comment(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

       

        [HttpGet]
        public async Task<IActionResult> GetAll(long id)
        {
            return HandleResult(
                await Mediator.Send(
                    new GetCommentsQuery()
                    {
                    }
                )
            );
        }


        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new CreateCommentCommand()
                    {
                        UserId = _userAccessor.GetUserId(),
                        createCommentDto = createCommentDto
                    }
                )
            );
        }

          
    }
}