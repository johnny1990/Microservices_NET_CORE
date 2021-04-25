using Contracts;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace WebApi.Controllers
{
    [Route("api/Departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _depR;

        public DepartmentsController(IDepartmentRepository depR)
        {
            _depR = depR;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public IActionResult GetDepartments()
        {
            var dep = _depR.GetDepartments();
            return new OkObjectResult(dep);
        }

        [HttpGet]
        [Route("GetDepartmentById")]
        public IActionResult GetDepartmentById(int id)
        {
            var dep = _depR.GetDepartmentById(id);
            return new OkObjectResult(dep);
        }

        [HttpPost]
        [Route("InsertDepartment")]
        public IActionResult InserDepartment([FromBody] Departments dep)
        {
            using (var scope = new TransactionScope())
            {
                _depR.InsertDepartment(dep);
                scope.Complete();
                return CreatedAtAction(nameof(GetDepartments), new { id = dep.Id }, dep);
            }
        }

        [HttpPut]
        [Route("UpdateDepartment")]
        public IActionResult UpdateDepartment([FromBody] Departments dep)
        {
            if (dep != null)
            {
                using (var scope = new TransactionScope())
                {
                    _depR.UpdateDepartment(dep);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteDepartment(int id)
        {
            _depR.DeleteDepartment(id);
            return new OkResult();
        }
    }
}

