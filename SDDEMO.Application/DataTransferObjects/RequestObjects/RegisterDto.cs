using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.DataTransferObjects.RequestObjects
{
    public class RegisterDto
    {
        public string username { get; set; }
        public string mailAddress { get; set; }
        public string password { get; set; }
    }
}
