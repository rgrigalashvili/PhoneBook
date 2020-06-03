using Phonebook.BLL.Models;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.BLL.Managers.Interfaces
{
    public interface IPersonManager
    {
        Task<OperationResult> AddPersonAsync(PersonModel model);
        Task<OperationResult> DeletePersonAsync(int personID);
        Task<OperationResult> AddPhoneAsync(PhoneNumberModel model);
        Task<List<PhoneNumberModel>> GetAllPhoneNumbersForPerson(int personID, int userID);
        Task<OperationResult> DeletePhoneAsync(int personID);
        Task<PersonModel> IdentifyPersonByPhone(string phoneNumber, int userID);
    }
}
