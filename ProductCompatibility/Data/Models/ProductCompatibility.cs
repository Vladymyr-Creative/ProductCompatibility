using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ProductCompatibility.Data.Models
{
    public class ProductsCompatibility
    {
        public int ID { get; set; }
        public int CompatibilityID { get; set; }                
        public int Product1ID { get; set; }                
        public int Product2ID { get; set; }
        public virtual Compatibility Compatibility { get; set; }
        public virtual Product Product1 { get; set; }
        public virtual Product Product2 { get; set; }        
    }
}
/*
 
 */
