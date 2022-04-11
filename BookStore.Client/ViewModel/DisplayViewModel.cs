using BookStore.Model;
using BookStore.Server;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace BookStore.Client.ViewModel
{
    public class DisplayViewModel : ViewModelBase
    {
        public ObservableCollection<ProductBase> AllProuducts { get; set; }
        public ObservableCollection<ProductBase> ShowProuducts { get; set; }                
        public ObservableCollection<string> GenresList { get; set; }
        public ObservableCollection<string> TypeCollection { get; set; }

        ProductsService productsService;
        private string selectedProuductType;
        public string SelectedProuductType
        {
            get => selectedProuductType;
            set
            {
                if (value != selectedProuductType)
                    Set(ref selectedProuductType, value);
                UpdateShowList();
                UpdateGenresList();
                UpdateVisibilBorders();
                SelectedProuduct = null;

            }
        }
        private string selectedGenreType;
        public string SelectedGenreType
        {
            get => selectedGenreType;
            set
            {
                Set(ref selectedGenreType, value);
                UpdatedByGenre();
            }
        }
        private string prouductName;
        public string ProuductName
        {
            get { return prouductName; }
            set
            {
                Set(ref prouductName, value);
            }
        }
        private decimal prouductPrice;
        public decimal ProuductPrice
        {
            get { return prouductPrice; }
            set
            {
                Set(ref prouductPrice, value);
            }
        }
        private string authorOrEditor;
        public string AuthorOrEditor
        {
            get { return authorOrEditor; }
            set
            {
                Set(ref authorOrEditor, value);
            }
        }
        private int editionOrIssueNumber;
        public int EditionOrIssueNumber
        {
            get { return editionOrIssueNumber; }
            set
            {
                Set(ref editionOrIssueNumber, value);
            }
        }
        private DateTime publicationDate;
        public DateTime PublicationDate
        {
            get { return publicationDate; }
            set
            {
                Set(ref publicationDate, value);
            }
        }
        public string PublicationDateString
        {
            get { return publicationDate.ToShortDateString(); }
            set { }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                Set(ref quantity, value);
            }
        }
        private ProductBase selectedProuduct;
        public ProductBase SelectedProuduct
        {
            get { return selectedProuduct; }
            set
            {
                Set(ref selectedProuduct, value);
                UpdateProp();
                MessengerInstance.Send(SelectedProuduct);
            }
        }

        private string genresString;
        public string GenresString
        {
            get { return genresString; }
            set
            {
                Set(ref genresString, value);
            }
        }
        private void UpdateProp()
        {
            ProductBase selected = SelectedProuduct;
            if (selected != null)
            {
                if (selected.GetType().Name == "Book")
                {
                    VisibilBookBorder = Visibility.Visible;
                    VisibilJournalBorder = Visibility.Collapsed;

                    Book selectedBook = selected as Book;
                    Synopsis = selectedBook.Synopsis;
                    AuthorOrEditor = selectedBook.AuthorName;
                    EditionOrIssueNumber = selectedBook.Edition;
                }
                if (selected.GetType().Name == "Journal")
                {
                    VisibilBookBorder = Visibility.Collapsed;
                    VisibilJournalBorder = Visibility.Visible;

                    Journal selectedJournal = selected as Journal;
                    AuthorOrEditor = selectedJournal.EditorName;
                    EditionOrIssueNumber = selectedJournal.IssueNumber;
                    Frequency = selectedJournal.Frequency;
                }
                ProuductName = selected.Description;
                PublicationDate = selected.PublicationDate;
                Quantity = selected.Quantity;

                StringBuilder sb = new StringBuilder();
                selectedProuduct.Genres.ToList().ForEach(g => sb.Append($"{g}, "));
                sb.Remove(sb.Length - 2, 2);
                GenresString = sb.ToString();
            }
            else
            {
                VisibilBookBorder = Visibility.Collapsed;
                VisibilJournalBorder = Visibility.Collapsed;
            }



        }
        private string frequency;
        public string Frequency
        {
            get { return frequency; }
            set
            {
                Set(ref frequency, value);
            }
        }
        private string synopsis;
        public string Synopsis
        {
            get { return synopsis; }
            set
            {
                Set(ref synopsis, value);
            }
        }

        private Visibility visibilJournalBorder;
        public Visibility VisibilJournalBorder
        {
            get => visibilJournalBorder;
            set => Set(ref visibilJournalBorder, value);
        }

        private Visibility visibilBookBorder;
        public Visibility VisibilBookBorder
        {
            get => visibilBookBorder;
            set => Set(ref visibilBookBorder, value);
        } 
        private void UpdateShowList()
        {
            if (ShowProuducts != null)
            {
                ShowProuducts.Clear();
                if (SelectedProuductType == "All") AllProuducts.ToList().ForEach(p => ShowProuducts.Add(p));
                if (SelectedProuductType == "Book") AllProuducts.ToList().ForEach(p =>
                {
                    if (p.GetType().Name == "Book") ShowProuducts.Add(p);
                });
                if (SelectedProuductType == "Journal") AllProuducts.ToList().ForEach(p =>
                {
                    if (p.GetType().Name == "Journal") ShowProuducts.Add(p);
                });

            }
        }
        private void UpdateGenresList()
        {
            if (GenresList != null)
            {
                GenresList.Clear();
                GenresList.Add("All");
                if (SelectedProuductType == "Book") productsService.BookGenre.ToList().ForEach(g => GenresList.Add(g));
                if (SelectedProuductType == "Journal") productsService.JournalGenre.ToList().ForEach(g => GenresList.Add(g));


            }
        }
        private void UpdateVisibilBorders()
        {
            VisibilBookBorder = Visibility.Collapsed;
            VisibilJournalBorder = Visibility.Collapsed;

            if (SelectedProuductType == "Book")
            {
                VisibilBookBorder = Visibility.Visible;
                VisibilJournalBorder = Visibility.Collapsed;

            }
            if (SelectedProuductType == "Journal")
            {
                VisibilJournalBorder = Visibility.Visible;
                VisibilBookBorder = Visibility.Collapsed;

            }
        }
        public DisplayViewModel(ProductsService productsService)
        { 
            this.productsService = productsService;
            TypeCollection = new ObservableCollection<string>();
            AllProuducts = new ObservableCollection<ProductBase>();
           
            ShowProuducts = new ObservableCollection<ProductBase>();
            GenresList = new ObservableCollection<string>();
            GetTypes();
            
            UpdatedAllProuducts();
            UpdateFirstType();
            MessengerInstance.Register<bool>(this, Updated, true);
        }
        private void UpdatedAllProuducts()
        {
            AllProuducts.Clear();     
            productsService.GetAllProuduct().ToList().ForEach(p => AllProuducts.Add(p));
            
        }
        private void Updated(bool obj)
        {
            UpdatedAllProuducts();
            UpdateFirstType();
        }
        private void UpdateFirstType()
        {
            SelectedProuductType = "All";
        }
        private void GetTypes()
        {
            TypeCollection.Add("All");
            productsService.Types.ForEach(p => TypeCollection.Add(p));
        }
        private void UpdatedByGenre()
        {
            if (ShowProuducts != null)
            {
                ShowProuducts.Clear();
                if (SelectedProuductType == "All") AllProuducts.ToList().ForEach(p => ShowProuducts.Add(p));
                if (SelectedProuductType == "Book" && SelectedGenreType=="All") AllProuducts.ToList().ForEach(p =>
                {
                    if (p.GetType().Name == "Book") ShowProuducts.Add(p);
                });
                if (SelectedProuductType == "Book") AllProuducts.ToList().ForEach(p =>
                {
                     if (p.Genres.Contains(SelectedGenreType)) ShowProuducts.Add(p);
                });
                if (SelectedProuductType == "Journal" && SelectedGenreType == "All") AllProuducts.ToList().ForEach(p =>
                {
                    if (p.GetType().Name == "Journal") ShowProuducts.Add(p);
                });
                if (SelectedProuductType == "Journal") AllProuducts.ToList().ForEach(p =>
                {
                    if (p.Genres.Contains(SelectedGenreType)) ShowProuducts.Add(p);
                });

            }


        }
    }
}

