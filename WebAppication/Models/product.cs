namespace WebAppication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        public int id { get; set; }

        public int Total { get; set; }

        public string Description { get; set; }

        public int? CAteId { get; set; }

        public int Tacked { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int? Available { get; set; }

        [Required]
        public string ProductName { get; set; }

        public int currentNO { get; set; }

        public virtual Category Category { get; set; }
    }
}
