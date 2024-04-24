using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_5_1
{
    public class OrderService
    {
        private List<Order> orders = new List<Order>();

        public OrderService() {}
        //添加订单、删除订单、修改订单、查询订单（按照订单号、商品名称、客户、订单金额等进行查询）

        //添加订单
        public void AddOrder(Order order)
        {
            if (orders.Contains(order))
            {
                Console.WriteLine($"the order ({order.Id}) already exists!");
                return;
            }
            orders.Add(order);
        }

        //根据Id删除订单
        public void RemoveOrder(int orderId)
        {
            orders.RemoveAll(o => o.Id == orderId);
        }

        //更新订单
        public void Update(Order order)
        {
            int index = orders.FindIndex(o => o.Id == order.Id);
            if (index < 0)
            {
                Console.WriteLine($"the order ({order.Id}) doesn't exist!");
                return;
            }
            orders.RemoveAt(index);
            orders.Add(order);
        }

        //根据Id查询订单
        public Order QueryById(int orderId)
        {
            return orders.FirstOrDefault(o => o.Id == orderId);
        }

        //查询所有订单
        public List<Order> QueryAll()
        {
            return orders;
        }

        //根据商品名称查询
        public List<Order> QueryByGoodsName(string goodsName)
        {
            var query = orders
                .Where(o => o.Details.Any(d => d.Good.Name == goodsName))
                .OrderBy(o => o.TotalPrice);
            return query.ToList();
        }

        //根据客户名查询
        public List<Order> QueryByCustomerName(string customerName)
        {
            var query = orders
                .Where(o => o.Customer.Name == customerName)
                .OrderBy(o => o.TotalPrice);
            return query.ToList();
        }

        //根据订单金额查询
        public List<Order> QueryByTotalPrice(float totalPrice)
        {
            var query = orders
                .Where(o => o.TotalPrice >= totalPrice)
                .OrderBy(o => o.TotalPrice);
            return query.ToList();
        }
        //对orders中的数据进行排序
        public void Sort(Comparison<Order> comparison)
        {
            orders.Sort(comparison);
        }
    }
}
