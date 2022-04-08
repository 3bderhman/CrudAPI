using CrudAPI.BL.Helper;
using CrudAPI.BL.Model;
using CrudAPI.DAL.External;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        [Route("~/api/Role/GetRole")]
        public IActionResult GetRole()
        {
            try
            {
                var data = roleManager.Roles;
                return Ok(new ApiResponsive<IEnumerable<IdentityRole>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Success",
                    Data = data
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
        [Route("~/api/User/GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            try
            {
                var data = await roleManager.FindByIdAsync(id);
                return Ok(new ApiResponsive<IdentityRole>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Success",
                    Data = data
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
        [HttpPost]
        [Route("~/api/Role/PostRole")]
        public async Task<IActionResult> PostRole(IdentityRole model)
        {
            try
            {
                var role = new IdentityRole()
                {
                    Name = model.Name,
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok(new ApiResponsive<IdentityResult>
                    {
                        Code = "201",
                        Status = "Created",
                        Message = "Data saved",
                        Data = result
                    });
                }
                else
                {
                    return BadRequest(new ApiResponsive<string>
                    {
                        Code = "400",
                        Status = "bad request",
                        Message = "Data Not Created",
                        Data = "Failed"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Data Not Created",
                    Data = ex.Message
                });
            }
        }
        [HttpPut]
        [Route("~/api/Role/PutRole")]
        public async Task<IActionResult> PutUser(IdentityRole model)
        {
            try
            {
                var Role = await roleManager.FindByIdAsync(model.Id);
                if (Role != null)
                {
                    Role.Name = model.Name;
                    Role.NormalizedName = model.Name.ToUpper();
                    var result = await roleManager.UpdateAsync(Role);
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
        [Route("~/api/Role/DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var Role = await roleManager.FindByIdAsync(id);
                if (Role != null)
                {
                    var result = await roleManager.DeleteAsync(Role);
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
        [HttpGet]
        [Route("~/api/Role/GetUserRole/{id}")]
        public async Task<IActionResult> GetUserRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if(role != null)
            {
                var model = new List<UserInRoleVM>();
                foreach(var item in userManager.Users)
                {
                    var UserInRole = new UserInRoleVM
                    {
                        Id = item.Id,
                        UserName = item.UserName,
                    };
                    if (await userManager.IsInRoleAsync(item, role.Name))
                    {
                        UserInRole.IsSelected = true;
                    }
                    else
                    {
                        UserInRole.IsSelected = false;
                    }
                    model.Add(UserInRole);
                }
                return Ok(new ApiResponsive<List<UserInRoleVM>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = model
                });
            }
            return BadRequest(new ApiResponsive<string>
            {
                Code = "404",
                Status = "Not Found",
                Message = "Data Not Found",
                Data = "failed"
            });
        }
        [HttpPost]
        [Route("~/api/Role/PostUserRole/{id}")]
        public async Task<IActionResult> PostUserRole( List<UserInRoleVM> model, string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    var user = await userManager.FindByIdAsync(model[i].Id);
                    IdentityResult result = null;
                    if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                }
                return Ok(new ApiResponsive<string>
                {
                    Code = "201",
                    Status = "Created",
                    Message = "Data saved",
                    Data = "Success"
                });

            }
            return BadRequest(new ApiResponsive<string>
            {
                Code = "404",
                Status = "Not Found",
                Message = "Data Not Found",
                Data = "failed"
            });

        }
    }
}
