using FirstDemo.DTO;
using FirstDemo.Model;

namespace FirstDemo.Repository
{
    public interface IEmployeeRepository
    {
        int Create(Employee employee);
        int Delete(int id);
        List<Employee> GetAll();
        Employee GetByID(int id);
        EmployeeDataWithDepartmentNameDTO GetByIdDTO(int id);
        Employee GetByName(string name);
        int Update(int id, Employee employee);
    }
}