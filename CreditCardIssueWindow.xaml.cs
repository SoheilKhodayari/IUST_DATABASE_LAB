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
    /// Interaction logic for CreditCardIssueWindow.xaml
    /// </summary>
    public partial class CreditCardIssueWindow : Window
    {
        public CreditCardIssueWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)combo.SelectedItem;
            string tag = item.Tag.ToString();
            if(tag == "Account-Card")
            {
                AccountNumberTxtBlock.Visibility = Visibility.Visible;
                AccountNumber.Visibility = Visibility.Visible;
                BalanceTxtBlock.Visibility = Visibility.Collapsed;
                Balance.Visibility = Visibility.Collapsed;
            }
            else if(tag == "Gift-Card")
            {
                BalanceTxtBlock.Visibility = Visibility.Visible;
                Balance.Visibility = Visibility.Visible;
                AccountNumberTxtBlock.Visibility = Visibility.Collapsed;
                AccountNumber.Visibility = Visibility.Collapsed;
            }
            else
            {
                BalanceTxtBlock.Visibility = Visibility.Collapsed;
                Balance.Visibility = Visibility.Collapsed;
                AccountNumberTxtBlock.Visibility = Visibility.Collapsed;
                AccountNumber.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_issue_clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("credit card issue confirmed");
        }

        private void btn_cancel_clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
