using FirstDemo.Data;
using FirstDemo.DTO;
using FirstDemo.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FirstDemo.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        AppDbContext context; //new AppDbContext();
        public EmployeeRepository(AppDbContext _context)
        {
            context = _context;
        }

        //Read All
        public List<Employee> GetAll()
        {
            return context.Employees.Include(s => s.Department).ToList();
        }
        //Read One
        public Employee GetByID(int id)
        {
            return context.Employees.Include(s => s.Department).FirstOrDefault(e => e.ID == id);
        }

        public EmployeeDataWithDepartmentNameDTO GetByIdDTO(int id)
        {
            Employee employee = context.Employees.Include(s => s.Department).FirstOrDefault(e => e.ID == id);
            EmployeeDataWithDepartmentNameDTO Emp = new EmployeeDataWithDepartmentNameDTO();
            Emp.DepartmentName = employee.Department.Name;
            Emp.StudentName = employee.Name;
            Emp.Address = employee.Address;
            Emp.ID = employee.ID;
            return Emp;
        }

        public Employee GetByName(string name)
        {
            return context.Employees.FirstOrDefault(s => s.Name == name);
        }
        //Create
        public int Create(Employee employee)
        {
            context.Employees.Add(employee);
            return context.SaveChanges();
        }
        //Update
        public int Update(int id, Employee employee)
        {
            Employee OldEmployee = context.Employees.FirstOrDefault(s => s.ID == id);
            if (OldEmployee != null)
            {
                OldEmployee.Name = employee.Name;
                OldEmployee.Address = employee.Address;
                OldEmployee.Phone = employee.Phone;
                OldEmployee.DepartmentID = employee.DepartmentID;
                return context.SaveChanges();
            }
            return 0;
        }
        //Delete
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
