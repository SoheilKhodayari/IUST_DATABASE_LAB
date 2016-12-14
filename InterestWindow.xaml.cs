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
    /// Interaction logic for InterestWindow.xaml
    /// </summary>
    public partial class InterestWindow : Window
    {
        public InterestWindow()
        {
            InitializeComponent();
        }

        private void btn_pay_clicked(object sender, RoutedEventArgs e)
        {
            string srcAccount = SourceDepositAccountNumber.Text;
            string destAccount = DestSavingAccountNumber.Text;

            // to do: update db

            MessageBox.Show("Interest Paid!");
        }

        private void btn_calculate_account_interest_clicked(object sender, RoutedEventArgs e)
        {
            /* calulate yearly and monthly interest and 
             * fill the following ReadOnly TextBoxes
             * 1-InterestRatio 2-MonthlyInterest 3-YearlyInterest */

            InterestRatio.Text = "20";
            MonthlyInterest.Text = "500000";
            YearlyInterest.Text = "6000000";
        }

    }
}
