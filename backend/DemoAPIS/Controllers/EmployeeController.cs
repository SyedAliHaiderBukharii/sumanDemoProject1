using DemoAPIS.Configurations;
using DemoDomain.Interfaces;
using DemoDomain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : GenericController
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public string GetEmployeeList(int skipCount, int maxResultCount, string search)
        {
            if(maxResultCount == 0)
            {
                maxResultCount = 10;
            }
            string test = string.Empty;
            search = search?.ToLower();
            int totalRecord =  _unitOfWork.Employees.GetAll().Result.Count();
            var employees = new List<Employee>();
            if (!string.IsNullOrEmpty(search))
                employees = _unitOfWork.Employees.GetAll().Result.Where(a => a.Name.ToLower().Contains(search)
                || a.Email.ToLower().Contains(search)
                || a.RoleName.StartsWith(search)
                ).OrderBy(a => a.Id).Skip(skipCount).Take(maxResultCount).ToList();
            else
                employees = _unitOfWork.Employees.GetAll().Result.OrderBy(a => a.Id).Skip(skipCount).Take(maxResultCount).ToList();

            

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("{");
            sb.Append("\"TotalRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"TotalDisplayRecords\": ");
            sb.Append(totalRecord);
            sb.Append(",");
            sb.Append("\"Data\": ");
            sb.Append(JsonConvert.SerializeObject(employees));
            sb.Append("}");
            return sb.ToString();
        }
        [HttpPost(nameof(CreateEmployee))]
        public IActionResult CreateEmployee(Employee obj)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
            var roles = new List<Employee>();
          
            if (!string.IsNullOrEmpty(obj.Email))

                roles = _unitOfWork.Employees.GetAll().Result.ToList();
            if (roles != null)
            {
                var GetEmp = roles.Where(m => m.Email==obj.Email).FirstOrDefault();
                if (GetEmp!=null)
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseBack<Employee> { Status = "Ok", Message = "Employee Already Exists", Data = null });
                }
                else
                {
                    var result = _unitOfWork.Employees.Add(obj);
                    _unitOfWork.Complete();
                    if (result is not null) return StatusCode(StatusCodes.Status200OK, new ResponseBack<Employee> { Status = "Ok", Message = "Employee added Successfully", Data = null });
                    else return StatusCode(StatusCodes.Status400BadRequest, new ResponseBack<Employee> { Status = "Ok", Message = "Error In Role Creating", Data = null });
                }
            }
            else
            {
                var result = _unitOfWork.Employees.Add(obj);
                _unitOfWork.Complete();
                if (result is not null) return StatusCode(StatusCodes.Status200OK, new ResponseBack<Employee> { Status = "Ok", Message = "Employee added Successfully", Data = null });
                else return StatusCode(StatusCodes.Status400BadRequest, new ResponseBack<Employee> { Status = "Ok", Message = "Error In Employee Creating", Data = null });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseBack<Employee> { Status = "Ok", Message = "Error In Employee Creating", Data = null });
        }

        [HttpPut(nameof(UpdateEmployee))]
        public IActionResult UpdateEmployee(Employee obj)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
            _unitOfWork.Employees.Update(obj);
            _unitOfWork.Complete();
            return StatusCode(StatusCodes.Status200OK, new ResponseBack<Role> { Status = "Ok", Message = "Employee Updated Successfully", Data = null }); ;
        }

        [HttpDelete(nameof(DeleteEmployee))]
        public IActionResult DeleteEmployee(Employee obj)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }

            _unitOfWork.Employees.Delete(obj);
            _unitOfWork.Complete();
            return StatusCode(StatusCodes.Status200OK, new ResponseBack<Employee> { Status = "Ok", Message = "Employee Deleted Successfully", Data = null });
        }  
        [HttpGet(nameof(EmployeeGetByID))]
        public IActionResult EmployeeGetByID(int id)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }

            var GetEmployee  = _unitOfWork.Employees.Get(id);
       
            return StatusCode(StatusCodes.Status200OK, new ResponseBack<Employee> { Status = "Ok", Message = "Role Deleted Successfully", Data = GetEmployee.Result }); 
        }
    }
}
