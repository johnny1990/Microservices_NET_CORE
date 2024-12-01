using Contracts;
using Entities;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetEmployees()
        {
            ResponseType type = ResponseType.Success;

            try
            {
                var emp = _empR.GetEmployees();

                if (!emp.Any())
                {
                    type = ResponseType.NotFound;
                }

                return Ok(ResponseHandler.GetAppResponse(type, emp));
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }         
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetEmployeeById(int id)
        {
            ResponseType type = ResponseType.Success;

            try
            {
                var emp = _empR.GetEmployeeById(id);

                if (emp == null)
                {
                    type = ResponseType.NotFound;
                }

                return Ok(ResponseHandler.GetAppResponse(type, emp));

            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("InsertEmployee")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult InsertEmployee([FromBody] Employees emp)
        {          
            try
            {
                using (var scope = new TransactionScope())
                {
                    ResponseType type = ResponseType.Success;
                    _empR.InsertEmployee(emp);
                    scope.Complete();
                    return Ok(ResponseHandler.GetAppResponse(type, emp));
                }
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateEmployee([FromBody] Employees emp)
        {
            if (emp != null)
            {
                using (var scope = new TransactionScope())
                {
                    ResponseType type = ResponseType.Success;
                    _empR.UpdateEmployee(emp);
                    scope.Complete();
                    return Ok(ResponseHandler.GetAppResponse(type, emp));
                }
            }
            return new NoContentResult();
        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _empR.DeleteEmployee(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
             catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
