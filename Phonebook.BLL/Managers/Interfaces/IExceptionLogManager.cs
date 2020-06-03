using Microsoft.AspNetCore.Http;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.BLL.Managers.Interfaces
{
    public interface IExceptionLogManager
    {
        Task<OperationResult> LogException(HttpContext httpContext);
    }
}
