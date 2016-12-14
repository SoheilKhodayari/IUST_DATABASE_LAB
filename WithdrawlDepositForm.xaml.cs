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

        public WithdrawlDepositForm(string accountId,
                                    string name,
                                    string ssn, 
                                    string remainder, 
                                    string branchcode,
                                    string opendate, 
                                    string type)
        {
            InitializeComponent();

            this._accountId = accountId;
            this._AccountPersonName = name;
            this._AccountPersonSSN = ssn;
            this._AccountBalance = remainder;
            this._AccountBranchCode = branchcode;
            this._AccountOpeningDate = opendate;
            this._AccountType = type;
            this.LeftHeadingAccount.Content = "AccountId: "+ this._accountId;
        }

        private void withdraw_confirm(object sender, RoutedEventArgs e)
        {
            // to do
            MessageBox.Show("Withdraw Confirmed");
        }

        private void deposit_confirm(object sender, RoutedEventArgs e)
        {
            // to do
            MessageBox.Show("Deposit Confirmed");
        }
    }
}
