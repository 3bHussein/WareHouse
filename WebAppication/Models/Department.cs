namespace WebAppication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string EmployeeName { get; set; }

        public string Desription { get; set; }
    }
}
