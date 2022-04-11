using BookStore.Client.Views;
using BookStore.Model;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        public ProductBase productBase;
        public ProductsService productsService;
        public List<CartProduct> AddedProducts { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand GoToShoppingCart { get; set; }
        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                Set(ref amount, value);
                if (Amount == 0) IsEnabledButton = false;
                else IsEnabledButton = true;

            }
        }
        public RelayCommand PlusCommand { get; set; }
        private bool isEnabledPlus;
        public bool IsEnabledPlus
        {
            get { return isEnabledPlus; }
            set => Set(ref isEnabledPlus, value);
        }

        private bool isEnabledButton;
        public bool IsEnabledButton
        {
            get { return isEnabledButton; }
            set => Set(ref isEnabledButton, value);
        }
        public RelayCommand MinusCommand { get; set; }
        public CustomerViewModel(ProductsService productsService)
        {
            this.productsService = productsService;
            GoBackCommand = new RelayCommand(GoBack);
            AddedProducts = new List<CartProduct>();
            IsEnabledPlus = true;
            IsEnabledButton = false;
            MessengerInstance.Register<ProductBase>(this, ProductData);
            PlusCommand = new RelayCommand(PlusAmount);
            MinusCommand = new RelayCommand(MinusAmount);
            AddCommand = new RelayCommand(Add);
            GoToShoppingCart = new RelayCommand(ShoppingCart);
        }
        private void ShoppingCart()
        {
            MessengerInstance.Send((UserControl)new ShoppingCartView());
            MessengerInstance.Send(AddedProducts);
        }
        private void Add()
        {
            productsService.AddProudctToList(AddedProducts, new CartProduct(productBase, amount));
            MessengerInstance.Send(true);
        }
        private void ProductData(ProductBase Product)
        {
            this.productBase = Product;
            IsEnabledPlus = true;
            Amount = default;
        }
        private void MinusAmount()
        {
            if (Amount > 1) Amount--;
        }
        private void PlusAmount()
        {
            if (productBase != null)
            {
                var AmountOfProductInCart = productsService.AmountOfProductInCart(AddedProducts, productBase);
                if (Amount + AmountOfProductInCart < productBase.Quantity)
                {
                    IsEnabledPlus = true;
                    Amount++;
                }
                if (productBase.Quantity == 0)
                {
                    IsEnabledButton = false;
                }
            }
        }
        private void GoBack()
        {
            IsEnabledPlus = true;
            Amount = default;
            MessengerInstance.Send(true);
            MessengerInstance.Send((UserControl)new StartView());
        }
    }
}
