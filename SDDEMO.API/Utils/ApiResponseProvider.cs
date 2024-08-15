using Microsoft.AspNetCore.Mvc;
using SDDEMO.Application.Wrappers;

namespace SDDEMO.API.Utils
{
    public class ApiResponseProvider<T> 
    {
        public static ObjectResult CreateResult(BaseApiResponse<T> baseApiResponse)
        {
            if (baseApiResponse.isSuccess)
                return new OkObjectResult(baseApiResponse);

            return new BadRequestObjectResult(baseApiResponse);
        }
    }
}
