using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Crud.BL.Model
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [MinLength(2, ErrorMessage = "Min Len 2")]
        [MaxLength(50, ErrorMessage = "Max Len 50")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Code Required")]
        public string? Code { get; set; }
    }
}
