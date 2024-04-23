using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace ruangou_9.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Customer()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Customer(string name) : this()
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   Id == customer.Id &&
                   Name == customer.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1479869798;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
    public class Goods
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Goods()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Goods(string name, double price) : this()
        {
            Name = name;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            var goods = obj as Goods;
            return goods != null &&
                   Id == goods.Id &&
                   Name == goods.Name;
        }

        public override int GetHashCode()
        {
            var hashCode = 1479869798;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
    public class Order : IComparable<Order>
    {

        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        public string? CustomerName { get => (Customer != null) ? Customer.Name : ""; }

        public DateTime CreateTime { get; set; }

        public List<OrderDetail> Details { get; set; }

        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
            Details = new List<OrderDetail>();
            CreateTime = DateTime.Now;
        }

        public Order(string orderId, Customer customer, List<OrderDetail> items) : this()
        {
            this.OrderId = orderId;
            this.Customer = customer;
            this.Details = items;
        }

        public double TotalPrice
        {
            get => Details.Sum(item => item.TotalPrice);
        }

        public void AddDetail(OrderDetail orderItem)
        {
            if (Details.Contains(orderItem))
                throw new ApplicationException($"添加错误：订单项{orderItem.GoodsName} 已经存在!");
            Details.Add(orderItem);
        }

        public void RemoveDetail(OrderDetail orderItem)
        {
            Details.Remove(orderItem);
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"Id:{OrderId}, customer:{Customer},orderTime:{CreateTime},totalPrice：{TotalPrice}");
            Details.ForEach(od => strBuilder.Append("\n\t" + od));
            return strBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null &&
                   OrderId == order.OrderId;
        }

        public override int GetHashCode()
        {
            var hashCode = -531220479;
            hashCode = hashCode * -1521134295 + OrderId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CustomerName);
            hashCode = hashCode * -1521134295 + CreateTime.GetHashCode();
            return hashCode;
        }

        public int CompareTo(Order other)
        {
            if (other == null) return 1;
            return this.OrderId.CompareTo(other.OrderId);
        }
    }
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Goods> Goods { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail
    {

        public string Id { get; set; }

        public int Index { get; set; } //序号

        public string? GoodsId { get; set; }

        [ForeignKey("GoodsId")]
        public Goods? GoodsItem { get; set; }

        public String? GoodsName { get => GoodsItem != null ? this.GoodsItem.Name : ""; }

        public double? UnitPrice { get => GoodsItem != null ? this.GoodsItem.Price : 0.0; }

        public string? OrderId { get; set; }

        public int Quantity { get; set; }

        public OrderDetail()
        {
            Id = Guid.NewGuid().ToString();
        }

        public OrderDetail(int index, Goods goods, int quantity)
        {
            this.Index = index;
            this.GoodsItem = goods;
            this.Quantity = quantity;
        }

        public double TotalPrice
        {
            get => GoodsItem == null ? 0.0 : GoodsItem.Price * Quantity;
        }

        public override string ToString()
        {
            return $"[No.:{Index},goods:{GoodsName},quantity:{Quantity},totalPrice:{TotalPrice}]";
        }

        public override bool Equals(object obj)
        {
            var item = obj as OrderDetail;
            return item != null &&
                   GoodsName == item.GoodsName;
        }

        public override int GetHashCode()
        {
            var hashCode = -2127770830;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GoodsName);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            return hashCode;
        }
    }
    public class OrderService {

        OrderDbContext dbContext;

        public OrderService(OrderDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public List<Order> GetAllOrders() {
            return dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include(o => o.Customer)
                .ToList<Order>();
        }

        public Order GetOrder(string id) {
            return dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include(o => o.Customer)
                .SingleOrDefault(o => o.OrderId == id);
        }

        public void AddOrder(Order order) {
            FixOrder(order);
            dbContext.Entry(order).State = EntityState.Added;
            dbContext.SaveChanges();
        }

        public void RemoveOrder(string orderId) {
            var order = dbContext.Orders
                .Include(o => o.Details)
                .SingleOrDefault(o => o.OrderId == orderId);
            if (order == null) return;
            dbContext.OrderDetails.RemoveRange(order.Details);
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
        }

        public List<Order> QueryOrdersByGoodsName(string goodsName) {
            var query = dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include(o=> o.Customer)
                .Where(order => order.Details.Any(item => item.GoodsItem.Name == goodsName));
            return query.ToList();
        }

        public List<Order> QueryOrdersByCustomerName(string customerName) {
            return dbContext.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.GoodsItem)
                .Include("Customer")
              .Where(order => order.Customer.Name == customerName)
              .ToList();
        }

        public void UpdateOrder(Order newOrder) {
            RemoveOrder(newOrder.OrderId);
            AddOrder(newOrder);
        }

        //避免级联添加或修改Customer和Goods
        private static void FixOrder(Order newOrder) {
            if (newOrder.Customer != null) {
                newOrder.CustomerId = newOrder.Customer.Id;
            }
            newOrder.Customer = null;
            newOrder.Details.ForEach(d => {
                if (d.GoodsItem != null) {
                    d.GoodsId = d.GoodsItem.Id;
                }
                d.GoodsItem = null;
            });
        }

        public void Export(String fileName) {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(fileName, FileMode.Create)) {
                xs.Serialize(fs, GetAllOrders());
            }
        }

        public void Import(string path) {
            XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(path, FileMode.Open)) {
                List<Order> temp = (List<Order>)xs.Deserialize(fs);
                temp.ForEach(order => {
                    if (dbContext.Orders.SingleOrDefault(o => o.OrderId == order.OrderId) == null) {
                        FixOrder(order);
                        dbContext.Orders.Add(order);
                    }
                });
                dbContext.SaveChanges();
            }
        }

        public object QueryByTotalAmount(float amout) {
            return dbContext.Orders.Include(o => o.Details).ThenInclude(d => d.GoodsItem).Include("Customer")
            .Where(order => order.Details.Sum(d => d.Quantity * d.GoodsItem.Price) > amout)
            .ToList();
        }
    }
}