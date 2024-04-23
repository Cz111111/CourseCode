using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //添加订单
            using (var context = new OrderContext())
            {
                var order = new Order { OrderId = "1", CreateTime = DateTime.Now };
                var customer = new Customer { CustomerId = "1", CustomerName = "A" };
                var exist = context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
                if (exist == null)
                    order.Customer = customer;

                context.Orders.Add(order);
                context.SaveChanges();
                Console.WriteLine("添加订单：" + order.OrderId);
            }
            using (var context = new OrderContext())
            {
                var order = new Order { OrderId = "2", CreateTime = DateTime.Now };
                order.Customer = new Customer { CustomerId = "2", CustomerName = "B" };

                context.Orders.Add(order);
                context.SaveChanges();
                Console.WriteLine("添加订单：" + order.OrderId);
            }

            //根据ID查找订单
            using (var context = new OrderContext())
            {
                var order = context.Orders.Include("Customer")
                    .SingleOrDefault(o => o.OrderId == "1");
                if (order != null) { Console.WriteLine($"编号1的订单: " + order.ToString()); }
            }

            //修改订单
            using (var context = new OrderContext())
            {
                var order = context.Orders.Include("Customer").FirstOrDefault(o => o.OrderId == "2");
                if (order != null)
                {
                    order.Customer.CustomerName = "C";
                }
                context.SaveChanges();
            }

            //删除订单
            using (var context = new OrderContext())
            {
                var order = context.Orders.Include("Customer").FirstOrDefault(o => o.OrderId == "2");
                if (order != null) context.Orders.Remove(order);
                context.SaveChanges();
            }
        }
    }
    public class Order
    {

        [Key]
        public string OrderId { get; set; }
        [Required]
        public virtual Customer Customer { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }



        public override string ToString()
        {
            return $"【订单编号】{this.OrderId} 【顾客姓名】{this.Customer.CustomerName} 【创建时间】{this.CreateTime} ";
        }


    }

    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }

    }
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class OrderContext : DbContext
    {
        public OrderContext() : base("OrderDataBase")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<OrderContext>());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

    }
}
