using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employees> GetEmployees();
        Employees GetEmployeeById(int EmployeeId);
        void InsertEmployee(Employees Employee);
        void DeleteEmployee(int EmployeeId);
        void UpdateEmployee(Employees Employee);
        void Save();
    }
}
