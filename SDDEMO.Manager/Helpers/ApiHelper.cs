using SDDEMO.Application.Enums;
using SDDEMO.Application.Extensions;
using SDDEMO.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Manager.Helpers
{
    public class ApiHelper<T>
    {
        public static BaseApiResponse<T> GenerateApiResponse(bool isSuccess, T data, string explanation = "")
        {
            string message;
            if (isSuccess)
                message = String.IsNullOrWhiteSpace(explanation) ? ResponseMessages.SuccessfullyDone.ToDescriptionString() : explanation;
            else
                message = String.IsNullOrWhiteSpace(explanation) ? ResponseMessages.AnErrorOccured.ToDescriptionString() : explanation;

            return new BaseApiResponse<T>()
            {
                dataToReturn = data,
                message = message,
                isSuccess = isSuccess,
            };
        }
    }
}
