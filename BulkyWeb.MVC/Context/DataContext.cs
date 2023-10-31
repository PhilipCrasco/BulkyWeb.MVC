using BulkyWeb.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.MVC.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext>options ) : base(options) { }

        public virtual DbSet<Category> Categories { get; set; }


    }
}
