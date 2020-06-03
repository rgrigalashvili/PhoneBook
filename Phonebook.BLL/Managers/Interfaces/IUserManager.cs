using Phonebook.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phonebook.Shared.Models;

namespace Phonebook.BLL.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<OperationResult> AddUserAsync(UserModel model);
        Task<string> Authenticate(LoginModel model);
    }
}
