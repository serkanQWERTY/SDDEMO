using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using SDDEMO.Application.Constants;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Interfaces.Managers;
using SDDEMO.Application.Interfaces.UnitOfWork;
using SDDEMO.Application.Wrappers;
using SDDEMO.Domain.Entity;
using SDDEMO.Manager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Manager.Managers
{
    public class LoggingManager : ILoggingManager
    {
        private IServiceScopeFactory serviceScopeFactory;
        private IUnitOfWork unitOfWork;
        private IHttpContextAccessor httpContextAccessor;
        private Logger logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public LoggingManager(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;

            var configMain = new NLog.Config.XmlLoggingConfiguration(FilePaths.systemLogFolderName);
            LogManager.Configuration = configMain;
            logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Infolog Method.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isLogin"></param>
        /// <param name="usernameLogin"></param>
        public void InfoLog(string message, bool isLogin = false, string usernameLogin = "")
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();

                if (isLogin)
                {
                    var eventInfo = new LogEventInfo(LogLevel.Info, logger.Name, message);
                    var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                    eventInfo.Properties["user"] = usernameLogin;
                    eventInfo.Properties["ipAddress"] = ipAddress;
                    logger.Log(eventInfo);
                }
                else
                {
                    User user = new TokenProvider(httpContextAccessor, unitOfWork).GetUserByToken();
                    var eventInfo = new LogEventInfo(LogLevel.Info, logger.Name, message);
                    var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                    eventInfo.Properties["user"] = user is null ? "---" : user.username;
                    eventInfo.Properties["ipAddress"] = ipAddress;
                    logger.Log(eventInfo);
                }
            }
        }

        /// <summary>
        /// ErrorLog Method.
        /// </summary>
        /// <param name="message"></param>
        public void ErrorLog(string message)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
                httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();

                User user = new TokenProvider(httpContextAccessor, unitOfWork).GetUserByToken();
                var eventInfo = new LogEventInfo(LogLevel.Error, logger.Name, message);
                var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                eventInfo.Properties["user"] = user is null ? "---" : user.username;
                eventInfo.Properties["ipAddress"] = ipAddress;
                logger.Log(eventInfo);
            }
        }
    }
}
