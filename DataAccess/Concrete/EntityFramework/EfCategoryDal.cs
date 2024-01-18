using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category>, ICategoryDal
    {
        //public List<Category> GetAll()
        //{
        //    using (CafeContext context = new CafeContext())
        //    {
        //        return context.Set<Category>().ToList();
        //    }
        //}
    }
}
