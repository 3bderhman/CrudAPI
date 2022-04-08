using AutoMapper;
using Crud.BL.Interface;
using Crud.BL.Model;
using Crud.DAL.Entity;
using CrudAPI.BL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenaricDERep<Department> deparmtnet;
        private readonly IMapper mapper;

        public DepartmentController(IGenaricDERep<Department> deparmtnet, IMapper mapper)
        {
            this.deparmtnet = deparmtnet;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("~/api/department/GetDepartment")]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                var data = await deparmtnet.GetAllAsync();
                var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
                return Ok(new ApiResponsive<IEnumerable<DepartmentVM>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message
                });
            }

        }
        [HttpGet]
        [Route("~/api/department/GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var data = await deparmtnet.GetByIdAsync(x => x.Id == id);
                var result = mapper.Map<DepartmentVM>(data);
                return Ok(new ApiResponsive<DepartmentVM>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "Data Not Found",
                    Data = ex.Message
                });
            }

        }
        [HttpPost]
        [Route("~/api/department/PostDepartment")]
        public async Task<IActionResult> PostDepartment(DepartmentVM obj)
        {
            try
            {
                var data = mapper.Map<Department>(obj);
                var result = await deparmtnet.Create(data);
                return Ok(new ApiResponsive<Department>
                {
                    Code = "201",
                    Status = "Created",
                    Message = "Data saved",
                    Data = result
                });
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
        [Route("~/api/department/PutDepartment")]
        public async Task<IActionResult> PutDepartment(DepartmentVM obj)
        {
            try
            {
                var data = mapper.Map<Department>(obj);
                var result = await deparmtnet.Update(data);
                return Ok(new ApiResponsive<Department>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Updated",
                    Data = result
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
        [Route("~/api/department/DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await deparmtnet.Delete(id);
                return Ok(new ApiResponsive<string>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Deleted",
                    Data = "Data Deleted"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "400",
                    Status = "bad request",
                    Message = "Data Not Deleted",
                    Data = ex.Message
                });
            }
        }
    }
}
