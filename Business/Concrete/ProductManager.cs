﻿using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; 
        IProductCategoryService _productCategoryService;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public ProductManager(IProductDal productDal, IProductCategoryService productCategoryService)
        {
            _productDal = productDal;
            _productCategoryService = productCategoryService;
        }


        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }
        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAllByCategoryId(int id)
        {
            return _productDal.GetAll(p => p.CategoryId == id); 
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice>=max);
        }

        public List<Product> GetByName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.Contains(productName)).ToList();
        }
        public List<Product> GetAllByCategoryName(string categoryName)
        {
            // Kategori ismine göre ürünleri çekmek için kategori servisini kullanabilirsiniz
            var category = this._productCategoryService.GetAllProductCategories().FirstOrDefault(c => c.CategoryName == categoryName);

            if (_productCategoryService != null)
            {
                // Eğer kategori bulunduysa, o kategoriye ait ürünleri çek
                return _productDal.GetAll(p => p.CategoryId == category.CategoryId);
            }

            // Eğer kategori bulunamadıysa, boş bir liste döndür
            return new List<Product>();
        }

      
    }
}
