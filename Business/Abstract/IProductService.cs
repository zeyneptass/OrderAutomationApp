using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetAllByCategoryId(int id); // kategoriye göre getiren function
        List<Product> GetAllByCategoryName(string categoryName);
        List<Product> GetByName(string productName);

        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        List<Product> GetByUnitPrice(decimal min, decimal max); 
    }
}
