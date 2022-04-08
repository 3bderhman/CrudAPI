using Crud.BL.Model;
using CrudAPI.BL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        [Route("~/api/email/PostEmail")]
        public IActionResult PostEmail(MailVM mail)
        {
            try
            {
                var data = Email.SendEmail(mail.Email, mail.Subject, mail.Body);
                return Ok(new ApiResponsive<string>
                {
                    Code = "201",
                    Status = "Sent",
                    Message = "Data Send",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponsive<string>
                {
                    Code = "201",
                    Status = "Sent",
                    Message = "Data Send",
                    Data = ex.Message
                });
            }
        }
    }
}
