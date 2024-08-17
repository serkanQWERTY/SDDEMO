using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.DataTransferObjects.ResponseObjects
{
    public class UserViewModel
    {
        public Guid id { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime updatedDate { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mailAddress { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
