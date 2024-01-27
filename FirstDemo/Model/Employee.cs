namespace FirstDemo.Model
{
    // Resource
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } 
        public string Phone { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
