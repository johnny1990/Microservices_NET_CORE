using Contracts;
using Entities;
using Helpers;
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
            ResponseType type = ResponseType.Success;

            try
            {
                var dep = _depR.GetDepartments();
                if (!dep.Any())
                {
                    type = ResponseType.NotFound;
                }

                return Ok(ResponseHandler.GetAppResponse(type, dep));
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("GetDepartmentById")]
        public IActionResult GetDepartmentById(int id)
        {
            ResponseType type = ResponseType.Success;

            try
            {
                var dep = _depR.GetDepartmentById(id);
                if (dep == null)
                {
                    type = ResponseType.NotFound;
                }

                return Ok(ResponseHandler.GetAppResponse(type, dep));
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
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
                    ResponseType type = ResponseType.Success;
                    _depR.InsertDepartment(dep);
                    scope.Complete();
                    return Ok(ResponseHandler.GetAppResponse(type, dep));
                }
            }
            catch (Exception ex)
            {
                Logger.LogWriter.LogException(ex);
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
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
                    ResponseType type = ResponseType.Success;
                    _depR.UpdateDepartment(dep);
                    scope.Complete();
                    return Ok(ResponseHandler.GetAppResponse(type, dep));
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
                ResponseType type = ResponseType.Success;
                _depR.DeleteDepartment(id);
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

