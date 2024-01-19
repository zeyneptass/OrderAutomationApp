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
    public class ReservationManager : IReservationService
    {
        IRezervationDal _reservationDal;

        public ReservationManager(IRezervationDal reservationDal)
        {
            _reservationDal = reservationDal;
        }

        public void Add(Reservation reservation)
        {
            _reservationDal.Add(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _reservationDal.Delete(reservation);
        }

        public List<Reservation> GetAll()
        {
            return _reservationDal.GetAll();
        }

        public List<Reservation> GetByCustomerName(string customerName)
        {
            return _reservationDal.GetAll(r => r.CustomerName == customerName).ToList();
        }

        public List<Reservation> GetByTableId(int tableId)
        {
            return _reservationDal.GetAll(r => r.TableId == tableId).ToList();
        }

        public List<Reservation> GetDetails(string customerName, int tableId, DateTime date)
        {
            return _reservationDal.GetAll(r => r.CustomerName == customerName && r.TableId == tableId &&
                                          r.ReservationDate.Date == date.Date).ToList();
        }

        public void Update(Reservation reservation)
        {
            _reservationDal.Update(reservation);
        }
    }
}
