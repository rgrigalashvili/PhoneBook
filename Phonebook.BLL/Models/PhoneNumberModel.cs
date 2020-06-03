using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.BLL.Models
{
    public class PhoneNumberModel : BaseModel
    {
        public int PersonId { get; set; }
        public PersonModel Person { get; set; }
        public string Number { get; set; }
        public int UserID { get; set; }
    }
}
