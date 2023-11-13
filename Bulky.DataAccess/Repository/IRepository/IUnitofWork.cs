using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IUnitofWork 
    { 

        public ICategoryRepository Category {get;}

        public IProductsRepository Products { get;}

 
        void Save();

    }
}
