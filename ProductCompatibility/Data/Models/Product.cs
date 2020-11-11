using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ProductCompatibility.Data.Models
{
    public class Product
    {
        [BindNever]
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "Enter product name")]
        [StringLength(15)]
        [Required(ErrorMessage = "Lengh atleast 5 char")]
        public string Name { get; set; }

        [Display(Name = "Enter product short decription")]
        [StringLength(55)]
        [Required(ErrorMessage = "Lengh atleast 5 char")]
        public string Description { get; set; }

        [Display(Name = "Enter product image url")]
        [StringLength(55)]
        [Required(ErrorMessage = "Lengh atleast 5 char")]
        public string Img { get; set; }
        public virtual Category Category { get; set; }
    }
}
