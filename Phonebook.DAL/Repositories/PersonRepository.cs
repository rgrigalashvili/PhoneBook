using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Database;
using Phonebook.DAL.Database.Entities;
using Phonebook.DAL.Repositories.Interfaces;
using Phonebook.Shared.Models;
using System.Threading.Tasks;

namespace Phonebook.DAL.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(PhoneBookDataContext context) : base(context)
        {
        }
        public async Task<bool> Exists(string privateNumber)
        {
            return await dbSet.AnyAsync(x => x.PrivateNumber == privateNumber);
        }

        [System.Obsolete]
        public async Task<OperationResult> RemovePersonAsync(int personID)
        {
            var result = context.Database.ExecuteSqlCommand("exec dbo.RemovePerson @p0", personID);

            if (result == 1)
            {
                return new OperationResult(true, "Request completed successfully");
            }
            if (result == 99)
            {
                return new OperationResult(true, "PhoneNumber exists so it's not possible to delete person");
            }
            return await Task.FromResult(new OperationResult(false, "Request failed"));
        }
    }
}
