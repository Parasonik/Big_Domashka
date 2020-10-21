using System;
using System.Collections.Generic;
using System.Text;

namespace Big_Domashka
{
    public class OrderItem
    {
        private Guid id;
        public double count { get; set; }
        public Guid productId { get; set; }
        public static OrderItem Get(Guid productid, double count)
        {
            OrderItem Ghost_order = new OrderItem();
            Ghost_order.id = Guid.NewGuid();
            Ghost_order.count = count;
            Ghost_order.productId = productid;
            return Ghost_order;
        }
    }
}
