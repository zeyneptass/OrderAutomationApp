using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; } // Stok miktarı
        public DateTime CreatedAt { get; set; } // Ürünün oluşturulma tarihi
        public DateTime? UpdatedAt { get; set; } // Ürünün son güncellenme tarihi (Opsiyonel, null olabilir)
    }
}
