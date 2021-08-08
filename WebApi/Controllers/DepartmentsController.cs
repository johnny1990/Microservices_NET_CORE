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
            try
            {
                var dep = _depR.GetDepartments();
                return new OkObjectResult(dep);
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetDepartmentById")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var dep = _depR.GetDepartmentById(id);
                return new OkObjectResult(dep);
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        [HttpPost]
        [Route("InsertDepartment")]
        public IActionResult InserDepartment([FromBody] Departments dep)
        {
            
            try
            {
                using (var scope = new TransactionScope())
                {
                    _depR.InsertDepartment(dep);
                    scope.Complete();
                    return CreatedAtAction(nameof(GetDepartments), new { id = dep.Id }, dep);
                }
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
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
            try
            {
                _depR.DeleteDepartment(id);
                return new OkResult();
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }
    }
}

