using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Phonebook.DAL.Database.Entities
{
    public class Person : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
