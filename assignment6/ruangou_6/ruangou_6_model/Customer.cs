using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_5_1
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Customer() { }

        public Customer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null && Id == customer.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
        public override string ToString()
        {
            return $"customerId:{Id}, customerName:{Name}";
        }
    }
}
