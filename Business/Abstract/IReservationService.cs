using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IReservationService
    {
        List<Reservation> GetAll();
        List<Reservation> GetByCustomerName(string customerName);
        List<Reservation> GetDetails(string customerName,int tableId,DateTime date);
        List<Reservation> GetByTableId(int  tableId);

        void Add(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(Reservation reservation);
    }
}
