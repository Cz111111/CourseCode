using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_5_1
{
    public class Program
    {
        public static void Main()
        {
            //商品
            Good pen = new Good(1, "pen", 3.99f);
            Good pencil = new Good(2, "pencil", 1.99f);
            Good eraser = new Good(3, "eraser", 5.99f);
            Good ruler = new Good(4, "ruler", 2.99f);

            //顾客
            Customer jack = new Customer(1, "Jack");
            Customer john = new Customer(2, "John");
            Customer joe = new Customer(3, "Joe");

            //订单
            Order order1 = new Order(1, jack);
            order1.AddDetails(new OrderDetail(pen, 10));
            order1.AddDetails(new OrderDetail(eraser, 2));
            order1.AddDetails(new OrderDetail(ruler, 1));

            Order order2 = new Order(2, john);
            order2.AddDetails(new OrderDetail(pencil, 3));
            order2.AddDetails(new OrderDetail(pen, 1));
            order2.AddDetails(new OrderDetail(ruler, 4));

            Order order3 = new Order(3, joe);
            order3.AddDetails(new OrderDetail(ruler, 8));
            order3.AddDetails(new OrderDetail(pencil, 5));
            order3.AddDetails(new OrderDetail(eraser, 2));


            OrderService orderService = new OrderService();
            orderService.AddOrder(order1);
            orderService.AddOrder(order2);
            orderService.AddOrder(order3);

            //查询
            Console.WriteLine("--------------------QueryById:1--------------------");
            Console.WriteLine(orderService.QueryById(1));

            Console.WriteLine("\n");
            Console.WriteLine("--------------------GetAllOrders--------------------");
            List<Order> orders = orderService.QueryAll();
            orders.ForEach(o => Console.WriteLine(o));


            Console.WriteLine("\n");
            Console.WriteLine("--------------------GetOrdersByCustomerName:'Joe'--------------------");
            orders = orderService.QueryByCustomerName("Joe");
            orders.ForEach(o => Console.WriteLine(o));


            Console.WriteLine("\n");
            Console.WriteLine("--------------------GetOrdersByGoodsName:'pen'--------------------");
            orders = orderService.QueryByGoodsName("pen");
            orders.ForEach(o => Console.WriteLine(o));


            Console.WriteLine("\n");
            Console.WriteLine("--------------------GetOrdersTotalAmount:54.87--------------------");
            orders = orderService.QueryByTotalPrice(54.87f);
            orders.ForEach(o => Console.WriteLine(o));

            Console.WriteLine("\n");
            Console.WriteLine("--------------------order by Amount--------------------");
            orderService.Sort(
              (o1, o2) => o2.TotalPrice.CompareTo(o1.TotalPrice));
            orderService.QueryAll().ForEach(
                o => Console.WriteLine(o));

            Console.WriteLine("\n");
            Console.WriteLine("--------------------Remove order(id=2) and qurey all--------------------");
            orderService.RemoveOrder(2);
            orderService.QueryAll().ForEach(
                o => Console.WriteLine(o));

        }
    }
}


