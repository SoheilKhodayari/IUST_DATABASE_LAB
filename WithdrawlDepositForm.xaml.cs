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
    /// Interaction logic for WithdrawlDepositForm.xaml
    /// </summary>
    public partial class WithdrawlDepositForm : Window
    {
        public string _accountId { get; set; }
        public string _AccountPersonName
        {
            get { return AccountPersonName.Text; }
            set { AccountPersonName.Text = value; }
        }

        public string _AccountPersonSSN
        {
            get { return AccountPersonSSN.Text; }
            set { AccountPersonSSN.Text = value; }
        }

         public string _AccountBalance
        {
            get { return AccountBalance.Text; }
            set { AccountBalance.Text = value; }
        }
        public string _AccountOpeningDate
        {
            get { return AccountOpeningDate.Text; }
            set { AccountOpeningDate.Text = value; }
        }

        public string _AccountType
        {
            get { return AccountType.Text; }
            set { AccountType.Text = value; }
        }

        public string _AccountBranchCode
        {
            get { return AccountBranchCode.Text; }
            set { AccountBranchCode.Text = value; }
        }

        public decimal PureBalance { get; set; }

        public ACCOUNT_TYPES type { get; set; }

        public WithdrawlDepositForm(string accountId,
                                    string name,
                                    string ssn, 
                                    string remainder, 
                                    string branchcode,
                                    string opendate, 
                                    string type,
                                    ACCOUNT_TYPES t)
        {
            InitializeComponent();

            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();

            this._accountId = accountId;
            this._AccountPersonName = name;
            this._AccountPersonSSN = ssn;
            this.PureBalance = decimal.Parse(remainder);
            
            this._AccountBranchCode = branchcode;
            this._AccountOpeningDate = opendate;
            this._AccountType = type;
            this.type = t;
            this.LeftHeadingAccount.Content = "ACCOUNT NUMBER: " + this._accountId;
            setBalanceOnWindow();

        }
        private void setBalanceOnWindow()
        {
            switch (this.type)
            {
                case ACCOUNT_TYPES.Saving_Account:
                    this._AccountBalance = this.PureBalance + " Rials";
                    break;
                case ACCOUNT_TYPES.Current_Account:
                    this._AccountBalance = this.PureBalance + " Rials";
                    break;
                case ACCOUNT_TYPES.Foreign_Currency_Account:
                    this._AccountBalance = this.PureBalance + " Dollars";
                    break;
                case ACCOUNT_TYPES.Deposite_Account:
                    this._AccountBalance = this.PureBalance + " Rials";
                    break;
            }
        }

        private void withdraw_confirm(object sender, RoutedEventArgs e)
        {
            if(!Validator.validateMoney(WithdrawalAmount.Text))
            {
                MessageBox.Show("Error Message: Invalid Amount For Withdrawal Amount");
                return;
            }
            SqlWrapper w = SqlWrapper.getInstance();
            w.withdraw(this._accountId,this.type,WithdrawalAmount.Text);
            this.PureBalance -= decimal.Parse(WithdrawalAmount.Text);
            MessageBox.Show(string.Format("Operation Successful: New Balance = {0}", this.PureBalance));
            setBalanceOnWindow();
            WithdrawalAmount.Clear();

            
        }

        private void deposit_confirm(object sender, RoutedEventArgs e)
        {
            if (!Validator.validateMoney(DepositAmount.Text))
            {
                MessageBox.Show("Error Message: Invalid Amount For Deposit Amount");
                return;
            }
            SqlWrapper w = SqlWrapper.getInstance();
            w.deposit(this._accountId, this.type, DepositAmount.Text);
            this.PureBalance += decimal.Parse(DepositAmount.Text);
            MessageBox.Show(string.Format("Operation Successful: New Balance = {0}", this.PureBalance));
            setBalanceOnWindow();
            DepositAmount.Clear();
        }
    }
}
