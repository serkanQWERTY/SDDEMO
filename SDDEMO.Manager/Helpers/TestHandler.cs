using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Manager.Helpers
{
    public class TestHandler
    {
        public List<string> names { get; } = new List<string>();

        public void AddNewName(string name)
        {
            names.Add(name);
        }
    }
}
