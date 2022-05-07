using Northwind.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.DataAccess.Concrete.EntityFramework;

namespace Northwind.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetProductsByCategory(int categoryID);

        List<Product> GetProductByProductName(string text);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
