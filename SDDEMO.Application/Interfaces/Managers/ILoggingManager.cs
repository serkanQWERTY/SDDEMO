using SDDEMO.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Interfaces.Managers
{
    public interface ILoggingManager
    {
        void InfoLog(string message, bool isLogin = false, string usernameLogin = "");
        void ErrorLog(string message);
    }
}
