using System;
using System.Collections.Generic;

namespace BookStore.Model
{
    [Serializable]
    public class Book : ProductBase
    {
        public string Title
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        public string AuthorName { get; set; }
        public int Edition { get; set; } //  מהדורה
        public string Synopsis { get; set; } //תקציר
        public List<string> Genres { get; set; }
        public Book(string title, string authorName, DateTime publicationDate, decimal basePrice, int quantity
           , List<string> genres, string synopsis, int edition = 1)
            : base(title, publicationDate, basePrice, quantity, genres)
        {
            AuthorName = authorName;
            Edition = edition;
            Genres = genres;
            Synopsis = synopsis;
        }
        public override bool IsSameProuduct(ProductBase productBase)
        {
            if (productBase is Book other)
            {
               return other.Title == this.Title && other.Edition==this.Edition;
            }
            return false;
        }

      
    }
}
