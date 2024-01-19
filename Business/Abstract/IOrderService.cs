﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {

        List<Order> GetAll();
        List<Order> GetAllOrderByProductId(int productId);
        Order GetByProductId(int productId);
        List<Order> GetOrderById(int orderId);
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}
