using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Wrappers
{
    public class BaseApiResponse<T>
    {
        public readonly IEnumerable<char> ErrorMessage;
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public T? dataToReturn { get; set; }
    }
}
