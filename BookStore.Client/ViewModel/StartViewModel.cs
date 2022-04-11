using BookStore.Client.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace BookStore.Client.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        public RelayCommand CustomerCommand { get; set; }
        public RelayCommand ManagerCommand { get; set; }
        public StartViewModel()
        {
            CustomerCommand = new RelayCommand(ClickCustomer);
            ManagerCommand = new RelayCommand(ClickManager);
        }
        private void ClickManager()
        {
            MessengerInstance.Send((UserControl)new ManagerView());
        }
        private void ClickCustomer()
        {
            MessengerInstance.Send((UserControl)new CustomerView());
        }
    }
}
