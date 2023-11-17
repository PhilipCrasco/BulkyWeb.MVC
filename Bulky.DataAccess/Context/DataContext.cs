using Bulky.Models.Model;
using BulkyWeb.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.DataAccess.Context
{
    public class DataContext : IdentityDbContext<IdentityUser> // user for .net Authentication
    {

        public DataContext(DbContextOptions<DataContext>options ) : base(options) { }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }



    }
}
