using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Reservation : IEntity
    {
        public int ReservationId { get; set; }
        public int TableId { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
    }
}
