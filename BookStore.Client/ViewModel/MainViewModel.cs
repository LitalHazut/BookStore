using BookStore.Client.Views;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
namespace BookStore.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserControl myUserControl;
        public UserControl MyUserControl { get => myUserControl; set => Set(ref myUserControl, value); }
        public MainViewModel()
        {
            MyUserControl = new StartView();
            MessengerInstance.Register<UserControl>(this, CreateUserControl);
        }
        private void CreateUserControl(UserControl user)
        {          
            MyUserControl = user;
        }
    }
}