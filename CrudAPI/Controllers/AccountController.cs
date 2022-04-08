using CrudAPI.BL.Helper;
using CrudAPI.BL.Model;
using CrudAPI.DAL.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost]
        [Route("~/api/Account/Registration")]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok(new ApiResponsive<string>
                    {
                        Code = "200",
                        Status = "Ok",
                        Message = "Success",
                        Data = "Success"
                    });
                }
                else
                {
                    var ErrorList = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        ErrorList.Add(error.Description);
                    }
                    return BadRequest(new ApiResponsive<List<string>>
                    {
                        Code = "400",
                        Status = "Faild",
                        Message = "Faild Registration",
                        Data = ErrorList
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Faild Registration",
                    Data = ex.Message
                });
            }
        }
        [HttpPost]
        [Route("~/api/Account/Login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                var User = await userManager.FindByNameAsync(model.UserName);
                if (User != null)
                {
                    var result = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Ok(new ApiResponsive<string>
                        {
                            Code = "200",
                            Status = "Ok",
                            Message = "Success",
                            Data = "Success"
                        });
                    }
                    else
                    {
                        return BadRequest(new ApiResponsive<string>
                        {
                            Code = "401",
                            Status = "Faild",
                            Message = "Faild Login",
                            Data = "Invalid Account"
                        });
                    }
                }
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "404",
                    Status = "Faild",
                    Message = "Not Found",
                    Data = "Invalid Account"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Faild Registration",
                    Data = ex.Message
                });
            }
        }
        [HttpPost]
        [Route("~/api/Account/SingOut")]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok(new ApiResponsive<string>
            {
                Code = "200",
                Status = "Ok",
                Message = "Success",
                Data = "SignOut"
            });
        }
        [HttpPost]
        [Route("~/api/Account/ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var PasswordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = Token }, Request.Scheme);
                    Email.SendEmail(user.Email, "PasswordReset", PasswordResetLink);
                    return Ok(new ApiResponsive<string>
                    {
                        Code = "200",
                        Status = "Ok",
                        Message = "Success",
                        Data = Token,
                    });
                }
                else
                {
                    return BadRequest(new ApiResponsive<string>
                    {
                        Code = "404",
                        Status = "Faild",
                        Message = "Faild Login",
                        Data = "Invalid Account"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Faild Registration",
                    Data = ex.Message
                });
            }
        }
        [HttpPost]
        [Route("~/api/Account/ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok(new ApiResponsive<string>
                        {
                            Code = "200",
                            Status = "Ok",
                            Message = "Success",
                            Data = "Success"
                        });
                    }
                    else
                    {
                        var ErrorList = new List<string>();
                        foreach (var error in result.Errors)
                        {
                            ErrorList.Add(error.Description);
                        }
                        return BadRequest(new ApiResponsive<List<string>>
                        {
                            Code = "404",
                            Status = "Faild",
                            Message = "Faild Reset",
                            Data = ErrorList
                        });
                    }
                }
                else
                {
                    return BadRequest(new ApiResponsive<string>
                    {
                        Code = "404",
                        Status = "Faild",
                        Message = "Faild Login",
                        Data = "Invalid Account"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Faild Registration",
                    Data = ex.Message
                });
            }
        }
    }
}
