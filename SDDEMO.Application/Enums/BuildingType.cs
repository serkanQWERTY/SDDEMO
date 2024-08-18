using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Enums
{
    public enum BuildingType
    {
        [Description("Çiftlik")]
        Farm = 0,

        [Description("Akademi")]
        Academy = 1,

        [Description("Merkez")]
        Headquarters = 2,

        [Description("Ağaç İşleme Tesisi")]
        LumberMill = 3,

        [Description("Kışla")]
        Barracks = 4
    }
}
