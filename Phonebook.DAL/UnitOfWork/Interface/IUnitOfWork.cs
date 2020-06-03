using Phonebook.DAL.Repositories.Interfaces;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.DAL.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        IUserRepository UserRepository { get; }
        IPhoneNumberRepository PhoneNumberRepository { get; }
        Task<OperationResult> CompleteAsync();
        void Complete();
        void Dispose();
    }
}
