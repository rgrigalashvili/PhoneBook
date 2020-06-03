using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Database;
using Phonebook.DAL.Database.Entities;
using Phonebook.DAL.Repositories.Interfaces;
using Phonebook.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.DAL.Repositories
{
    public class PhoneNumberRepository : BaseRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(PhoneBookDataContext context) : base(context)
        {

        }
        public async Task<bool> Exists(string number, int userID)
        {
            return await dbSet.AnyAsync(x => x.Number == number && x.UserID == userID);
        }
        public async Task<bool> ExistsPhoneNumberForPerson(int personID)
        {
            return await dbSet.AnyAsync(x => x.PersonId == personID);
        }

        [System.Obsolete]
        public async Task<OperationResult> AddPhoneNumberAsync(PhoneNumber model)
        {
            var result = context.Database.ExecuteSqlCommand("exec dbo.AddPhoneNumber @p0, @p1, @p2", model.PersonId, model.UserID, model.Number);

            if (result == 1)
            {
                return new OperationResult(true, null);
            }

            return await Task.FromResult(new OperationResult(false, null));
        }

        public IQueryable<PhoneNumber> GetAllPhoneNumbersForPerson(int personID)
        {
            return context.PhoneNumbers.Where(x => x.PersonId == personID).Select(x => x);
        }

        public async Task<Person> GetByPhoneNumberAsync(string phoneNumber, int userID)
        {
            return await Task.FromResult(context.PhoneNumbers.Where(x => x.Number == phoneNumber && x.UserID == userID).Select(x => x.Person).FirstOrDefault());
        }
    }
}
