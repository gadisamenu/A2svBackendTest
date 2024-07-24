using Application.Contracts;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediatr;

        protected readonly IUserAccessor _userAccessor;

        public BaseController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        protected IMediator Mediator => _mediatr ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HandleResult<T>(ErrorOr<T> result, string message = "Operation successful")
        {
            return result.Match( value => Ok(value), Problem);
        }
        
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0) return Problem();

            return errors.All(error => error.Type == ErrorType.Validation)
                ? GetValidationProblem(errors)
                : GetProblem(errors[0]);
        }

        private IActionResult GetProblem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Failure => StatusCodes.Status417ExpectationFailed,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        private IActionResult GetValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors) modelStateDictionary.AddModelError(error.Code, error.Description);

            return ValidationProblem(modelStateDictionary);

        }
    }
}