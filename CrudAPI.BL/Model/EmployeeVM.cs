using System.ComponentModel.DataAnnotations;
using Crud.DAL.Entity;
using Microsoft.AspNetCore.Http;

namespace Crud.BL.Model
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            this.CreationDate = DateTime.Now;
            this.IsDeleted = false;
            this.IsUpdated = false;
        }
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        [MinLength(3, ErrorMessage = "Min Len 3")]
        [MaxLength(50, ErrorMessage = "Max Len 50")]
        public string Name { get; set; }
        [Range(3000, 50000, ErrorMessage = "Range btw 3k : 50dk")]
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        public string? Note { get; set; }
        [Required(ErrorMessage = "Adsress Required")]
        [RegularExpression("[0-9]{2,5}-[a-zA-Z]{2,20}", ErrorMessage = "num-streetName")]
        public string address { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsUpdated { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string? ImgName { get; set; }
        public string? FileName { get; set; }
        public IFormFile? Img { get; set; }
        public IFormFile? File { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
