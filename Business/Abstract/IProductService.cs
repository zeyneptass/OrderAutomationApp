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
        Product GetProductByName(string productName);
        Product GetByUnitPrice(decimal unitPrice);
        Product GetById(int productId);
        List<Product> GetAllByUnitPrice(decimal unitPrice);
        List<Product> GetAll();
        List<Product> GetAllByCategoryId(int id); // kategoriye göre getiren function
        List<Product> GetAllByCategoryName(string categoryName);
        List<Product> GetByName(string productName);
        List<Product> GetAllUnitPrice(decimal unitPrice); 

        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        
    }
}
