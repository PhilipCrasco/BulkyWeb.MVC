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
            var objFromDb = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if(product != null )
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Author = product.Author;  
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price500 = product.Price500;
                objFromDb.CategoryId = product.CategoryId;
                if(objFromDb.ImageUrl != null )
                {
                    objFromDb.ImageUrl = objFromDb.ImageUrl;
                }

            }
        }
    }

}
