using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Domain.Entity
{
    public class User : BaseEntity
    {
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mailAddress { get; set; }
        public string password { get; set; }
    }
}
