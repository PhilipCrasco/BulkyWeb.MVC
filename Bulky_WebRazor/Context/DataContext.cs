using Bulky_WebRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky_WebRazor.Context
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Category> Categories { get; set; }

    }
}
