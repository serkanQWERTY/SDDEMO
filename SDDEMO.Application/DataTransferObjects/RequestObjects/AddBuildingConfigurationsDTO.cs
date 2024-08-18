﻿using SDDEMO.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.DataTransferObjects.RequestObjects
{
    public class AddBuildingConfigurationsDTO
    {
        public BuildingType BuildingType { get; set; }
        public int BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
