using Nest;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Extensions;
using SDDEMO.Application.Interfaces.Managers;
using SDDEMO.Application.Wrappers;
using SDDEMO.Domain;
using SDDEMO.Manager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Manager.Managers
{
    public class ElasticManager : IElasticManager
    {
        private readonly IElasticClient elasticClient;
        private ILoggingManager logger;

        public ElasticManager(IElasticClient elasticClient, ILoggingManager logger)
        {
            this.elasticClient = elasticClient;
            this.logger = logger;
        }


        /// <summary>
        /// Konfigürasyon Verilerini Ekleme Metodu.
        /// </summary>
        /// <param name="addBuildingConfigurationsDTO"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<bool>> AddBuildingConfigurationAsync(AddBuildingConfigurationsDTO addBuildingConfigurationsDTO)
        {
            var response = await elasticClient.IndexDocumentAsync(addBuildingConfigurationsDTO);

            if (response.IsValid)
            {
                logger.InfoLog($"Yeni konfigürasyon eklendi: {addBuildingConfigurationsDTO.BuildingType}", false);
                return ApiHelper<bool>.GenerateApiResponse(true, true, ResponseMessages.SuccessfullyDone.ToDescriptionString());
            }
            else
            {
                logger.InfoLog($"Konfigürasyon eklenirken hata oluştu: {response.OriginalException.Message}", false);
                return ApiHelper<bool>.GenerateApiResponse(false, false, ResponseMessages.AnErrorOccured.ToDescriptionString());
            }
        }


        /// <summary>
        /// Konfigürasyon Verilerini Getirme Metodu.
        /// </summary>
        /// <returns></returns>
        public async Task<BaseApiResponse<IEnumerable<BuildingConfigurationsViewModel>>> GetBuildingConfigurationsAsync()
        {
            var searchResponse = await elasticClient.SearchAsync<BuildingConfigurationsViewModel>(s => s
                .Index("building-configurations")
                .Query(q => q
                    .MatchAll()
                )
            );

            var buildings = searchResponse.Hits.Select(hit => new BuildingConfigurationsViewModel
            {
                id = hit.Id,
                BuildingType = hit.Source.BuildingType,
                BuildingCost = hit.Source.BuildingCost,
                ConstructionTime = hit.Source.ConstructionTime
            });

            logger.InfoLog("Konfigürasyonlar başarıyla getirildi.", false);
            return ApiHelper<IEnumerable<BuildingConfigurationsViewModel>>.GenerateApiResponse(true, buildings, ResponseMessages.SuccessfullyDone.ToDescriptionString());
        }


        /// <summary>
        /// Konfigürasyon Verilerini Güncelleme Metodu.
        /// </summary>
        /// <param name="updateBuildingConfigurationsDTO"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<bool>> UpdateBuildingConfigurationAsync(UpdateBuildingConfigurationsDTO updateBuildingConfigurationsDTO)
        {
            var response = await elasticClient.UpdateAsync<UpdateBuildingConfigurationsDTO>(updateBuildingConfigurationsDTO.id, u => u
                .Index("building-configurations")
                .Doc(updateBuildingConfigurationsDTO)
            );

            if (response.IsValid)
            {
                logger.InfoLog($"Konfigürasyon başarıyla güncellendi: {updateBuildingConfigurationsDTO.id}", false);
                return ApiHelper<bool>.GenerateApiResponse(true, true, ResponseMessages.SuccessfullyUpdated.ToDescriptionString());
            }
            else
            {
                logger.InfoLog($"Konfigürasyon güncellenirken hata oluştu: {response.OriginalException.Message}", false);
                return ApiHelper<bool>.GenerateApiResponse(false, false, ResponseMessages.AnErrorOccured.ToDescriptionString());
            }
        }


        /// <summary>
        /// Konfigürasyon Verilerini Silme Metodu.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseApiResponse<bool>> DeleteBuildingConfigurationAsync(string id)
        {
            var response = await elasticClient.DeleteAsync<BuildingConfiguration>(id, d => d
                .Index("building-configurations")
            );

            if (response.IsValid)
            {
                logger.InfoLog($"Konfigürasyon başarıyla silindi: {id}", false);
                return ApiHelper<bool>.GenerateApiResponse(true, true, ResponseMessages.SuccessfullyDeleted.ToDescriptionString());
            }
            else
            {
                logger.InfoLog($"Konfigürasyon silinirken hata oluştu: {response.OriginalException.Message}", false);
                return ApiHelper<bool>.GenerateApiResponse(false, false, ResponseMessages.AnErrorOccured.ToDescriptionString());
            }
        }
    }
}
