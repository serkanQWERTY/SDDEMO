using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SDDEMO.Application.Enums;
using SDDEMO.Infrastructure.Helpers;
using SDDEMO.Manager.Helpers;

namespace SDDEMO.API.Utils
{
    public static class BasicResponse
    {
        public static BadRequestObjectResult GetValidationErrorResponse(ValidationResult validationResult)
        {
            var errorMessage = StringHelper.GetStringFromArray(
                validationResult.Errors.Select(a => a.ErrorMessage).ToList());
            var result = ApiHelper<bool>.GenerateApiResponse(
                false, false, errorMessage);

            return new BadRequestObjectResult(result);
        }

        public static BadRequestObjectResult DataAlreadyExistsResponse()
        {
            var errorMessage = "";
            var result = ApiHelper<bool>.GenerateApiResponse(
                false, false, errorMessage);

            return new BadRequestObjectResult(result);
        }
    }
}
