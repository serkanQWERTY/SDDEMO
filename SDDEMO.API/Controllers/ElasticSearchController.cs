using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;
using SDDEMO.Application.Interfaces.Managers;
using SDDEMO.Domain;
using SDDEMO.API.Validators;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.API.Utils;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace SDDEMO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticSearchController : ControllerBase
    {
        private readonly IElasticManager elasticManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="elasticManager"></param>
        public ElasticSearchController(IElasticManager elasticManager)
        {
            this.elasticManager = elasticManager;
        }


        /// <summary>
        /// AddBuildingConfigurationAsync Operation.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPost("AddBuildingConfigurationAsync")]
        [Authorize]
        public async Task<IActionResult> AddBuildingConfiguration([FromBody] AddBuildingConfigurationsDTO addBuildingConfigurationsDTO)
        {
            var validationResult = new BuildingConfigurationValidator().Validate(addBuildingConfigurationsDTO);

            if (!validationResult.IsValid)
                return BasicResponse.GetValidationErrorResponse(validationResult);

            var result = await elasticManager.AddBuildingConfigurationAsync(addBuildingConfigurationsDTO);

            return ApiResponseProvider<bool>.CreateResult(result);
        }


        /// <summary>
        /// Gets all of Building Types Objects.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet("GetAllBuildingTypes")]
        [Authorize]
        public IActionResult GetAllBuildingTypes()
        {
            return ApiResponseProvider<List<EnumViewModel>>.CreateResult(elasticManager.GetAllBuildingTypes());
        }


        /// <summary>
        /// GetBuildingConfigurationsAsync Operation.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBuildingConfigurationsAsync")]
        [Authorize]
        public async Task<IActionResult> GetBuildingConfigurations()
        {
            var result = await elasticManager.GetBuildingConfigurationsAsync();

            return ApiResponseProvider<IEnumerable<BuildingConfigurationsViewModel>>.CreateResult(result);
        }


        /// <summary>
        /// UpdateBuildingConfiguration Operation.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        [HttpPut("UpdateBuildingConfigurationAsync")]
        [Authorize]
        public async Task<IActionResult> UpdateBuildingConfiguration([FromBody] UpdateBuildingConfigurationsDTO updateBuildingConfigurationsDTO)
        {
            var validationResult = new BuildingConfigurationValidator().Validate(updateBuildingConfigurationsDTO);

            if (!validationResult.IsValid)
                return BasicResponse.GetValidationErrorResponse(validationResult);

            var result = await elasticManager.UpdateBuildingConfigurationAsync(updateBuildingConfigurationsDTO);

            return ApiResponseProvider<bool>.CreateResult(result);
        }


        /// <summary>
        /// DeleteBuildingConfiguration Operation.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBuildingConfigurationAsync")]
        [Authorize]
        public async Task<IActionResult> DeleteBuildingConfiguration(string id)
        {
            var result = await elasticManager.DeleteBuildingConfigurationAsync(id);

            return ApiResponseProvider<bool>.CreateResult(result);
        }
    }
}
