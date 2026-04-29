using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nhs.Test.Api.Filters
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log the exception or perform additional actions

            context.Result = new ObjectResult($"Global error: {context.Exception.Message}")
            {
                StatusCode = 500,
            };
            context.ExceptionHandled = true;
        }




    }
}
