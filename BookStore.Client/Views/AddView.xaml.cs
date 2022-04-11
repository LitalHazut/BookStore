using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BookStore.Client.Views
{
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();
            cb2.IsEnabled = false;
            cb3.IsEnabled = false;
        }

        private void ComboIsChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cb1!=null & cb2!=null & cb2!=null)
            {
                if (cb1.SelectedValue == cb2.SelectedValue) cb2.SelectedValue = cb3.SelectedValue;
                if (cb1.SelectedValue == cb3.SelectedValue) cb3.SelectedValue = null;
                if(cb2.SelectedValue == cb3.SelectedValue) cb3.SelectedValue = null;
                _ = cb1.SelectedValue != null ? cb2.IsEnabled = true : cb2.IsEnabled = false;
                _ = cb2.SelectedValue != null ? cb3.IsEnabled = true : cb3.IsEnabled = false;
            }
        }
        private void ComboIsChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (cb4 != null & cb5 != null & cb6 != null)
            {
                if (cb4.SelectedValue == cb5.SelectedValue) cb5.SelectedValue = cb6.SelectedValue;
                if (cb4.SelectedValue == cb6.SelectedValue) cb6.SelectedValue = null;
                if (cb5.SelectedValue == cb6.SelectedValue) cb6.SelectedValue = null;
                _ = cb4.SelectedValue != null ? cb5.IsEnabled = true : cb5.IsEnabled = false;
                _ = cb5.SelectedValue != null ? cb6.IsEnabled = true : cb6.IsEnabled = false;
            }
        }



    }
}
