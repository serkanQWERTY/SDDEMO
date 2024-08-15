using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Enums
{
    public enum ValidationMessages
    {
        [Description("{fieldName} alanı zorunludur.")]
        FieldIsRequired,
    }
}
