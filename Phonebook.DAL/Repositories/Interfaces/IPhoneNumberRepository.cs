using Phonebook.DAL.Database.Entities;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.DAL.Repositories.Interfaces
{
    public interface IPhoneNumberRepository : IBaseRepository<PhoneNumber>
    {
        Task<bool> Exists(string number, int userID);
        Task<bool> ExistsPhoneNumberForPerson(int personID);
        IQueryable<PhoneNumber> GetAllPhoneNumbersForPerson(int personID);
        Task<OperationResult> AddPhoneNumberAsync(PhoneNumber entity);
        Task<Person> GetByPhoneNumberAsync(string phoneNumber, int userID);
    }
}
