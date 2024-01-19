using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {

        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }


        public void Add(Order order)
        {
            _orderDal.Add(order);
        }


        public void Delete(Order order)
        {
            _orderDal.Delete(order);
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrderByProductId(int productId)
        {
            return _orderDal.GetAll(o => o.ProductId == productId);
        }

        public Order GetByProductId(int productId)
        {
            return _orderDal.Get(o => o.ProductId == productId);
        }

        public List<Order> GetOrderById(int orderId)
        {
            return _orderDal.GetAll(o =>o.OrderId == orderId);
        }

        public void Update(Order order)
        {
            _orderDal.Update(order);
        }


    }
}
