using Microsoft.AspNetCore.Http;
using Phonebook.BLL.Managers.Interfaces;
using System.Threading.Tasks;

namespace Phonebook.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate request;

        public ExceptionHandlerMiddleware(RequestDelegate request)
        {
            this.request = request;
        }
        public async Task InvokeAsync(HttpContext context, IExceptionLogManager exceptionLogManager)
        {
            await exceptionLogManager.LogException(context);

            await request(context);
        }
    }
}
