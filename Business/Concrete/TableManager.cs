using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Concrete
{
    public class TableManager : ITableService
    {
        ITableDal _tableDal;

        public TableManager(ITableDal tableDal)
        {
            _tableDal = tableDal;
        }

        public List<Entities.Concrete.Table> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Entities.Concrete.Table> GetByTableId(int tableId)
        {
            return _tableDal.GetAll(t  => t.TableId == tableId);
        }
    }
}
