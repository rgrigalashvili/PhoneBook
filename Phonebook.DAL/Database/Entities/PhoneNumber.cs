using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Phonebook.DAL.Database.Entities
{
    public class PhoneNumber : EntityBase
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Number { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
