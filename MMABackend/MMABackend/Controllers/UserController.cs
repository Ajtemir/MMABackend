using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MMABackend.Configurations.Users;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;
using MMABackend.Managers.Users;
using MMABackend.Services.Users;
using MMABackend.ViewModelResults.Common;
using MMABackend.ViewModels.User;

namespace MMABackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly UnitOfWork _uow;
        private readonly IUsersManager _manager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _appEnvironment;

        public UserController
        (
            IUserService service,
            UnitOfWork uow,
            IUsersManager manager,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, IWebHostEnvironment appEnvironment)
        {
            _service = service;
            _uow = uow;
            _manager = manager;
            _userManager = userManager;
            _roleManager = roleManager;
            _appEnvironment = appEnvironment;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(UserRegistrationViewModel viewModel)
        {
            if (_userManager.Users.FirstOrDefault(x => x.Email == viewModel.Email) != null)
                throw new Exception("User already registered");
            var result = await _userManager.CreateAsync(viewModel, viewModel.Password);

            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<BothTokenViewModel>> SignIn(UserCredentialsViewModel model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email)
                       ?? throw new Exception("User not found by email");
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            return result ? Ok(_service.GetBothTokens(user)) : BadRequest("Wrong password");
        }

        [HttpPost]
        [Authorize(Roles = "owner", AuthenticationSchemes = AccessTokenConfig.SchemeName)]
        public async Task<ActionResult<string>> AddRole(AddRoleViewModel model)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
            return Ok(result);
        }

        [HttpPost("GetAccessToken")]
        [Authorize(AuthenticationSchemes = RefreshTokenConfig.SchemeName)]
        public ActionResult<BothTokenViewModel> GetAccessTokenByRefreshToken()
        {
            var idClaims = HttpContext.User
                .FindFirst(x => x.Type == RefreshTokenConfig.UserIdClaim);
            if (idClaims is null)
                throw new Exception("While getting access token by refresh token user id claim not found");
            var email = idClaims.Value;
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email)
                       ?? throw new Exception("Not found user while getting access token through refresh one by email");
            var bothToken = _service.GetBothTokens(user);
            return Ok(bothToken);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCodeWordToEmailToRestorePassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ??
                       throw new Exception("User not found");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _manager.SendEmailAsync(email, token);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCodeWordToEmailToConfirmEmail(string email)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email)
                       ?? throw new Exception("User not found");
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _manager.SendEmailAsync(email, code);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RestorePassword(RestorePasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email) ?? throw new Exception("User not found");
            var result = await _userManager.ResetPasswordAsync(user, model.SecretWord, model.NewPassword);
            return result.Succeeded ? Ok() : BadRequest(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(ConfirmPasswordViewModel model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email)
                       ?? throw new Exception("User not found");
            var result = await _userManager.ConfirmEmailAsync(user, model.SecretWord);
            return result.Succeeded ? Ok() : BadRequest(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateProfile(UpdateProfileViewModel model)
        {
            var user = _uow.GetUserByEmailOrError(model.Email);
            user.UserName = model.Username;
            user.PhoneNumber = model.PhoneNumber;
            await _uow.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetProfile([FromQuery]GetProfileViewModel model)
        {
            var user = _uow.GetUserByEmailOrError(model.Email);
            var avatar = _uow.UserAvatars.FirstOrDefault(x => x.UserId == user.Id);
            return Ok(new ProfileResult
            {
                Email = user.Email,
                Phone = user.PhoneNumber,
                Username = user.UserName,
                Avatar = avatar?.Path,
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_uow.Users.Select(x => x.Email));
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ProfileAvatar([FromQuery]string email, [FromQuery]bool? updateDelete,[FromForm] SetProfilePhotoViewModel model)
        {
            var user = _uow.GetUserByEmailOrError(email);
            var extension = Path.GetExtension(model.Avatar.FileName)!;
            const string folder = "/avatars/";
            switch (updateDelete)
            {
                case true:
                    var fileName = Guid.NewGuid();
                    string pathUpdate =  folder + fileName + extension;
                    await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathUpdate, FileMode.Create))
                        await model.Avatar.CopyToAsync(fileStream);
                    var avatar = _uow.UserAvatars.FirstOrError(x => x.UserId == user.Id);
                    avatar.Path = pathUpdate;
                    _uow.Update(avatar);
                    await _uow.SaveChangesAsync();
                    break;
                case false:
                    var avatarDelete = _uow.UserAvatars.FirstOrError(x => x.UserId == user.Id);
                    _uow.Remove(avatarDelete);
                    await _uow.SaveChangesAsync();
                    break;
                default:
                    var searched = _uow.UserAvatars.FirstOrDefault(x => x.UserId == user.Id);
                    if (searched is not null) throw new Exception("Avatar exists");
                    var fileNameCreate = Guid.NewGuid();
                    string pathCreate =  folder + fileNameCreate + extension;
                    await using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathCreate, FileMode.Create))
                        await model.Avatar.CopyToAsync(fileStream);
                    var file = new UserAvatar { UserId = user.Id, Path = pathCreate};
                    _uow.UserAvatars.Add(file);
                    await _uow.SaveChangesAsync();
                    break;
            }
            return Ok();
        }
        
    }
}