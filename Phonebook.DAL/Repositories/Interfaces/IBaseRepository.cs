using Phonebook.DAL.Database.Entities;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : EntityBase
    {
        Task<OperationResult> AddAsync(TEntity entity);
        Task<OperationResult> RemoveAsync(int Id);
    }
}
