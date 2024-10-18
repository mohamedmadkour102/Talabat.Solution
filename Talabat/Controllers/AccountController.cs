using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Identity;
using Talabat.DTO_s;
using Talabat.Errors;

namespace Talabat.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost ("Login")] // Post ==> api/Account/Login

        public async Task <ActionResult<UserDto>> Login(LoginDto Model)
        {
            var user = await _userManager.FindByEmailAsync(Model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var Passcheck = await _signInManager.CheckPasswordSignInAsync(user, Model.Password, false);
            if (Passcheck.Succeeded is false) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token = "This is token"
            });

        }

    }
}
