using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductCategoryManager : IProductCategoryService
    {
        ICategoryDal _categoryDal;

        public ProductCategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }


        public Category GetProductCategoryById(int categoryId)
        {
            return _categoryDal.GetAll().FirstOrDefault(c => c.CategoryId == categoryId);
        }
    }
}
