using E_Commerce.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public static ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.data);

            return ToProblem(result.Errors);
        }

        public static ActionResult<T> ToActionResult<T>(Result result)
        {
            if (result.IsSuccess)
                return new OkResult();

            return ToProblem(result.Errors);
        }


        public static ObjectResult ToProblem(IReadOnlyList<Error> errors)
        {
            var first = errors[0];

            var statusCode = first.type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.UnAuthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = first.code,
                Detail = first.Description,
                Extensions = { ["errors"] = errors}
            };

            return new ObjectResult(problemDetails) { StatusCode = statusCode };
        }

    }
}
