using AutoMapper;
using Phonebook.BLL.Managers.Interfaces;
using Phonebook.BLL.Models;
using Phonebook.DAL.Database.Entities;
using Phonebook.DAL.UnitOfWork.Interface;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.BLL.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public PersonManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationResult> AddPersonAsync(PersonModel model)
        {
            var userDTO = mapper.Map<PersonModel, Person>(model);

            if (await unitOfWork.PersonRepository.Exists(model.PrivateNumber))
            {
                return new OperationResult(false, "Person exists!");
            }
            await unitOfWork.PersonRepository.AddAsync(userDTO);
            return await unitOfWork.CompleteAsync();
        }

        public async Task<OperationResult> DeletePersonAsync(int personID)
        {
            if (await unitOfWork.PhoneNumberRepository.ExistsPhoneNumberForPerson(personID))
            {
                return new OperationResult(false, "Delete is impossible because phoneNumber exists!");
            }
            await unitOfWork.PersonRepository.RemoveAsync(personID);
            return await unitOfWork.CompleteAsync();
        }
        public async Task<OperationResult> AddPhoneAsync(PhoneNumberModel model)
        {
            var userDTO = mapper.Map<PhoneNumberModel, PhoneNumber>(model);

            if (await unitOfWork.PhoneNumberRepository.Exists(model.Number, model.UserID))
            {
                return new OperationResult(false, "PhoneNumber exists!");
            }
            await unitOfWork.PhoneNumberRepository.AddPhoneNumberAsync(userDTO);
            return await unitOfWork.CompleteAsync();
        }

        public async Task<List<PhoneNumberModel>> GetAllPhoneNumbersForPerson(int personID, int userID)
        {
            var phoneNumber = await Task.FromResult(unitOfWork.PhoneNumberRepository.GetAllPhoneNumbersForPerson(personID).Where(x => x.UserID == userID));
            return mapper.Map<List<PhoneNumber>, List<PhoneNumberModel>>(phoneNumber.ToList());
        }

        public async Task<OperationResult> DeletePhoneAsync(int personID)
        {
            await unitOfWork.PhoneNumberRepository.RemoveAsync(personID);
            return await unitOfWork.CompleteAsync();
        }

        public async Task<PersonModel> IdentifyPersonByPhone(string phoneNumber, int userID)
        {
            var person = await unitOfWork.PhoneNumberRepository.GetByPhoneNumberAsync(phoneNumber, userID);

            return mapper.Map<Person, PersonModel>(person);
        }
    }
}
