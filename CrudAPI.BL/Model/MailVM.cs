using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Crud.BL.Model
{
    public class MailVM
    {
        [Required(ErrorMessage = "requried")]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
