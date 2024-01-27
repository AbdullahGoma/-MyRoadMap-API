using APIAssignment.DTO;
using APIAssignment.Models;

namespace APIAssignment.Repository
{
    public interface IEmployeeRepository
    {
        int Create(Employee employee);
        int Delete(int id);
        List<Employee> GetAll();
        Employee GetByID(int id);
        Employee GetByName(string name);
        EmployeeDataWithDepartmentNameDTO GetByIdDTO(int id);
        int Update(int id, Employee employee);
    }
}