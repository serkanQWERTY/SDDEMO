using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using SDDEMO.Application.Wrappers;
using SDDEMO.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Interfaces.Managers
{
    public interface IUserManager : IManager<User>
    {
        BaseApiResponse<RegisterViewModel> Register(RegisterDto registerDto);
        BaseApiResponse<LoginViewModel> Login(LoginDto loginDto);
        BaseApiResponse<bool> Logout();
        BaseApiResponse<List<UserViewModel>> GetAllUsers();
        BaseApiResponse<bool> DeleteUser(Guid guid);
        BaseApiResponse<bool> DeleteUserPermanently(Guid guid);
        BaseApiResponse<bool> UpdateUser(UpdateUserDto updateUserDto);
        BaseApiResponse<bool> ChangeStatusUser(Guid guid);
        BaseApiResponse<UserViewModel> GetUserById(Guid guid);
    }
}
