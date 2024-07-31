using BusinessWebSoftPvtLmtTaskApi.Data;
using BusinessWebSoftPvtLmtTaskApi.Models;
using BusinessWebSoftPvtLmtTaskApi.Repository.IRepository;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BusinessWebSoftPvtLmtTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository _repository;

        public EmployeeController(IGenericRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _repository.ListAsync<Employee>("SP_GetEmployees"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error While Retriving Data");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is required.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", Guid.NewGuid());
                    parameters.Add("@Name", employee.Name);
                    parameters.Add("@Address", employee.Address);
                    parameters.Add("@Email", employee.Email);
                    parameters.Add("@Phone", employee.Phone);
                    parameters.Add("@Salary", employee.Salary);
                    parameters.Add("@Designation", employee.Designation);

                    await _repository.ExecuteAsync("SP_CreateEmployee", parameters);
                    return Ok("Employee created successfully.");
                }
                else
                {
                    return BadRequest("Invalid employee data.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the employee.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest("Employee data is invalid.");
            }



            try
            {
                if (ModelState.IsValid)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", employee.Id);
                    parameters.Add("@Name", employee.Name);
                    parameters.Add("@Address", employee.Address);
                    parameters.Add("@Email", employee.Email);
                    parameters.Add("@Phone", employee.Phone);
                    parameters.Add("@Salary", employee.Salary);
                    parameters.Add("@Designation", employee.Designation);

                    await _repository.ExecuteAsync("SP_UpdateEmployee", parameters);
                    return Ok("Employee updated successfully.");
                }
                else
                {
                    return BadRequest("Invalid employee data.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the employee.");
            }
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            try
            {
                var employee = await _repository.SingleAsync<Employee>("SP_GetEmployeeById", parameters);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the Employee.");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            try
            {
                await _repository.ExecuteAsync("Sp_DeleteEmployeeById", parameters);
                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the contact.");
            }
        }
    }
}

