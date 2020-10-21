using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Big_Domashka
{

    public class Shop
    {
        private static Shop instance;

        private Shop()
        { }
        public static Shop getInstance()
        {
            if (instance == null)
                instance = new Shop();
            return instance;
        }
        private Guid id = Guid.NewGuid();
        private string name { get; set; }
        private string description { get; set; }
        public List<Product> listOfProducts = new List<Product>();
        
    }
}