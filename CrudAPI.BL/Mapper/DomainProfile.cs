using AutoMapper;
using Crud.BL.Model;
using Crud.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAPI.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<DepartmentVM, Department>();
            CreateMap<Department, DepartmentVM>();
            CreateMap<EmployeeVM, Employee>();
            CreateMap<Employee, EmployeeVM>();

        }
    }
}
