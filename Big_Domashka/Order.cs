using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Big_Domashka
{
    public class Order
    {
        public Guid id;
        public DateTime dateOfOrder;
        public Guid buyerId { get; set; }
        public List<OrderItem> listOfOrderItems = new List<OrderItem>();
        public  Order(Guid buyerId1, List<OrderItem> orderItems)
        {
            id = Guid.NewGuid();
            dateOfOrder = DateTime.Now;
            buyerId = buyerId1;
            listOfOrderItems = orderItems;
        }

    }
}
