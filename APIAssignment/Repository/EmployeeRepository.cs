using APIAssignment.Data;
using APIAssignment.DTO;
using APIAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace APIAssignment.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        AppDbContext context;
        public EmployeeRepository(AppDbContext _context)
        {
            context = _context;
        }
        public List<Employee> GetAll()
        {
            return context.Employees.ToList();
        }
        public Employee GetByID(int id)
        {
            return context.Employees.FirstOrDefault(s => s.ID == id);
        }
        public Employee GetByName(string name)
        {
            return context.Employees.FirstOrDefault(s => s.Name == name);
        }


        public EmployeeDataWithDepartmentNameDTO GetByIdDTO(int id)
        {
            Employee employee = context.Employees.Include(s => s.Department).FirstOrDefault(e => e.ID == id);
            EmployeeDataWithDepartmentNameDTO Emp = new EmployeeDataWithDepartmentNameDTO();
            Emp.DepartmentName = employee.Department.Name;
            Emp.StudentName = employee.Name;
            Emp.Address = employee.Address;
            Emp.ID = employee.ID;
            Emp.Salary = employee.Salary;
            return Emp;
        }


        public int Create(Employee employee)
        {
            context.Employees.Add(employee);
            return context.SaveChanges();
        }
        public int Update(int id, Employee employee)
        {
            Employee OldEmployee = context.Employees.FirstOrDefault(s => s.ID == id);
            if (OldEmployee != null)
            {
                OldEmployee.Name = employee.Name;
                OldEmployee.Salary = employee.Salary;
                OldEmployee.Address = employee.Address;
                OldEmployee.Phone = employee.Phone;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Employee OldEmployee = context.Employees.FirstOrDefault(s => s.ID == id);
            if (OldEmployee != null)
            {
                try
                {
                    context.Employees.Remove(OldEmployee);
                    return context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return -1;
                }
            }
            return 0;
        }

    }
}
