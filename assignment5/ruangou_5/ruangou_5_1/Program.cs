using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_5_1
{
    //添加订单、删除订单、修改订单、
    //查询订单（按照订单号、商品名称、客户、订单金额等进行查询）

    //主要的类有Order（订单）、OrderDetails（订单明细），
    //OrderService（订单服务），
    //订单数据可以保存在OrderService中一个List中。
    //在Program里面可以调用OrderService的方法完成各种订单操作

    /*
    使用LINQ语言实现各种查询功能，查询结果按照订单总金额排序返回。
    在订单删除、修改失败时，能够产生异常并显示给客户错误信息。
    作业的订单和订单明细类需要重写Equals方法，确保添加的订单不重复，每个订单的订单明细不重复。
    订单、订单明细、客户、货物等类添加ToString方法，用来显示订单信息。
    OrderService提供排序方法对保存的订单进行排序。默认按照订单号排序，也可以使用Lambda表达式进行自定义排序。
    */
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("请选择操作：");
                Console.WriteLine("1. 添加订单");
                Console.WriteLine("2. 删除订单");
                Console.WriteLine("3. 修改订单");
                Console.WriteLine("4. 查询订单");
                Console.WriteLine("5. 退出");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("无效的选项，请重新输入！");
                    continue;
                }

                switch (input)
                {
                    case "1":
                        // 添加订单逻辑
                        try
                        {
                            Order newOrder = new Order
                            {
                                customer_name = "新客户",
                                date = DateTime.Now.ToString(),
                                orderDetails = new OrderDetails()
                            };
                            // 假设这里添加了一些商品到newOrder.orderDetails.goods中
                            orderService.AddOrder(newOrder);
                            Console.WriteLine("订单添加成功！");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"添加订单时发生错误: {ex.Message}");
                        }
                        break;
                    case "2":                    // 删除订单逻辑

                        try
                        {
                            int orderId = int.Parse(Console.ReadLine());
                            if (orderService.DeleteOrder(orderId))
                            {
                                Console.WriteLine("订单删除成功！");
                            }
                            else
                            {
                                Console.WriteLine("订单不存在或删除失败！");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"删除订单时发生错误: {ex.Message}");
                        }
                        break;
                    case "3":
                        // 修改订单逻辑
                        try
                        {
                            int modifyOrderId = int.Parse(Console.ReadLine());
                            Order modifyOrder = new Order
                            {
                                order_id = modifyOrderId,
                                customer_name = "修改后的客户名",
                                date = DateTime.Now.AddHours(1).ToString(),
                                orderDetails = new OrderDetails()
                            };
                            // 假设这里修改了一些商品到modifyOrder.orderDetails.goods中
                            if (orderService.UpdateOrder(modifyOrder))
                            {
                                Console.WriteLine("订单修改成功！");
                            }
                            else
                            {
                                Console.WriteLine("订单不存在或修改失败！");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"修改订单时发生错误: {ex.Message}");
                        }
                        break;
                    case "4":                    
                        // 查询订单逻辑
                        try
                        {
                            string queryKey = Console.ReadLine();
                            var queryResult = orderService.QueryOrder(queryKey);
                            if (queryResult.Count > 0)
                            {
                                foreach (var order in queryResult)
                                {
                                    Console.WriteLine(order);
                                }
                            }
                            else
                            {
                                Console.WriteLine("没有找到符合条件的订单。");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"查询订单时发生错误: {ex.Message}");
                        }
                        break;
                    case "5":
                        // 退出系统
                        exit = true;
                        Console.WriteLine("感谢使用订单管理系统，再见！");
                        break;
                    default:
                        Console.WriteLine("无效的选项，请重新输入！");
                        break;
                }
            }
        }
    }
        // 订单服务
    class OrderService
    {
        // 存储订单的列表
        private List<Order> orders = new List<Order>();

        // 添加订单，检查订单是否已存在
        public bool AddOrder(Order order)
        {
            var existingOrder = orders.FirstOrDefault(o => o.order_id == order.order_id);
            if (existingOrder != null)
            {
                throw new Exception("订单已存在。");
            }
            orders.Add(order);
            return true;
        }

        // 删除订单，检查订单是否存在
        public bool DeleteOrder(int orderId)
        {
            var order = orders.FirstOrDefault(o => o.order_id == orderId);
            if (order == null)
            {
                throw new Exception("订单不存在。");
            }
            orders.Remove(order);
            return true;
        }

        // 修改订单，检查订单是否存在
        public bool UpdateOrder(Order updatedOrder)
        {
            var order = orders.FirstOrDefault(o => o.order_id == updatedOrder.order_id);
            if (order == null)
            {
                throw new Exception("订单不存在。");
            }
            order.customer_name = updatedOrder.customer_name;
            order.date = updatedOrder.date;
            order.orderDetails = updatedOrder.orderDetails;
            return true;
        }

        // 根据关键字查询订单，使用LINQ
        public List<Order> QueryOrder(string key)
        {
            return orders.AsQueryable()
                .Where(o => o.customer_name.Contains(key) || o.date.Contains(key))
                .OrderBy(o => o.orderDetails.total_money) // 按照订单总金额排序
                .ToList();
        }

        // 对保存的订单进行排序
        public void SortOrders(Func<Order, object> sortExpression)
        {
            orders = orders.OrderBy(sortExpression).ToList();
        }
    }

    // 订单
    class Order : IEquatable<Order>
    {
        public int order_id { get; set; }
        public string customer_name { get; set; }
        public string date { get; set; }
        public OrderDetails orderDetails { get; set; }

        // 重写Equals方法，确保订单不重复
        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }

        public bool Equals(Order other)
        {
            return other != null && order_id == other.order_id;
        }

        public override int GetHashCode()
        {
            return order_id.GetHashCode();
        }

        // 重写ToString方法，显示订单信息
        public override string ToString()
        {
            return $"订单号: {order_id}, 客户: {customer_name}, 日期: {date}, 订单详情: {orderDetails}";
        }
    }
    // 订单明细
    class OrderDetails : IEquatable<OrderDetails>
    {
        public List<Goods> goods { get; set; } = new List<Goods>();
        public int total_money { get; set; }        // 计算总金额
        public void CalculateTotalMoney()
        {
            total_money = goods.Sum(g => g.goods_money);
        }
        // 重写Equals方法，确保订单明细不重复
        public override bool Equals(object obj)
        {
            return Equals(obj as OrderDetails);
        }
        public bool Equals(OrderDetails other)
        {
            return other != null && goods.SequenceEqual(other.goods);
        }
        public override int GetHashCode()
        {
            return goods.Aggregate(0, (current, g) => current ^ g.GetHashCode());
        }
        // 重写ToString方法，显示订单明细信息
        public override string ToString()
        {
            return $"总金额: {total_money}, 商品列表: {string.Join(", ", goods.Select(g => g.ToString()))}";
        }
    }

    // 货品
    class Goods : IEquatable<Goods>
    {
        public string goods_name { get; set; }
        public int goods_num { get; set; }
        public int goods_money { get; set; }

        // 重写Equals方法，确保每个订单的订单明细中的货物不重复
        public override bool Equals(object obj)
        {
            return Equals(obj as Goods);
        }

        public bool Equals(Goods other)
        {
            return other != null && goods_name == other.goods_name
                    && goods_num == other.goods_num
                    && goods_money == other.goods_money;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + goods_name.GetHashCode();
            hash = hash * 23 + goods_num.GetHashCode();
            hash = hash * 23 + goods_money.GetHashCode();
            return hash;
        }

        // 重写ToString方法，显示货物信息
        public override string ToString()
        {
            return $"商品名: {goods_name}, 数量: {goods_num}, 单价: {goods_money}";
        }
    }
}


