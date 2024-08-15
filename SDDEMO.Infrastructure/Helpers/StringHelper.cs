using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Infrastructure.Helpers
{
    public static class StringHelper
    {
        public static string GetStringFromArray(List<string> array)
        {
            string explanation = String.Join(" ", array.ToArray());
            return explanation;
        }
    }
}
