using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Commands;
using Application.Features.Auth.Dtos;
using Application.Contracts;
using Application.Features.Auth.Queries;
using Application.Features.Recipes.Dtos;


namespace API.Controllers
{
    public class Recipe : BaseController
    {
        public Recipe(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpGet("${id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return HandleResult(
                await Mediator.Send(
                    new GetRecipeDetailQuery()
                    {
                        Id = id
                    }
                )
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long id)
        {
            return HandleResult(
                await Mediator.Send(
                    new GetRecipesQuery()
                    {
                    }
                )
            );
        }


        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDto createRecipeDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new CreateRecipeCommand()
                    {
                        UserId = _userAccessor.GetUserId(),
                        createRecipeDto = createRecipeDto
                    }
                )
            );
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRecipeDto updateRecipeDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new UpdateRecipeCommand()
                    {
                        updateRecipeDto =  updateRecipeDto
                    }
                )
            );
        }

          
    }
}