using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Table : IEntity
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
    }
}
