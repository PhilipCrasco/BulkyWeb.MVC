using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.MVC.Models
{
    public class Category : BaseEntity
    {
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        public int Display_Order { get; set; }


    }
}
