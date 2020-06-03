using Phonebook.DAL.Database.Entities;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.DAL.Repositories.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<bool> Exists(string privateNumber);
        Task<OperationResult> RemovePersonAsync(int Id);
    }
}
