using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product  {
            ProductId = 1,
            ProductName = "Ürün 1",
            Description = "Bu ürün harika!",
            UnitPrice = 19.99m,
            UnitsInStock = 50,
            CreatedAt = DateTime.Now,
            UpdatedAt = null
        },

            new Product
            {
                ProductId = 2,
                ProductName = "Ürün 2",
                Description = "Yüksek kaliteli ürün",
                UnitPrice = 29.99m,
                UnitsInStock = 30,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            },

            new Product
            {
                ProductId = 3,
                ProductName = "Ürün 3",
                Description = "Fırsat ürünü!",
                UnitPrice = 9.99m,
                UnitsInStock = 100,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            }
        };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            productToUpdate.CategoryId = product.CategoryId;
        }
    }
}
