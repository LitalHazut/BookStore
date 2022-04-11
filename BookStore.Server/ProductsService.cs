using BookStore.Data;
using BookStore.Data.Interfaces;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Server
{
    public class ProductsService
    {
        IRepository<ProductBase> repository;
        public List<string> Types { get; set; } 
        public List<string> BookGenre { get; set; }
        public List<string> JournalGenre { get; set; }
        public List<string> JournalFrequency { get; set; }
       
        public ProductsService()
        {
            repository = RepositoryFactory.GetProductRepository();
            AddTypes();
            AddBookGeners();
            AddJournalGeners();
            AddJournalFrequency();
        }

        private void AddTypes()
        {
            Types = new List<string>
            {
                "Book",
                "Journal",
            };
        }
        private void AddBookGeners()
        {
            BookGenre = new List<string>
            {
              "Drama",
              "Action",
              "Romance",
              "Fiction",
              "Comedy",

            };
        }
        private void AddJournalGeners()
        {
            JournalGenre = new List<string>
            {
                "Science",
                "Law",
                "Fashion",
                "Economy",
                "Nature",
                "News"
            };
        }
        private void AddJournalFrequency()
        {
            JournalFrequency = new List<string>
            {
              "Daily",
              "Weekly",
              "ByWeekly",
              "Monthly",
              "BiMonthly",
              "Quarterly",
              "Annually"
            };
        }
        public void AddBook(string title, string authorName, DateTime publicationDate, decimal basePrice, int quantity
           , List<string> genres, string synopsis, int edition = 1)
        {
            var book = new Book(title, authorName, publicationDate, basePrice, quantity, genres, synopsis, edition);
            AddProuduct(book);
        }
        public void AddJournal(string name, string editorName, DateTime publicationDate,
            decimal basePrice, int quantity, int issueNumber, string frequency, List<string> genres)
        {
            var journal = new Journal(name, editorName, publicationDate, basePrice, quantity, issueNumber, frequency, genres);
            AddProuduct(journal);
        }
        public void RemoveProduct(Guid id)
        {
            repository.Delete(id);
        }
        public void AddProuduct(ProductBase productBase)
        {
            repository.Insert(productBase);
        }
        private void BuyProduct(CartProduct cartProduct)
        {
            repository.Get(cartProduct.Product.Id).Quantity -= cartProduct.Amount;
        }
        public void BuyAllProducts(List<CartProduct> prouductList)
        {
            prouductList.ForEach(p =>
            {
                BuyProduct(p);
            });

            prouductList.Clear();
        }
        public void AddProudctToList(List<CartProduct> prouductList, CartProduct product)
        {
            var prouductInCart = IsInCart(prouductList, product.Product.Id);
            if (prouductInCart == null) prouductList.Add(product);
            else
            {
                prouductInCart.Amount += product.Amount;
            }
        }
        public int AmountOfProductInCart(List<CartProduct> prouductList, ProductBase product)
        {
            var prouductInCart = IsInCart(prouductList, product.Id);
            if (prouductInCart != null) return prouductInCart.Amount;
            else return 0;
        }
        private CartProduct IsInCart(List<CartProduct> prouductList, Guid id)
        {
            return prouductList.FirstOrDefault(p => p.Product.Id == id);
        }
        public IEnumerable<ProductBase> GetAllProuduct()
        {
            return repository.Get();
        }
        public void SaveData()
        {
            repository.Dispose();
        }

    }
}
