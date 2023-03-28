using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMABackend.Configurations.Users;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Managers.Users;
using MMABackend.Services.Users;
using MMABackend.ViewModels.User;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UnitOfWork _uow;
        private readonly IUsersManager _manager;

        public UserController(IUserService service, UnitOfWork uow, IUsersManager manager)
        {
            _service = service;
            _uow = uow;
            _manager = manager;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult SignUp(UserRegistrationViewModel viewModel)
        {
            _service.AddUser(viewModel);
            return Ok();
        }
        
        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult<BothToken> SignIn(UserCredentials credentials)
        {
            var user = _service.GetUserByCredentials(credentials);
            var tokens = _service.GetBothTokens(user);
            return Ok(tokens);
        }
        
        [HttpPost("GetAccessToken")]
        [Authorize(AuthenticationSchemes = RefreshTokenConfig.SchemeName)]
        public ActionResult<BothToken> GetAccessTokenByRefreshToken()
        {
            var idClaims =  HttpContext.User.FindFirst(x => x.ValueType == ClaimsIdentity.DefaultNameClaimType);
            if (idClaims is null) throw new Exception("While getting access token by refresh token user id claim not found");
            var userId = int.Parse(idClaims.Value);
            var user = _uow.Users.FirstOrDefault(x => x.Id == userId);
            var bothToken = _service.GetBothTokens(user);
            return Ok(bothToken);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public ActionResult SendCodeWordToEmailToRestorePassword(string email)
        {
            var secretWord = Guid.NewGuid().ToString();
            _service.SetSecretWordForRestorePasswordInCache(email, secretWord);
            _manager.SendEmailAsync(email, secretWord);
            return Ok();
        }
        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public ActionResult SendCodeWordToEmailToConfirmEmail(string email)
        {
            var secretWord = Guid.NewGuid().ToString();
            _service.SetSecretWordForConfirmEmailInCache(new ConfirmPasswordViewModel
            {
                SecretWord = secretWord,
                Email = email,
            });
            _manager.SendEmailAsync(email, secretWord);
            return Ok();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult RestorePassword(RestorePasswordViewModel model)
        {
            _service.RestorePassword(model);
            return Ok();
        }
        
        [HttpPost("[action]")]
        [AllowAnonymous]
        public ActionResult ConfirmEmail(ConfirmPasswordViewModel model)
        {
            return Ok();
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = AccessTokenConfig.SchemeName)]
        public ActionResult Test()
        {
            return Ok("This page for admin");
        }
    }
}