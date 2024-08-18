using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.DataTransferObjects.ResponseObjects
{
    public class BuildingConfigurationsViewModel
    {
        public string id { get; set; }
        public string BuildingType { get; set; }
        public int BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
