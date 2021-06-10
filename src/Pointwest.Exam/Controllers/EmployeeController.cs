using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pointwest.Exam.Domain.Models;
using Pointwest.Exam.Domain.Repositories;
using Pointwest.Exam.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Exam.Controllers
{
    /// <summary>
    /// Employee API
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repo"></param>
        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _repo.GetListAsync();
            return Ok(employees);
        }
        /// <summary>
        /// get all Employees by page
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var employees = await _repo.GetListAsync(pageNumber, pageSize);
            return Ok(employees);
        }
        /// <summary>
        /// Get Employee by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var employee = await _repo.GetByIdAsync(id);
            if (employee == null)
            {
                return BadRequest();
            }
            return Ok(employee);
        }
        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeViewModel employee)
        {
            var id = await _repo.InsertAsync(new Employee { 
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName
            });
            return CreatedAtAction(nameof(GetById), new { id }, employee);
        }
        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UpdateEmployeeViewModel employee)
        {
            var updateEmployee = await _repo.GetByIdAsync(employee.Id);
            if (updateEmployee == null)
            {
                return BadRequest();
            }

            updateEmployee.FirstName = employee.FirstName;
            updateEmployee.MiddleName = employee.MiddleName;
            updateEmployee.LastName = employee.LastName;

            await _repo.UpdateAsync(updateEmployee);
            return Ok();
        }
        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await _repo.GetByIdAsync(id);
            if (employee == null)
            {
                return BadRequest();
            }
            await _repo.DeleteAsync(employee);
            return Ok();
        }
    }
}
