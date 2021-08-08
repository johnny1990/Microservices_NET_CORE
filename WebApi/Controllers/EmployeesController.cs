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
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _empR;

        public EmployeesController(IEmployeeRepository empR)
        {
            _empR = empR;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public IActionResult GetEmployees()
        {
            try
            {
                var emp = _empR.GetEmployees();
                return new OkObjectResult(emp);
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }         
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public IActionResult GetEmployeeById(int id)
        {            
            try
            {
                var emp = _empR.GetEmployeeById(id);
                return new OkObjectResult(emp);
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        [HttpPost]
        [Route("InsertEmployee")]
        public IActionResult InsertEmployee([FromBody] Employees emp)
        {          
            try
            {
                using (var scope = new TransactionScope())
                {
                    _empR.InsertEmployee(emp);
                    scope.Complete();
                    return CreatedAtAction(nameof(GetEmployees), new { id = emp.Id }, emp);
                }
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return NotFound();
            }
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromBody] Employees emp)
        {
            if (emp != null)
            {
                using (var scope = new TransactionScope())
                {
                    _empR.UpdateEmployee(emp);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _empR.DeleteEmployee(id);
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
