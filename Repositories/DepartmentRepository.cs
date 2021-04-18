using Contracts;
using Database;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MicroservicesContext _dbContext;

        public DepartmentRepository(MicroservicesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteDepartment(int DepartmentId)
        {
            var dep = _dbContext.Departments.Find(DepartmentId);
            _dbContext.Departments.Remove(dep);
            Save();
        }

        public Departments GetDepartmentById(int DepartmentId)
        {
            return _dbContext.Departments.Find(DepartmentId);
        }

        public IEnumerable<Departments> GetDepartments()
        {
            return _dbContext.Departments.ToList();
        }

        public void InsertDepartment(Departments Department)
        {
            _dbContext.Add(Department);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateDepartment(Departments Department)
        {
            _dbContext.Entry(Department).State = EntityState.Modified;
            Save();
        }
    }
}
