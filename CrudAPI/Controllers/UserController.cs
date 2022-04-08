using CrudAPI.BL.Helper;
using CrudAPI.DAL.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpGet]
        [Route("~/api/User/GetUser")]
        public IActionResult GetUser()
        {
            try
            {
                var data = userManager.Users;
                var UserList = new List<GetUser>();
                foreach (var user in data)
                {
                    UserList.Add(new GetUser
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        IsAgree = user.IsAgree,
                    });
                }
                return Ok(new ApiResponsive<IEnumerable<GetUser>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Success",
                    Data = UserList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Faild",
                    Data = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("~/api/User/GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var data = await userManager.FindByIdAsync(id);
                var User = new GetUser
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    Email = data.Email,
                    IsAgree = data.IsAgree,
                };
                return Ok(new ApiResponsive<GetUser>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Success",
                    Data = User
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Faild",
                    Data = ex.Message
                });
            }
        }
        [HttpPut]
        [Route("~/api/User/PutUser")]
        public async Task<IActionResult> PutUser(ApplicationUser model)
        {
            try
            {
                var user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.NormalizedUserName = model.UserName.ToUpper();
                    user.Email = model.Email;
                    user.NormalizedEmail = model.Email.ToUpper();
                    user.SecurityStamp = model.SecurityStamp;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new ApiResponsive<string>
                        {
                            Code = "202",
                            Status = "Accepted",
                            Message = "Data Updated",
                            Data = "Success"
                        });
                    }
                    else
                    {
                        return BadRequest(new ApiResponsive<string>
                        {
                            Code = "400",
                            Status = "bad request",
                            Message = "Data Not Updated",
                            Data = "Failed"
                        });
                    }
                }
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "404",
                    Status = "bad request",
                    Message = "Data Not found",
                    Data = "Failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Data Not Updated",
                    Data = ex.Message
                });
            }
        }
        [HttpDelete]
        [Route("~/api/User/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new ApiResponsive<string>
                        {
                            Code = "202",
                            Status = "Accepted",
                            Message = "Data Deleted",
                            Data = "Data Deleted"
                        });
                    }
                    else
                    {
                        return BadRequest(new ApiResponsive<string>
                        {
                            Code = "400",
                            Status = "bad request",
                            Message = "Data Not Delete",
                            Data = "Failed"
                        });
                    }
                }
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "404",
                    Status = "bad request",
                    Message = "Data Not found",
                    Data = "Failed"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Data Not Updated",
                    Data = ex.Message
                });
            }
        }
    }
}
