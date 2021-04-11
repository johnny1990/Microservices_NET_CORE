
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MicroservicesContext _dbContext;

        public EmployeeRepository(MicroservicesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteEmployee(int EmployeeId)
        {
            var emp = _dbContext.Employees.Find(EmployeeId);
            _dbContext.Employees.Remove(emp);
            Save();
        }

        public Employees GetEmployeeById(int EmployeeId)
        {
            return _dbContext.Employees.Find(EmployeeId);
        }

        public IEnumerable<Employees> GetEmployees()
        {
            return _dbContext.Employees.ToList();
        }

        public void InsertEmployee(Employees Employee)
        {
            _dbContext.Add(Employee);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateEmployee(Employees Employee)
        {
            _dbContext.Entry(Employee).State = EntityState.Modified;
            Save();
        }
    }
}
