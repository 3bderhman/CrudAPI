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
    public class EmployeeController : ControllerBase
    {
        private readonly IGenaricDERep<Department> deparmtnet;
        private readonly IGenaricDERep<Employee> employee;
        private readonly IMapper mapper;

        public EmployeeController(IGenaricDERep<Department> deparmtnet, IGenaricDERep<Employee> employee, IMapper mapper)
        {
            this.deparmtnet = deparmtnet;
            this.employee = employee;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("~/api/employee/GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var data = await employee.GetAllAsync();
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponsive<IEnumerable<EmployeeVM>>
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
        [Route("~/api/employee/GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var data = await employee.GetByIdAsync(x => x.Id == id);
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponsive<EmployeeVM>
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
        [Route("~/api/employee/PostEmployee")]
        public async Task<IActionResult> PostEmployee([FromForm] EmployeeVM obj)
        {
            try
            {

                var data = mapper.Map<Employee>(obj);
                data.ImgName = Files.UpbloadFile(obj.Img, "Imgs");
                data.FileName = Files.UpbloadFile(obj.File, "Docs");
                var result = await employee.Create(data);
                return Ok(new ApiResponsive<Employee>
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
        [Route("~/api/employee/PutEmployee")]
        public async Task<IActionResult> PutEmployee([FromForm] EmployeeVM obj)
        {
            try
            {
                var data = mapper.Map<Employee>(obj);
                var result = await employee.Update(data);
                return Ok(new ApiResponsive<Employee>
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
        [Route("~/api/employee/DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var data = await employee.GetByIdAsync(x => x.Id == id);
                Files.DeleteFile("Imgs", data.ImgName);
                Files.DeleteFile("Docs", data.FileName);
                await employee.Delete(id);
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
