using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using NLog;
using SDDEMO.Application.Constants;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Extensions;
using SDDEMO.Manager.Helpers;
using System.Net;

namespace SDDEMO.API.ExceptionHandling
{
    public static class ExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            var logger = LogManager.LoadConfiguration(FilePaths.systemLogFolderName).GetCurrentClassLogger();

            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    HttpStatusCode status = HttpStatusCode.InternalServerError;
                    context.Response.StatusCode = (int)status;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;
                        string logMessage = LogMessages.LoggingMessageForError.ToDescriptionString()
                        .Replace("{errorMessage}", ex.Message)
                        .Replace("{stackTrace}", ex.StackTrace);

                        logger.Error(logMessage);

                        var result = ApiHelper<bool>.GenerateApiResponse(false, false, ResponseMessages.AnErrorOccured.ToDescriptionString());
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    }
                });
            });
        }
    }
}
