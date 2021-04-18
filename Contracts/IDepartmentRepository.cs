using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Departments> GetDepartments();
        Departments GetDepartmentById(int DepartmentId);
        void InsertDepartment(Departments Department);
        void DeleteDepartment(int DepartmentId);
        void UpdateDepartment(Departments Department);
        void Save();
    }
}
