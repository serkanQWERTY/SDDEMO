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
        [Authorize]
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
    }
}
