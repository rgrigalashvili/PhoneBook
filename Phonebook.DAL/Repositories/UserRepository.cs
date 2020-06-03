using Microsoft.EntityFrameworkCore;
using Phonebook.DAL.Database;
using Phonebook.DAL.Database.Entities;
using Phonebook.DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Phonebook.DAL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(PhoneBookDataContext context) : base(context)
        {

        }

        public virtual async Task<bool> Exists(string userName)
        {
            return await dbSet.AnyAsync(x => x.UserName == userName);
        }
        public async Task<User> GetUserInfo(string userName)
        {
            return await dbSet.SingleOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
