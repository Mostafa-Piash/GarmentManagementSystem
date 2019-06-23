using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Entity
{
    public class OrderList
    {
        public int OrderId { get; set; }
        public string OrderItemType { get; set; }
        public int OrderQuantity { get; set; }
        public string OrderStatus { get; set; }

    }
}
