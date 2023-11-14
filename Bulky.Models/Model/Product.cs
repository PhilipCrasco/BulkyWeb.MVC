using BulkyWeb.Models.Model;
using BulkyWeb.MVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.Model
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "List Price")]
        [Range(1 , 1000)]
        public decimal ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public decimal Price { get; set; }


        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public decimal Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 500+")]
        [Range(1, 1000)]
        public decimal Price500 { get; set; }

        [ValidateNever]
        public virtual Category Category { get; set; }


        [ForeignKey("CategoryId")]
        public int  CategoryId { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
