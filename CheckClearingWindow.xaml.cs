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
    /// Interaction logic for CheckClearingWindow.xaml
    /// </summary>
    public partial class CheckClearingWindow : Window
    {
        public string AccountBalanceLabelTxt
        {
            get { return AccountBalance.Text; }
            set { AccountBalance.Text = value; }
        }
        public CheckClearingWindow()
        {
            InitializeComponent();
        }

        private void btn_cancel_check_clearing(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_evaluate_check_clearing(object sender, RoutedEventArgs e)
        {
            DishonorCheckBtn.IsEnabled = false;
            CashCheckBtn.IsEnabled = false;

            // check account number balance and set "AccountBalanceLabelTxt"
            // and then enable either of "DishonorCheckBtn" or "CashCheckBtn"

            AccountBalanceLabelTxt = "100000";
            /* for now, suppose that check is cleared */
            DishonorCheckBtn.IsEnabled = false;
            CashCheckBtn.IsEnabled = true;
            
        }

        private void btn_confirm_dishonor(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dishonor Check");
        }

        private void btn_cash_check(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cash the Check");
            this.Close();
        }
    }
}
