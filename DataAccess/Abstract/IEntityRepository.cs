using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    // Generic constraint: Generic kısıt 
    // aşağıdaki class keywordü referans tip olabileceği anlamına gelir
    // herhangi bir clas değil veritabanı class'larını yazsın IEntity veya IEntitiy refrens alan class'lar tek yazılabilr
    //  new() : new'lenebilr demek Entity bir interface ve interface'ler new'lenemediği için IEntitiy yazmamızı da engellmiş oluruz yani sadece IEntity implemente eden class'lar yazılabilir interface'lerde T yerine
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
