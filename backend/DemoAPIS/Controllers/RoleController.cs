using DemoAPIS.Configurations;
using DemoDomain.Interfaces;
using DemoDomain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoAPIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : GenericController
    {
       

        private readonly IUnitOfWork _unitOfWork;
        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public string GetRolesList(int startIndex, int pagelength, string search)
        {
            if (pagelength == 0)
            {
                pagelength = 10;
            }
            string test = string.Empty;
            search = search?.ToLower();
            int totalRecord = _unitOfWork.Roles.GetAll().Result.Count();
            var roles = new List<Role>();
            if (!string.IsNullOrEmpty(search))
                roles = _unitOfWork.Roles.GetAll().Result.Where(a => a.Name.ToLower().Contains(search)
              ).OrderBy(a => a.Id).Skip(startIndex).Take(pagelength).ToList();
            else
                roles = _unitOfWork.Roles.GetAll().Result.OrderBy(a => a.Id).Skip(startIndex).Take(pagelength).ToList();

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
            sb.Append(JsonConvert.SerializeObject(roles));
            sb.Append("}");
            return sb.ToString();
        }
        [HttpPost(nameof(CreateRole))]
        public IActionResult CreateRole(Role obj)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
            var roles = new List<Role>();
            //   var data = _unitOfWork.Roles.Get(obj.Name);
            if (!string.IsNullOrEmpty(obj.Name))

                roles = _unitOfWork.Roles.GetAll().Result.ToList();
            if (roles != null)
            {
                var GetRole = roles.Exists(m => m.Name.Equals(obj.Name, StringComparison.CurrentCultureIgnoreCase));
                if (GetRole)
                {
                    return StatusCode(StatusCodes.Status200OK, new ResponseBack<Role> { Status = "Ok", Message = "Role Already Exists", Data = null });
                }
                else {
                    var result = _unitOfWork.Roles.Add(obj);
                    _unitOfWork.Complete();
                    if (result is not null) return StatusCode(StatusCodes.Status200OK, new ResponseBack<Role> { Status = "Ok", Message = "Role added Successfully", Data = null });
                    else return StatusCode(StatusCodes.Status400BadRequest, new ResponseBack<Role> { Status = "Ok", Message = "Error In Role Creating", Data = null });
                }
            }
            else {
                var result = _unitOfWork.Roles.Add(obj);
                _unitOfWork.Complete();
                if (result is not null) return StatusCode(StatusCodes.Status200OK, new ResponseBack<Role> { Status = "Ok", Message = "Role added Successfully", Data = null });
                else return StatusCode(StatusCodes.Status400BadRequest, new ResponseBack<Role> { Status = "Ok", Message = "Error In Role Creating", Data = null });
            }
           return StatusCode(StatusCodes.Status400BadRequest, new ResponseBack<Role> { Status = "Ok", Message = "Error In Role Creating", Data = null });
        }

        [HttpPut(nameof(UpdateRole))]
        public IActionResult UpdateRole(Role obj)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
            _unitOfWork.Roles.Update(obj);
            _unitOfWork.Complete();
            return StatusCode(StatusCodes.Status200OK, new ResponseBack<Role> { Status = "Ok", Message = "Role Updated Successfully", Data = null }); ;
        } 
        [HttpPut(nameof(DeleteRole))]
        public IActionResult DeleteRole(Role obj)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }

            _unitOfWork.Roles.Delete(obj);
            _unitOfWork.Complete();
            return StatusCode(StatusCodes.Status200OK, new ResponseBack<Role> { Status = "Ok", Message = "Role Deleted Successfully", Data = null }); 
        }
        [HttpGet(nameof(RoleGetByID))]
        public IActionResult RoleGetByID(int id)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }

            var GetRole = _unitOfWork.Roles.Get(id);
        //    return Ok(GetRole);
            return StatusCode(StatusCodes.Status200OK, new ResponseBack<Role> { Status = "Ok", Message = "Role Deleted Successfully", Data = GetRole.Result }) ;
        }

    }
}
