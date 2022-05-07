using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.DataAccess.Abstract;
using Northwind.Entites.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework;
using Northwind.Business.Abstract;
using System.Data.Entity.Infrastructure;

namespace Northwind.Business.Concrete
{
    public class ProductManager:IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            try
            {
                _productDal.Delete(product);

            }
            catch (DbUpdateException e)
            {

                throw;
            }
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll(); 
        }

        public List<Product> GetProductByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().Contains(productName.ToLower()));
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetAll(p=>categoryId == p.CategoryID);
        }

        public void Update(Product product)
        {
             _productDal.Update(product);
            
        }
    }
}
