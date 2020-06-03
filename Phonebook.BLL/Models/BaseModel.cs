using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.BLL.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime? DateCreated
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }
        private DateTime? dateCreated = null;
        public DateTime? DateChanged { get; set; }
        public DateTime? DateDelated { get; set; }
    }
}
