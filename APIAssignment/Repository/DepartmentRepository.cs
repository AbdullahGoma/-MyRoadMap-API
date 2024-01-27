using APIAssignment.Data;
using APIAssignment.DTO;
using APIAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace APIAssignment.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        AppDbContext context;
        public DepartmentRepository(AppDbContext _context)
        {
            context = _context;
        }
        //Read All
        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }
        //Read One
        public Department GetByID(int id)
        {
            return context.Departments.FirstOrDefault(s => s.ID == id);
        }

        public Department GetByName(string name)
        {
            return context.Departments.FirstOrDefault(s => s.Name == name);
        }

        public DepartmentDetailsWithEmployeeName GetByNameDTO(int id)
        {
            Department department = context.Departments.Include(e => e.Employees).FirstOrDefault(s => s.ID == id);
            DepartmentDetailsWithEmployeeName dep = new DepartmentDetailsWithEmployeeName();
            dep.DepartmentName = department.Name;
            dep.ID = department.ID;
            foreach (var item in department.Employees)
            {
                dep.EmployeesName.Add(item.Name);
            }
            return dep;
        }

        //Create
        public int Create(Department department)
        {
            context.Departments.Add(department);
            return context.SaveChanges();
        }
        //Update
        public int Update(int id, Department department)
        {
            Department OldDepartment = context.Departments.FirstOrDefault(s => s.ID == id);
            if (OldDepartment != null)
            {
                OldDepartment.Name = department.Name;
                OldDepartment.Manager = department.Manager;
                return context.SaveChanges();
            }
            return 0;
        }
        //Delete
        public int Delete(int id)
        {
            Department OldDepartment = context.Departments.FirstOrDefault(s => s.ID == id);
            if (OldDepartment != null)
            {
                try
                {
                    context.Departments.Remove(OldDepartment);
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
