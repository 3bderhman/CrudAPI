using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud.DAL.Entity
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string Email { get; set; }
        public string? Note { get; set; }
        public string address { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsUpdated { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string? ImgName { get; set; }
        public string? FileName { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
