using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using ZooBookSys.Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZooBookSys.Exam.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }

        Task<List<Employee>> GetListAsync();
        Task<List<Employee>> GetListAsync(int pageNumber, int pageSize);

        Task<Employee> GetByIdAsync(Guid employeeId);

        Task<Guid> InsertAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IRepositoryAsync<Employee> _repository;

        public EmployeeRepository(IRepositoryAsync<Employee> repository)
        {
            _repository = repository;
        }

        public IQueryable<Employee> Employees => _repository.Entities;

        public async Task<Employee> GetByIdAsync(Guid employeeId)
        {
            return await _repository.Entities.Where(p => p.Id == employeeId).FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
        public async Task<List<Employee>> GetListAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetPagedReponseAsync(pageNumber, pageSize);
        }

        public async Task<Guid> InsertAsync(Employee employee)
        {
            employee.InsertedDateTime = DateTime.UtcNow;
            employee.UpdatedDateTime = DateTime.UtcNow;
            await _repository.AddAsync(employee);
            return employee.Id;
        }

        public async Task UpdateAsync(Employee employee)
        {
            employee.UpdatedDateTime = DateTime.UtcNow;
            await _repository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            await _repository.DeleteAsync(employee);
        }
    }
}
