using Bulky.Models.Model;
using BulkyWeb.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.DataAccess.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext>options ) : base(options) { }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }


    }
}
