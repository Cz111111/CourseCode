using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_5_1
{
    public class Order : IComparable<Order>
    {
        
        public int Id { get; set; }

        private List<OrderDetail> details = new List<OrderDetail>();

        private Customer Customer { get; set; }
        public string CustomerName { get => (Customer != null) ? Customer.Name : ""; }

        public DateTime Time { get; }
        public List<OrderDetail> Details
        {
            get { return details; }
        }

        public float TotalPrice
        {
            get => Details.Sum(d => d.TotalPrice);
        }

        public Order()
        {
            Time = DateTime.Now;
        }

        public Order(int orderId, Customer customer)
        {
            Id = orderId;
            Customer = customer;
            Time = DateTime.Now;
        }

        public List<OrderDetail> GetOrderDetails()
        {
            return Details;
        }

        public void AddDetails(OrderDetail orderDetail)
        {
            if (Details.Contains(orderDetail))
            {
                Console.WriteLine($"The good ({orderDetail.GoodName}) already exists in order {Id}");
                return;

            }
            Details.Add(orderDetail);
        }
        public void RemoveDetails(int num)
        {
            Details.RemoveAt(num);
        }

        public int CompareTo(Order order)
        {
            return (order == null) ? 1 : this.Id.CompareTo(order.Id);
        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null && Id == order.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append($"orderId:{Id}, customer:({Customer}),date:{Time}");
            Details.ForEach(detail => result.Append("\n\t" + detail));
            return result.ToString();
        }
    }
}
