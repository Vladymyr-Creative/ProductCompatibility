using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductCompatibility.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCompatibility.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Categery is required!")]
        public int Cat { get; set; }

        [Display(Name = "Name")]
        [StringLength(25, ErrorMessage ="Name is too long!")]
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Display(Name = "Description")]        
        [Required(ErrorMessage = "Decription is required!")]
        public string Desc { get; set; }

        [DisplayName("Upload Image")]        
        public IFormFile Img { get; set; }
    }
}
