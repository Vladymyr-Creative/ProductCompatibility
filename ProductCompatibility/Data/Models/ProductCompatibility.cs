using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProductCompatibility.Data.Models
{
    public class ProductsCompatibility
    {
        public int Id { get; set; }
        [Required]
        public int CompatibilityId { get; set; }
        [Required]
        public int Product1Id { get; set; }
        [Required]
        public int Product2Id { get; set; }

        public virtual Compatibility Compatibility { get; set; }
        public virtual Product Product1 { get; set; }
        public virtual Product Product2 { get; set; }        
    }
}