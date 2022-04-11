using System;
using System.Collections.Generic;
using System.Linq;
namespace BookStore.Model
{
    [Serializable]
    public class Journal : ProductBase
    {
        public string Name
        {
            get { return base.Description; }
            set { base.Description = value; }
        }
        public string EditorName { get; set; }
        public int IssueNumber { get; set; } 
        public string Frequency { get; set; }
        public Journal(string name,string editorName, DateTime publicationDate,
            decimal basePrice,int quantity, int issueNumber, string frequency , List<string> genres)
           : base(name, publicationDate, basePrice, quantity, genres)
        {
            EditorName = editorName;
            IssueNumber = issueNumber;
            Frequency = frequency;
            this.Genres = genres;
        }

        public override bool IsSameProuduct(ProductBase productBase)
        {
            if (productBase is Journal otherJournal)
            {
                return otherJournal.Name == this.Name && otherJournal.IssueNumber == this.IssueNumber;
            }
            return false;
        }

    }
}
