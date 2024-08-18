using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using SDDEMO.Application.Wrappers;
using SDDEMO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Interfaces.Managers
{
    public interface IElasticManager
    {
        Task<BaseApiResponse<bool>> AddBuildingConfigurationAsync(AddBuildingConfigurationsDTO addBuildingConfigurationsDTO);
        Task<BaseApiResponse<IEnumerable<BuildingConfigurationsViewModel>>> GetBuildingConfigurationsAsync();
        Task<BaseApiResponse<bool>> UpdateBuildingConfigurationAsync(UpdateBuildingConfigurationsDTO updateBuildingConfigurationsDTO);
        Task<BaseApiResponse<bool>> DeleteBuildingConfigurationAsync(string id);
    }
}
