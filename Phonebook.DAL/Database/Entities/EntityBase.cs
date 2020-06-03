using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Phonebook.DAL.Database.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DateCreated
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }
        private DateTime? dateCreated = null;
        public DateTime? DateChanged { get; set; }
        public DateTime? DateDelated { get; set; }
    }
}
