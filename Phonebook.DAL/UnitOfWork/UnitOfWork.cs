using Phonebook.DAL.Database;
using Phonebook.DAL.Repositories;
using Phonebook.DAL.Repositories.Interfaces;
using Phonebook.DAL.UnitOfWork.Interface;
using Phonebook.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private PhoneBookDataContext context;
        private bool disposed = false;

        private IPersonRepository personRepository;
        private IUserRepository userRepository;
        private IPhoneNumberRepository phoneNumberRepository;
        public UnitOfWork(PhoneBookDataContext context)
        {
            this.context = context;
        }
        public IPersonRepository PersonRepository
        {
            get { return personRepository = personRepository ?? new PersonRepository(context); }
        }
        public IUserRepository UserRepository
        {
            get { return userRepository = userRepository ?? new UserRepository(context); }
        }
        public IPhoneNumberRepository PhoneNumberRepository
        {
            get { return phoneNumberRepository = phoneNumberRepository ?? new PhoneNumberRepository(context); }
        }
        public void Complete()
        {
            context.SaveChanges();
        }
        public async Task<OperationResult> CompleteAsync()
        {
            await context.SaveChangesAsync();
            return new OperationResult(true, "Request completed successfully");
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
