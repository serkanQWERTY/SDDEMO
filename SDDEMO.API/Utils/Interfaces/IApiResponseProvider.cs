using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using SDDEMO.Application.Wrappers;

namespace SDDEMO.API.Utils.Interfaces
{
    public interface IApiResponseProvider
    {
        ObjectResult CreateResult(BaseApiResponse<T> baseApiResponse);
    }
}
