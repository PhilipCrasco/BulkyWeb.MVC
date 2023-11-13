using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.DataAccess.Context;
using BulkyWeb.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DataContext _context;

        public ICategoryRepository Category { get; set; }

        public IProductsRepository Products { get; set; }

        public UnitofWork(DataContext context)
        {
            _context = context;

            Category = new CategoryRepository(_context);
            Products = new ProductRepository(_context);
            
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
