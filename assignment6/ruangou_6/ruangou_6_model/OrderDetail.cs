using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_5_1
{
    public class OrderDetail
    {
        private Good Good { get; set; }

        public string GoodName { get => Good != null ? this.Good.Name : ""; }
        public float UnitPrice { get => Good != null ? this.Good.Price : 0.0f; }
        public int Count { get; set; }

        public float TotalPrice
        {
            get => Good.Price * Count;
        }

        public OrderDetail() { }

        public OrderDetail(Good good, int count)
        {
            this.Good = good;
            this.Count = count;
        }

        public override bool Equals(object obj)
        {
            var orderdetail = obj as OrderDetail;
            return orderdetail != null && this.GoodName.Equals(orderdetail.GoodName);
        }

        public override int GetHashCode()
        {
            return 785010553 + Good.GetHashCode();
        }

        public override string ToString()
        {
            return $"Good({Good}),Count:{Count},TotalPrice:{TotalPrice}";
        }
    }
}
