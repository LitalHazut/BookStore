using BookStore.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class CartProduct
    {
        public IDataEntity Product { get; set; }
        public int Amount { get; set; }
        public CartProduct(IDataEntity product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        
    }
}
