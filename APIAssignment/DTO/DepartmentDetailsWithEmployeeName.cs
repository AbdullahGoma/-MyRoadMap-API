namespace APIAssignment.DTO
{
    public class DepartmentDetailsWithEmployeeName
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public List<string> EmployeesName { get; set; } = new List<string>();
    }
}
