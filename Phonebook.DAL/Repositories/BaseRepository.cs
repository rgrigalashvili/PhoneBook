using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Database;
using Phonebook.DAL.Database.Entities;
using Phonebook.DAL.Repositories.Interfaces;
using Phonebook.Shared.Models;
using System.Threading.Tasks;

namespace Phonebook.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
    {
        internal PhoneBookDataContext context;
        internal DbSet<TEntity> dbSet;
        public BaseRepository(PhoneBookDataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual async Task<OperationResult> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return new OperationResult(true, null);
        }
        public virtual async Task<OperationResult> RemoveAsync(int Id)
        {
            var entity = await dbSet.FindAsync(Id);

            dbSet.Remove(entity);

            return new OperationResult(true, "Removed");
        }
    }
}
