using Microsoft.AspNetCore.Http;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Extensions;
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
    public class UserManager : Manager<User>, IUserManager
    {
        private ILoggingManager logger;
        public UserManager(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ILoggingManager logger) : base(unitOfWork, httpContextAccessor, logger)
        {
            this.logger = logger;
        }

        public BaseApiResponse<RegisterViewModel> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }

        public BaseApiResponse<LoginViewModel> Login(LoginDto loginDto)
        {
            var userToSearch = _unitOfWork.userRepository.GetAllWithFilter(d => String.Equals(d.username.Trim(), loginDto.username.Trim()) && String.Equals(d.password.Trim(), loginDto.password.Trim()));

            if (userToSearch != null && userToSearch.Count() > 0)
            {
                User user = userToSearch.FirstOrDefault();
                LoginViewModel mappedData = Mapper.Map<User, LoginViewModel>(user);
                mappedData.token = new TokenProvider().GenerateToken(user);

                logger.InfoLog("Kullanıcı girişi yapıldı. Kullanıcı adı: " + mappedData.username, true, mappedData.username);

                return ApiHelper<LoginViewModel>.GenerateApiResponse(true, mappedData, "");
            }

            return ApiHelper<LoginViewModel>.GenerateApiResponse(false, null, ResponseMessages.UserNotFound.ToDescriptionString());
        }

        public BaseApiResponse<bool> LogOut()
        {
            logger.InfoLog("Kullanıcı sistemden çıkış yaptı.");
            return ApiHelper<bool>.GenerateApiResponse(true, true, "");
        }
    }
}