using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.Data.Models
{
    public class Order
    {
        [BindNever]
        public int ID { get; set; }

        [Display(Name = "Enter name")]
        [StringLength(15)]
        [Required(ErrorMessage ="Lengh atleast 5 char")]
        public string Name { get; set; }

        [Display(Name = "Enter surname")]
        [StringLength(25)]
        [Required(ErrorMessage = "Lengh atleast 5 char")]
        public string SurName { get; set; }

        [Display(Name = "Enter address")]
        [StringLength(25)]
        [Required(ErrorMessage = "Lengh atleast 5 char")]
        public string Address{ get; set; }

        [Display(Name = "Enter name")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Uncorrect email")]
        public string Email{ get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime{ get; set; }
        public List<OrderDetail> OrderDetails{ get; set; }
    }
}
