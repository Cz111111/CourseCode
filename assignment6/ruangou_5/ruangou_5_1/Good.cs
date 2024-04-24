using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_5_1
{
    public class Good
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public Good() { }

        public Good(int id, string name, float price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public override bool Equals(object obj)
        {
            var goods = obj as Good;
            return goods != null && this.Id == goods.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Id:{Id}, Name:{Name}, Value:{Price}";
        }
    }
}
