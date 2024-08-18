using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.DataTransferObjects.RequestObjects
{
    public class UpdateBuildingConfigurationsDTO : AddBuildingConfigurationsDTO
    {
        public string id { get; set; }
    }
}
