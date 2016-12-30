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
using WpfApplication1.Models;
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
        string accountNumber { get; set; }
        string reciver { get; set; }
        string revicedDate { get; set; }
        string amount { get; set; }
        string serial { get; set; }
        public CheckClearingWindow()
        {
            InitializeComponent();
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();

            this.accountNumber = null;
            this.reciver = null;
            this.revicedDate = null;
            this.amount = null;
            this.serial = null;
        }

        private void btn_cancel_check_clearing(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_evaluate_check_clearing(object sender, RoutedEventArgs e)
        {

            this.accountNumber = AccountNumber.Text;
            this.reciver = Receiver.Text;
            this.revicedDate = ReceivedDate.Text;
            this.amount = Amount.Text;
            this.serial = SerialNumber.Text;
            if(!Validator.IsDigitsOnly(accountNumber))
            {
                MessageBox.Show("Error Message: invalid account number");
                return;
            }
            if(!Validator.validateChar(reciver))
            {
                MessageBox.Show("Error Message: invalid reciver parameter");
                return;
            }
            if(!Validator.validateDate(revicedDate))
            {
                MessageBox.Show("Error Message: invalid recived date specifed");
                return;
            }
            if(!Validator.validateMoney(amount))
            {
                MessageBox.Show("Error Message: invalid amount parameter");
                return;
            }

            DbHelper helper = DbHelper.getInstance();
            CurrentAccount ca1 = helper.getCurrentAccDetails(accountNumber);
            CurrentAccount ca2 = helper.getDbContext().CurrentAccounts.Where(a => a.AId.ToString() == accountNumber).FirstOrDefault();
            if (ca1 == null || ca2 == null)
            {
                MessageBox.Show("Error Message: Account number does not exist.");
                return;
            }
            AccountBalanceLabelTxt = ca2.Remainder.ToString();

            Check check = helper.getCheckOfAccount(accountNumber);
            if(check == null)
            {
                MessageBox.Show("Error Message: Specified account does not have a check registered with it.");
            }else
            {
                SerialNumber.Text = check.CheckId.ToString();
            }

            MessageBox.Show("[Next] Press Cash the Check Now");
            // check account number balance and set "AccountBalanceLabelTxt"
            // and then enable either of "DishonorCheckBtn" or "CashCheckBtn"
            CashCheckBtn.IsEnabled = true;
            /* for now, suppose that check is cleared */

            
        }


        private void btn_cash_check(object sender, RoutedEventArgs e)
        {
            if( this.accountNumber == null || 
                this.reciver == null  ||
                this.revicedDate == null ||
                this.amount == null ||
                this.serial == null )
            {
                MessageBox.Show("Error Message: Please press the evaluate button first");
                return;
            }
            DbHelper helper = DbHelper.getInstance();
            int result = helper.controlCheckPaper(accountNumber, reciver, revicedDate, amount);
            if(result == -1)
            {
                MessageBox.Show("Error Message: Specified account does not have a check registered with it.");
            }else if(result == -2)
            {
                MessageBox.Show("Error Message: Specified account does not have enough balance.");
            }
            else
            {
                MessageBox.Show(string.Format("CheckPaper registered Successfully with Code [ CheckPaperId={0} ]",result));
            }
            this.Close();
        }
    }
}
