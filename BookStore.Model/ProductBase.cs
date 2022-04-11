using BookStore.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace BookStore.Model
{
    [Serializable]
    public abstract class ProductBase: IDataEntity
    {
        public Guid Id { get; internal set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal BasePrice { get; set; }
        public int Quantity { get; set; }
     
        public List<string> Genres { get; set; }
        
        protected ProductBase(string discription, DateTime publicationDate, decimal basePrice, int quantity, List<string> genres )
            : this(Guid.NewGuid(), discription, publicationDate, basePrice, quantity, genres) { }

        internal ProductBase(Guid id, string discription, DateTime publicationDate, decimal basePrice, int quantity ,List<string> genres )
        {
            Id = id;
            Description = discription;
            PublicationDate = publicationDate;
            BasePrice = basePrice;
            Quantity = quantity;
            this.Genres = genres;
        }
        public abstract bool IsSameProuduct(ProductBase productBase);
    
    }
}
