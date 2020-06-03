using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.BLL.Models
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
