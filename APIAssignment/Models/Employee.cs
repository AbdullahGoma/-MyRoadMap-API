using System.Text.Json.Serialization;

namespace APIAssignment.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int DepartmentID { get; set; }
        [JsonIgnore]
        public Department? Department { get; set; }
    }
}
