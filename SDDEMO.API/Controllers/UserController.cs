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
        /// Register Operation.
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns>RegisterViewModel</returns>
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
        /// GetTokenAndLogin Operation.
        /// </summary>
        /// <param name="loginDto">Username and password</param>
        /// <returns>LoginViewModel</returns>
        [HttpPost("GetTokenAndLogin")]
        public IActionResult GetTokenAndLogin([FromBody] LoginDto loginDto)
        {
            var validationResult = new LoginValidator().Validate(loginDto);

            if (!validationResult.IsValid)
                return BasicResponse.GetValidationErrorResponse(validationResult);

            var result = userManager.Login(loginDto);

            return ApiResponseProvider<LoginViewModel>.CreateResult(result);
        }

        /// <summary>
        /// Logout Operation.
        /// </summary>
        /// <returns>bool</returns>
        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.Logout());
        }

        /// <summary>
        /// GetAllUsers Operation.
        /// </summary>
        /// <returns>List<UserViewModel></returns>
        [HttpGet("GetAllUsers")]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            return ApiResponseProvider<List<UserViewModel>>.CreateResult(userManager.GetAllUsers());
        }

        /// <summary>
        /// UpdateUser Operation.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>bool</returns>
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
        /// ChangeStatusUser Operation.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>bool</returns>
        [HttpPut("ChangeStatusUser")]
        [Authorize]
        public IActionResult ChangeStatusUser(Guid guid)
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.ChangeStatusUser(guid));
        }

        /// <summary>
        /// DeleteUser Operation.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>bool</returns>
        [HttpDelete("DeleteUser")]
        [Authorize]
        public IActionResult DeleteUser(Guid guid)
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.DeleteUser(guid));
        }

        /// <summary>
        /// DeleteUserPermanently Operation.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns>bool</returns>
        [HttpDelete("DeleteUserPermanently")]
        [Authorize]
        public IActionResult DeleteUserPermanently(Guid guid)
        {
            return ApiResponseProvider<bool>.CreateResult(userManager.DeleteUserPermanently(guid));
        }
    }
}
