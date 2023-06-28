using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web.Http.Filters;

namespace FoodDelivery.ExceptionMiddleware
{

    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Exception exception = context.Exception;

            if (exception is ArgumentNullException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, "Error input data");
            }
            else if (exception is UnauthorizedAccessException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized, "User not Authorized");
            }
            else if (exception is DbUpdateException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.NotFound, "Error when accessing the database");
            }
            else
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, "internal server error");
            }
        }

    }
}
