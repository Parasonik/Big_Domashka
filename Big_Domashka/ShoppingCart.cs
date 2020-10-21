using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Big_Domashka
{
    class ShoppingCart
    {

        public Guid id;
        private byte capacity = 50; // зачем оно нужно если можно сразу поставить предел в листе
        public List<OrderItem> listOfOrderItems = new List<OrderItem>();
    }
}
