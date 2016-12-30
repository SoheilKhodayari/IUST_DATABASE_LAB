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
        public int CardType { get; set; }
        public CreditCardIssueWindow()
        {
            InitializeComponent();
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
            this.CardType = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)combo.SelectedItem;
            string tag = item.Tag.ToString();
            if(tag == "Account-Card")
            {
                this.CardType = 0;
                AccountNumberTxtBlock.Visibility = Visibility.Visible;
                AccountNumber.Visibility = Visibility.Visible;
                BalanceTxtBlock.Visibility = Visibility.Collapsed;
                Balance.Visibility = Visibility.Collapsed;
            }
            else if(tag == "Gift-Card")
            {
                this.CardType = 1;
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
            DbHelper h = DbHelper.getInstance();
            SqlWrapper w = SqlWrapper.getInstance();
            List<string> result;
            if(this.CardType == 0)
            {
                if(!Validator.IsDigitsOnly(AccountNumber.Text))
                {
                   MessageBox.Show("Error Message: invalid saving account number specified");
                   return;
                }
                result = w.creditCradIssuance(this.CardType,AccountNumber.Text);
                MessageBox.Show("credit card issue successfull");
                MessageBox.Show(string.Format("CardDetails:\n\n CardNumber:{0} \n Balance: {1}\n ExpDate:{2} \n CVV2: {3}\n Password: {4}\n SecondPassword: {5}", result[0], result[1], result[2], result[3], result[4], result[5]));
            }
            else
            {
               if(!Validator.validateMoney(Balance.Text))
               {
                   MessageBox.Show("Error Message: invalid balance specified");
                   return;
               }
               result = w.creditCradIssuance(this.CardType, Balance.Text);
               MessageBox.Show("credit card issue successfull");
               MessageBox.Show(string.Format("CardDetails:\n\n CardNumber:{0} \n Balance: {1}\n ExpDate:{2} \n CVV2: {3}n Password: {4}\n SecondPassword: {5}", result[0], result[1], result[2], result[3], result[4], result[5]));

            }

        }

        private void btn_cancel_clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
