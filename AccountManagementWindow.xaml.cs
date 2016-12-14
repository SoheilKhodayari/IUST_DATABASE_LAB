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
    /// Interaction logic for AccountManagementWindow.xaml
    /// </summary>
    public partial class AccountManagementWindow : Window
    {
        public string _accountId { get; set; }
        
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

        public AccountManagementWindow(string accountId,string _AccountType, string _AccountBranchCode, string _AccountOpeningDate, string _AccountBalance)
        {

            InitializeComponent();

            this._accountId = accountId;
            this._AccountType = _AccountType;
            this._AccountBranchCode = _AccountBranchCode;
            this._AccountOpeningDate = _AccountOpeningDate;
            this._AccountBalance = _AccountBalance;
            this.LeftHeadingAccount.Content = "AccountId: " + this._accountId;
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void edit_customer_details(object sender, RoutedEventArgs e)
        {
            // submit btn for edit detail pressed
        }

        private void close_account_confirm(object sender, RoutedEventArgs e)
        {
            // close account
        }

        private void block_account_confirm(object sender, RoutedEventArgs e)
        {
            // block account
        }
    }
}
