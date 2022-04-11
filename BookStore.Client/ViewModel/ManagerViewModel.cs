using BookStore.Client.Views;
using BookStore.Model;
using BookStore.Server;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class ManagerViewModel:ViewModelBase
    {
        ProductBase product;
        ProductsService productsService;
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand RemoveProductCommand { get; set; }
        public RelayCommand AddCommand { get; set; }      
        public ManagerViewModel(ProductsService productsService)
        {
            this.productsService = productsService;
            GoBackCommand = new RelayCommand(GoBack);
            AddCommand = new RelayCommand(Add);         
            RemoveProductCommand = new RelayCommand(RemoveProduct);
            MessengerInstance.Register<ProductBase>(this, ProductData);
           
        }
        private void ProductData(ProductBase product)
        {
            this.product = product;
        }
        private void RemoveProduct()
        {
            productsService.RemoveProduct(product.Id);
            MessengerInstance.Send(true);
        }

        private void Add()
        {
            MessengerInstance.Send((UserControl)new AddView());
        }
        private void GoBack()
        {
            MessengerInstance.Send(true);
            MessengerInstance.Send((UserControl)new StartView());

        }
    }
}
