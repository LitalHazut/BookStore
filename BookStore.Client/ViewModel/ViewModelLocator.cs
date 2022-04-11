//  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
using BookStore.Server;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
namespace BookStore.Client.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartViewModel>();
            SimpleIoc.Default.Register<ManagerViewModel>();
            SimpleIoc.Default.Register<CustomerViewModel>();
            SimpleIoc.Default.Register<DisplayViewModel>();
            SimpleIoc.Default.Register<ShoppingCartViewModel>();
            SimpleIoc.Default.Register<AddViewModel>();
            SimpleIoc.Default.Register<ProductsService>();
            
        }
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public StartViewModel Start => ServiceLocator.Current.GetInstance<StartViewModel>();
        public ManagerViewModel Manager => ServiceLocator.Current.GetInstance<ManagerViewModel>();
        public CustomerViewModel Customer => ServiceLocator.Current.GetInstance<CustomerViewModel>();
        public DisplayViewModel Display => ServiceLocator.Current.GetInstance<DisplayViewModel>();
        public AddViewModel Add => ServiceLocator.Current.GetInstance<AddViewModel>();
        public ShoppingCartViewModel ShoppingCart => ServiceLocator.Current.GetInstance<ShoppingCartViewModel>();
       
       
    }
}