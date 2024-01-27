using FirstDemo.DTO;
using FirstDemo.Model;

namespace FirstDemo.Repository
{
    public interface IDepartmentRepository
    {
        int Create(Department department);
        int Delete(int id);
        List<Department> GetAll();
        Department GetByID(int id);
        Department GetByName(string name);
        DepartmentDetailsWithEmployeeName GetByNameDTO(int id);
        int Update(int id, Department department);
    }
}