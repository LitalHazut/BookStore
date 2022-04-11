using BookStore.Client.Views;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace BookStore.Client.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        ProductsService productsService;
        public ObservableCollection<string> TypeCollection { get; set; }      
        public ObservableCollection<string> FirstGenresList { get; set; }
        public ObservableCollection<string> SecondGenresList { get; set; }
        public ObservableCollection<string> ThirdGenresList { get; set; }
        public ObservableCollection<string> JournalFrequencyList { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                Set(ref description, value);
                TestIfOnePropIsNull();
            }
        }
        private string authorName;
        public string AuthorName
        {
            get { return authorName; }
            set
            {
                Set(ref authorName, value);
                TestIfOnePropIsNull();
            }
        }
       
        private DateTime publicationDate;
        public DateTime PublicationDate
        {
            get { return publicationDate; }
            set
            {
                Set(ref publicationDate, value);
                TestIfOnePropIsNull();
            }
        }
        private int basePrice;
        public int BasePrice
        {
            get { return basePrice; }
            set
            {
                Set(ref basePrice, value);
                TestIfOnePropIsNull();
            }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                Set(ref quantity, value);
                TestIfOnePropIsNull();
            }
        }
        private List<string> genres;
        public List<string> Genres
        {
            get { return genres; }
            set
            {
                Set(ref genres, value);
            }
        }
        private int edition;
        public int Edition
        {
            get { return edition; }
            set
            {
                Set(ref edition, value);
                if (Edition <= 0) Edition = 1;
                TestIfOnePropIsNull();
            }
        }
        private string selectedProuductType;
        public string SelectedProuductType
        {
            get => selectedProuductType;
            set
            {
                if (value != selectedProuductType)
                    Set(ref selectedProuductType, value);
                GetJournalFrequencyList();
                UpdateGenreProp();
                UpdateVisibilBorders();
                GetGenre();
            }

        }
        private bool isEnabledGenre;
        public bool IsEnabledGenre
        {
            get => isEnabledGenre;
            set => Set(ref isEnabledGenre, value);
        }

        private bool isEnabledAddButton;
        public bool IsEnabledAddButton
        {
            get => isEnabledAddButton;
            set => Set(ref isEnabledAddButton, value);

        }
        private bool isEnabledSecondGenre;
        public bool IsEnabledSecondGenre
        {
            get => isEnabledSecondGenre;
            set => Set(ref isEnabledSecondGenre, value);
        }
        private bool isEnabledThirdGenre;
        public bool IsEnabledThirdGenre
        {
            get => isEnabledThirdGenre;
            set => Set(ref isEnabledThirdGenre, value);
        }

        private string selectedGenre1;
        public string SelectedGenre1
        {
            get => selectedGenre1;
            set
            {
                Set(ref selectedGenre1, value);
                if (SelectedGenre1 != null)
                {
                    SecondGenresList.Clear();
                    FirstGenresList.ToList().ForEach(genre => SecondGenresList.Add(genre));
                    SecondGenresList.Remove(SelectedGenre1);
                }
                TestIfOnePropIsNull();
            }
        }
        private string selectedGenre2;
        public string SelectedGenre2
        {
            get => selectedGenre2;
            set
            {
                Set(ref selectedGenre2, value);
                if (SelectedGenre2 != null)
                {
                    ThirdGenresList.Clear();
                    SecondGenresList.ToList().ForEach(genre => ThirdGenresList.Add(genre));
                    ThirdGenresList.Remove(SelectedGenre2);
                }
                TestIfOnePropIsNull();
            }
        }
        private string selectedGenre3;
        public string SelectedGenre3
        {
            get => selectedGenre3;
            set
            {
                Set(ref selectedGenre3, value);
                TestIfOnePropIsNull();
            }
        }
        private void UpdateGenreProp()
        {
            IsEnabledGenre = true;
        }
        private string frequency;
        public string Frequency
        {
            get { return frequency; }
            set
            {
                Set(ref frequency, value);
                TestIfOnePropIsNull();
            }
        }
        private int issueNumber;
        public int IssueNumber
        {
            get { return issueNumber; }
            set
            {
                Set(ref issueNumber, value);
                TestIfOnePropIsNull();
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

        private string synopsis;
        public string Synopsis
        {
            get => synopsis;
            set
            {
                Set(ref synopsis, value);
            }
        }
        public AddViewModel(ProductsService productsService)
        {
            this.productsService = productsService;
            TypeCollection = new ObservableCollection<string>();
            FirstGenresList = new ObservableCollection<string>();
            SecondGenresList = new ObservableCollection<string>();
            ThirdGenresList = new ObservableCollection<string>();

            JournalFrequencyList = new ObservableCollection<string>();
            List<string> Genres = new List<string>();
            GoBackCommand = new RelayCommand(GoBack);
            AddCommand = new RelayCommand(Add);
            GetTypes();
            UpdateVisibilBorders();
        }
        public void AddBookOrJournal()
        {
            List<string> SelectedGenres = new List<string>();

            if (SelectedGenre1 != null) SelectedGenres.Add(SelectedGenre1);
            if (SelectedGenre2 != null) SelectedGenres.Add(SelectedGenre2);
            if (SelectedGenre3 != null) SelectedGenres.Add(SelectedGenre3);

            if (SelectedProuductType == "Book" || SelectedProuductType=="All")
            {
                productsService.AddBook(Description, AuthorName, PublicationDate, BasePrice, Quantity, SelectedGenres, synopsis, Edition);
            }
            if (SelectedProuductType == "Journal" || SelectedProuductType=="All")
            {
                productsService.AddJournal(Description, AuthorName, PublicationDate, BasePrice, Quantity, IssueNumber, Frequency, SelectedGenres);
            }
        }
        public void UpdateVisibilBorders()
        {
            VisibilBookBorder = Visibility.Collapsed;
            VisibilJournalBorder = Visibility.Collapsed;

            if (SelectedProuductType == "Book")
            {
                VisibilBookBorder = Visibility.Visible;
                VisibilJournalBorder = Visibility.Collapsed;
                ClearProp();
            }
            if (SelectedProuductType == "Journal")
            {
                VisibilJournalBorder = Visibility.Visible;
                VisibilBookBorder = Visibility.Collapsed;
                ClearProp();
            }
        }
        public void GetGenre()
        {
            FirstGenresList.Clear();
            if (SelectedProuductType == "Journal")
            {
                productsService.JournalGenre.ForEach(g => FirstGenresList.Add(g));
            }
            if (SelectedProuductType == "Book")
            {
                productsService.BookGenre.ForEach(g => FirstGenresList.Add(g));
            }
        }
        public void GetJournalFrequencyList()
        {
            if (SelectedProuductType == "Journal")
                productsService.JournalFrequency.ForEach(j => JournalFrequencyList.Add(j));
        }
        public void GetTypes()
        {
            productsService.Types.ForEach(p => TypeCollection.Add(p));
        }
        private void Add()
        {
            AddBookOrJournal();
            MessengerInstance.Send(true);
            ClearProp();
            MessengerInstance.Send((UserControl)new ManagerView());

        }
        private void GoBack()
        {
            MessengerInstance.Send((UserControl)new StartView());
            SelectedProuductType = null;
        }
        private void ClearProp()
        {
            Description = default;
            AuthorName = default;
            Edition = default;
            SelectedGenre1 = default;
            SelectedGenre2 = default;
            SelectedGenre3 = default;
            PublicationDate = default;
            IssueNumber = default;
            BasePrice = default;
            Quantity = default;
            Frequency = default;
            Synopsis = default;

        }
        private void TestIfOnePropIsNull()
        {
            if (SelectedProuductType != "Product")
            {
                if (Description == null || Description == "" || AuthorName == null || AuthorName == "" || BasePrice <= 0 || Quantity <= 0 || (SelectedGenre1 == null && SelectedGenre2 == null && SelectedGenre3 == null))
                {
                    IsEnabledAddButton = false;
                    return;
                }
                if (SelectedProuductType == "Journal")
                {
                    if (Frequency == null || IssueNumber <= 0)
                    {
                        IsEnabledAddButton = false;
                        return;
                    }
                }
                IsEnabledAddButton = true;
            }
            else IsEnabledAddButton = false;
        }

    }
}
