using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for AccountOpeningWindow.xaml
    /// </summary>
    public partial class AccountOpeningWindow : Window
    {
        public AccountOpeningWindow()
        {
            InitializeComponent();
        }

        private void cancel_form_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void reset_form_click(object sender, RoutedEventArgs e)
        {

        }

        private void submit_form_click(object sender, RoutedEventArgs e)
        {

        }


    }
}
