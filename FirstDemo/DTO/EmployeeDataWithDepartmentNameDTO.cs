namespace FirstDemo.DTO
{
    // Hide Model => Secure Model Structure & Solve Serialization Problem Cycle
    public class EmployeeDataWithDepartmentNameDTO
    {
        public int ID { get; set; }
        public string StudentName { get; set; }
        public string Address { get; set; }
        public string DepartmentName { get; set; }
    }
}
