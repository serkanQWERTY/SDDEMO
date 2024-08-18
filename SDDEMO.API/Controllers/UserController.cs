using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDDEMO.API.Utils;
using SDDEMO.API.Validators;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using SDDEMO.Application.Interfaces.Managers;
using System.Configuration;

namespace SDDEMO.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userManager"></param>
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// User Register Operation.
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>Returns ViewModel</returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var validationResult = new RegisterValidator().Validate(registerDto);

            if (!validationResult.IsValid)
                return BasicResponse.GetValidationErrorResponse(validationResult);

            var result = userManager.Register(registerDto);

            return ApiResponseProvider<RegisterViewModel>.CreateResult(result);
        }

        /// <summary>
        /// Login operation by username and password.
        /// </summary>
        /// <param name="loginDto">Username and password</param>
        /// <returns>Returns token if login operation is successfull</returns>
        [HttpPost("GetTokenAndLogin")]
        public IActionResult GetToken([FromBody] LoginDto loginDto)
        {
            var validationResult = new LoginValidator().Validate(loginDto);

            if (!validationResult.IsValid)
                return BasicResponse.GetValidationErrorResponse(validationResult);

            var result = userManager.Login(loginDto);

            return ApiResponseProvider<LoginViewModel>.CreateResult(result);
        }

        /// <summary>
        /// User log out.
        /// </summary>
        /// <returns>Returns state</returns>
        [HttpGet("LogOut")]
        [Authorize]
        public IActionResult LogOut()
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.LogOut());
        }

        /// <summary>
        /// Get All Users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            return ApiResponseProvider<List<UserViewModel>>.CreateResult(userManager.GetAllUsers());
        }

        /// <summary>
        /// Update User.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] UpdateUserDto dto)
        {
            var validationResult = new UpdateValidator().Validate(dto);

            if (!validationResult.IsValid)
                return BasicResponse.GetValidationErrorResponse(validationResult);

            return ApiResponseProvider<bool>.CreateResult(userManager.UpdateUser(dto));
        }

        /// <summary>
        /// Change Status of User.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpPut("ChangeStatusUser")]
        [Authorize]
        public IActionResult ChangeStatusUser(Guid guid)
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.ChangeStatusUser(guid));
        }

        /// <summary>
        /// Delete User.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUser")]
        [Authorize]
        public IActionResult DeleteUser(Guid guid)
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.DeleteUser(guid));
        }


        /// <summary>
        /// Delete User Permanently.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpDelete("DeleteUserPermanently")]
        [Authorize]
        public IActionResult DeleteUserPermanently(Guid guid)
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.DeleteUserPermanently(guid));
        }
    }
}
