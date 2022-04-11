using BookStore.Model;
using BookStore.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.UnitTest
{
    [TestClass]
    public class TestProductsService
    {
        readonly ProductsService productsService = new ProductsService();
        List<CartProduct> cartProducts;
        CartProduct cartProduct1;
        CartProduct cartProduct3;
        Book book1;
        Book book2;
        Book book3;
        List<string> list;
        public TestProductsService()
        {
            
            cartProducts = new List<CartProduct>();
            List<string> list = new List<string>
            {
                "Drama",
                "Action",
            };
          
        }
        [TestMethod]
        public void TestAddProuductValuesValid()
        { 
            //Check the Count Of Product
            book1 = new Book("book1", "lital hazut", new DateTime(2000, 1, 1), 30, 5, list, "lilililililili");
            book2 = new Book("simba", "Ariel", new DateTime(2001, 7, 7), 28, 8, list, "sss");
            int beforeCount = productsService.GetAllProuduct().ToList().Count;
            productsService.AddProuduct(book1);
            productsService.AddProuduct(book2);
            Assert.IsTrue(productsService.GetAllProuduct().ToList().Count == beforeCount + 2);
        }

        [TestMethod]
        public void TestAddProductDuplicatesValid()
        {
            //Check if have sam product
            book2 = new Book("sos", "Ariel", new DateTime(2001, 7, 7), 28, 8, list, "sss");
            book3 = new Book("sos", "Ariel", new DateTime(2001, 7, 7), 28, 8, list, "sss");

            int countBefore = productsService.GetAllProuduct().ToList().Count;
            productsService.AddProuduct(book2);
            productsService.AddProuduct(book3);

            Assert.IsTrue(productsService.GetAllProuduct().ToList().Count == countBefore + 1);
        }

        [TestMethod]
        public void TestAddProductToQuantityValid()
        {
            //Check the wuantity of product
            book3 = new Book("book5", "Ariel", new DateTime(2001, 7, 7), 28, 8, list, "sss");

            productsService.AddProuduct(book3); //Quantity(8)
            productsService.AddProuduct(book3); //Quantity(8)

            // Quantity(8)+Quantity(8)= Total (16)
            Assert.IsTrue(book3.Quantity == 16);
        }
        [TestMethod]
        public void TestBuyAllProductsUpdatedCart()
        {
            //Check if i buy all cart 
            book1 = new Book("test1", "lital hazut", new DateTime(2000, 1, 1), 30, 5, list, "lilililililili");
            cartProduct1 = new CartProduct(book1, 1);
            book3 = new Book("test3", "Ariel", new DateTime(2001, 7, 7), 28, 8, list, "sss");
            cartProduct3 = new CartProduct(book3, 2);

            productsService.AddProuduct(book1);
            productsService.AddProuduct(book3);
            productsService.AddProudctToList(cartProducts, cartProduct1);
            productsService.AddProudctToList(cartProducts, cartProduct3);
            productsService.BuyAllProducts(cartProducts);

            Assert.IsTrue(cartProducts.Count==0);
        }
        [TestMethod]
        public void TestQuantityOfProductAfterBuy()
        { 
            //How much products remained after buy
            book1 = new Book("testi", "lital hazut", new DateTime(2000, 1, 1), 30, 5, list, "lilililililili");
            cartProduct1 = new CartProduct(book1, 1);
            productsService.AddProuduct(book1);
            productsService.AddProudctToList(cartProducts, cartProduct1);
            productsService.BuyAllProducts(cartProducts);

            Assert.IsTrue(book1.Quantity == 4);
        }
    }
}




