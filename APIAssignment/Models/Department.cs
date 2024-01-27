﻿using System.ComponentModel.DataAnnotations;

namespace APIAssignment.Models
{
    public class Department
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Manager { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
