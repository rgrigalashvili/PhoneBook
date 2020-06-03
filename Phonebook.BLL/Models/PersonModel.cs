using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.BLL.Models
{
    public class PersonModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int UserID { get; set; }
        public UserModel User { get; set; }
    }
}
