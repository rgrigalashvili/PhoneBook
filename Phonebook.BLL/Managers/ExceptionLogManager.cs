using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Phonebook.BLL.Managers.Interfaces;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.BLL.Managers
{
    public class ExceptionLogManager : IExceptionLogManager
    {
        private readonly ILogger<ExceptionLogManager> logger;
        public ExceptionLogManager(ILogger<ExceptionLogManager> logger)
        {
            this.logger = logger;
        }
        public async Task<OperationResult> LogException(HttpContext httpContext)
        {
            var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                var errorMessage = exceptionHandlerPathFeature.Error.Message;
                var stackTrace = exceptionHandlerPathFeature.Error.StackTrace;

                logger.LogError($"ErrorMessage:{ errorMessage}, StackTrace:{ stackTrace}");
            }
            return await Task.FromResult(new OperationResult(false, "Unexpected error occurred"));
        }
    }
}
