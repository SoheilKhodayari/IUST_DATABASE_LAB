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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WpfApplication1.Models;

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
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();

        }

        private void btn_calculate_account_interest_clicked(object sender, RoutedEventArgs e)
        {
            /* calulate yearly and monthly interest and 
             * fill the following ReadOnly TextBoxes
             * 1-InterestRatio 2-MonthlyInterest 3-YearlyInterest */

            if(!Validator.IsDigitsOnly(DepositAccountNumber.Text))
            {
                MessageBox.Show("Error Message: Invalid Deposit Account Number");
                return;
            }
            SqlWrapper w = SqlWrapper.getInstance();
            List<decimal> values = w.DespositAccountRatio(DepositAccountNumber.Text);
            if(values.Count() == 0)
            {
                MessageBox.Show("Error Message: Account Number does not exists.");
                return;
            }
            InterestRatio.Text = values[0].ToString();
            MonthlyInterest.Text = values[1].ToString();
            YearlyInterest.Text = values[2].ToString();

        }

        private void btn_reset_clicked(object sender, RoutedEventArgs e)
        {

        }

    }
}
