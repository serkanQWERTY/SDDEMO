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

        /// <summary>
        /// Register Manager Method.
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        public BaseApiResponse<RegisterViewModel> Register(RegisterDto registerDto)
        {
            var currentUser = new TokenProvider(_httpContextAccessor, _unitOfWork).GetUserByToken();

            var existingUser = _unitOfWork.userRepository.GetAllWithFilter(u =>
                u.username == registerDto.username || u.mailAddress == registerDto.mailAddress).FirstOrDefault();

            if (existingUser != null)
            {
                logger.InfoLog($"Zaten üyelik mevcut: {existingUser.username}" + $" {existingUser.mailAddress}", isLogin: false, "");
                return ApiHelper<RegisterViewModel>.GenerateApiResponse(false, null, ResponseMessages.UserAlreadyExists.ToDescriptionString());
            }

            var newUser = new User
            {
                id = Guid.NewGuid(),
                name = registerDto.name,
                surname = registerDto.surname,
                username = registerDto.username,
                password = registerDto.password,
                mailAddress = registerDto.mailAddress,
                creationDate = DateTime.UtcNow,
                isActive = true,
                isDeleted = false,
                updatedDate = DateTime.UtcNow,
                createdBy = currentUser.id
            };

            _unitOfWork.userRepository.Add(newUser);
            _unitOfWork.CommitChanges();

            RegisterViewModel mappedData = Mapper.Map<User, RegisterViewModel>(newUser);

            logger.InfoLog($"Yeni kullanıcı kaydı oluşturuldu: {mappedData.username}", isLogin: false, usernameLogin: mappedData.mailAddress);

            return ApiHelper<RegisterViewModel>.GenerateApiResponse(true, mappedData, ResponseMessages.SuccessfullyCreated.ToDescriptionString());
        }

        /// <summary>
        /// Login Manager Method.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public BaseApiResponse<LoginViewModel> Login(LoginDto loginDto)
        {
            var userToSearch = _unitOfWork.userRepository.GetAllWithFilter(d => String.Equals(d.username.Trim(), loginDto.username.Trim()) && String.Equals(d.password.Trim(), loginDto.password.Trim()));

            if (userToSearch != null && userToSearch.Count() > 0)
            {
                User user = userToSearch.FirstOrDefault();
                LoginViewModel mappedData = Mapper.Map<User, LoginViewModel>(user);
                mappedData.token = new TokenProvider().GenerateToken(user);

                logger.InfoLog("Kullanıcı girişi yapıldı. Kullanıcı adı: " + mappedData.username + " Kullanıcı Mail adresi: " + mappedData.mailAddress, true, mappedData.mailAddress);

                return ApiHelper<LoginViewModel>.GenerateApiResponse(true, mappedData, ResponseMessages.SuccessfullyDone.ToDescriptionString());
            }

            return ApiHelper<LoginViewModel>.GenerateApiResponse(false, null, ResponseMessages.UserNotFound.ToDescriptionString());
        }

        /// <summary>
        /// Logout Manager Method.
        /// </summary>
        /// <returns></returns>
        public BaseApiResponse<bool> LogOut()
        {
            var currentUser = new TokenProvider(_httpContextAccessor, _unitOfWork).GetUserByToken();

            logger.InfoLog("Kullanıcı sistemden çıkış yaptı. Kullanıcı adı: " + currentUser.username + " Kullanıcı Mail adresi: " + currentUser.mailAddress, true, currentUser.mailAddress);
            return ApiHelper<bool>.GenerateApiResponse(true, true, ResponseMessages.SuccessfullyDone.ToDescriptionString());
        }

        /// <summary>
        /// Get All Users from Database Method.
        /// </summary>
        /// <returns></returns>
        public BaseApiResponse<List<UserViewModel>> GetAllUsers()
        {
            var users = _unitOfWork.userRepository.GetAll().Where(user => !user.isDeleted).ToList();

            if (users == null || !users.Any())
            {
                logger.InfoLog("Veritabanında kullanıcı bulunamadı.", false);
                return ApiHelper<List<UserViewModel>>.GenerateApiResponse(false, null, ResponseMessages.RecordNotFound.ToDescriptionString());
            }

            var userViewModels = Mapper.Map<List<User>, List<UserViewModel>>(users.ToList());

            return ApiHelper<List<UserViewModel>>.GenerateApiResponse(true, userViewModels, ResponseMessages.SuccessfullyDone.ToDescriptionString());
        }

        /// <summary>
        /// Delete User (Change isDeleted Prop) Method.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public BaseApiResponse<bool> DeleteUser(Guid guid)
        {
            var result = GetUserById(guid);

            if (result.isSuccess && result.dataToReturn != null)
            {
                DeleteFromDatabase(guid);

                logger.InfoLog($"Kullanıcı başarıyla silindi: {result.dataToReturn.username}", false);

                return ApiHelper<bool>.GenerateApiResponse(true, true, ResponseMessages.SuccessfullyDeleted.ToDescriptionString());
            }

            logger.InfoLog($"Kullanıcı bulunamadı veya işlem sırasında bir hata oluştu. Kullanıcı ID: {guid}", false);
            return ApiHelper<bool>.GenerateApiResponse(false, false, ResponseMessages.AnErrorOccured.ToDescriptionString());
        }

        /// <summary>
        /// Delete User (Permanently) Method.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public BaseApiResponse<bool> DeleteUserPermanently(Guid guid)
        {
            if (guid == null)
            {
                return ApiHelper<bool>.GenerateApiResponse(false, false, ResponseMessages.InvalidValue.ToDescriptionString());
            }

            _unitOfWork.userRepository.DeletePermanently(guid);
            _unitOfWork.CommitChanges();

            logger.InfoLog($"Kullanıcı başarıyla tamamen silindi.", false);

            return ApiHelper<bool>.GenerateApiResponse(true, true, ResponseMessages.SuccessfullyDeleted.ToDescriptionString());

        }

        /// <summary>
        /// Update User Method.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public BaseApiResponse<UserViewModel> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change Status Method.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public BaseApiResponse<bool> ChangeStatusUser(Guid guid)
        {
            var result = GetUserById(guid);

            if (result.isSuccess && result.dataToReturn != null)
            {
                logger.InfoLog($"Kullanıcı bulundu:  {result.dataToReturn.username}", false);

                User dataToUpdate = GetByIdFromDatabase(guid);

                dataToUpdate.updatedDate = DateTime.Now;
                dataToUpdate.isActive = !dataToUpdate.isActive;

                UpdateInDatabase(dataToUpdate);

                logger.InfoLog($" Kullanıcı durumu değiştirildi: {dataToUpdate.username}, Yeni Durum: {dataToUpdate.isActive}", false);

                return ApiHelper<bool>.GenerateApiResponse(true, true, ResponseMessages.SuccessfullyChanged.ToDescriptionString());
            }

            logger.InfoLog($"Bu id için kullanıcı bulunamadı veya işlem başarısız oldu: {guid}", false);
            return ApiHelper<bool>.GenerateApiResponse(false, false, ResponseMessages.AnErrorOccured.ToDescriptionString());
        }


        /// <summary>
        /// Get By Id User Method.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public BaseApiResponse<UserViewModel> GetUserById(Guid guid)
        {
            var user = _unitOfWork.userRepository.GetAll()
                .Where(u => !u.isDeleted && u.id == guid)
                .FirstOrDefault();

            if (user == null)
            {
                logger.InfoLog("Veritabanında kullanıcı bulunamadı.", false);
                return ApiHelper<UserViewModel>.GenerateApiResponse(false, null, ResponseMessages.RecordNotFound.ToDescriptionString());
            }

            var userViewModel = Mapper.Map<User, UserViewModel>(user);

            logger.InfoLog($"Kullanıcı:  {userViewModel.id}");
            return ApiHelper<UserViewModel>.GenerateApiResponse(true, userViewModel, ResponseMessages.SuccessfullyDone.ToDescriptionString());
        }
    }
}
