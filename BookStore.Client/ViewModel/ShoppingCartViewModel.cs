using BookStore.Client.Views;
using BookStore.Model;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class ShoppingCartViewModel:ViewModelBase
    {
        public List<CartProduct> ListProductBase { get; set; }
        public ProductsService productsService;
        public ObservableCollection <CartProduct> AddedToCartProduct { get; set; }
        public RelayCommand BuyCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public ShoppingCartViewModel(ProductsService productsService)
        {
            this.productsService = productsService;
            
            BuyCommand = new RelayCommand(BuyButton);
            GoBackCommand = new RelayCommand(GoBack);
            MessengerInstance.Register<List<CartProduct>>(this, ProductData);
            AddedToCartProduct = new ObservableCollection<CartProduct>();
        }
        private void ProductData(List<CartProduct> Product)
        {
            this.ListProductBase = Product;
            AddToList();
        }
        private void GoBack()
        {
            MessengerInstance.Send(true);
            MessengerInstance.Send((UserControl)new CustomerView()); 
        }
        private void AddToList()
        {
            AddedToCartProduct.Clear();
            ListProductBase.ForEach(a => AddedToCartProduct.Add(a));
        }
        private void BuyButton()
        {
            productsService.BuyAllProducts(ListProductBase);
            MessengerInstance.Send(true);
            MessageBox.Show("The Proudct was added to the shopping cart!!");
            AddedToCartProduct.Clear();
        }
       
    }
}
