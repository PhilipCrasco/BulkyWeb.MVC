using BulkyWeb.Models.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.MVC.Models
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
