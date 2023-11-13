using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Model;
using BulkyWeb.DataAccess.Context;
using BulkyWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductsRepository
    {

        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }

}
