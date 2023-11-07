﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bulky_WebRazor.Models
{
    public class Category : BaseEntity
    {
        [Required]
        [DisplayName("Category Name")] // Data Anotation
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int Display_Order { get; set; }
    }
}
