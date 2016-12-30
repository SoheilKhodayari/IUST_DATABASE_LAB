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
    /// Interaction logic for AccountManagementInputDialog.xaml
    /// </summary>
    public partial class AccountManagementInputDialog : Window
    {
        public AccountManagementInputDialog()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            string accountId = this.ResponseText;
            if(!Validator.IsDigitsOnly(accountId))
            {
                MessageBox.Show("Error Message: Invalid Account Number");
                return;
            }
            bool account_exists = !accountId.Equals("");
            SavingAccount sa = null;
            CurrentAccount ca = null; 
            DepositAccount da = null; 
            ForeignCurrencyAccount fca = null;
            string AccountType;
            string AccountBranchCode;
            string AccountBalance;
            string AccountOpeningDate;

            DbHelper helper = DbHelper.getInstance();
            BankDBContext ctx = helper.getDbContext();

            ACCOUNT_TYPES account_type = ACCOUNT_TYPES.Saving_Account;
            var selected_account_obj = this.AccountTypeCombo.SelectedValue;
            if (selected_account_obj == null)
            {
                MessageBox.Show("Error Message: Account type not specified"); return;
            }
            string selected_account = selected_account_obj.ToString();
            if (selected_account == "Deposit")
            { 
                account_type = ACCOUNT_TYPES.Deposite_Account;
                da = helper.getDepositAccDetails(accountId);
            }
            else if (selected_account == "Current")
            {
                account_type = ACCOUNT_TYPES.Current_Account;
                ca = helper.getCurrentAccDetails(accountId);
            }
            else if (selected_account == "ForeignCurrency")
            {
                account_type = ACCOUNT_TYPES.Foreign_Currency_Account;
                fca = ctx.ForeignCurrencyAccounts.Where(x => x.AId.ToString() == accountId).FirstOrDefault();
            }
            else if (selected_account == "Saving")
            {
                account_type = ACCOUNT_TYPES.Saving_Account;
                sa = helper.getSavingAccDetails(accountId);
            }
            if( sa == null && ca == null && da == null && fca == null)
            {
                account_exists = false;
                MessageBox.Show("Error Message: Invalid Account Number");
                return;
            }

            if (account_exists)
            {
                if (account_type == ACCOUNT_TYPES.Saving_Account)
                {
                     AccountType = "Saving Account";
                     AccountBranchCode = sa.BranchId_FK.ToString();
                     AccountBalance = sa.Remainder.ToString();
                     AccountOpeningDate = sa.OpeningDate.ToString();
                }
                else if (account_type == ACCOUNT_TYPES.Deposite_Account)
                {
                     AccountType = "Deposit Account";
                     AccountBranchCode = da.BranchId_FK.ToString();
                     AccountBalance = da.Remainder.ToString();
                     AccountOpeningDate = da.OpeningDate.ToString();
                }
                else if (account_type == ACCOUNT_TYPES.Foreign_Currency_Account)
                {
                     AccountType = "Foreign Currency Account";
                     AccountBranchCode = fca.BranchId_FK.ToString();
                     AccountBalance = fca.Remainder.ToString();
                     AccountOpeningDate = fca.OpeningDate.ToString();
                }
                else // if (type == ACCOUNT_TYPES.Current_Account)
                {
                     AccountType = "Current Account";
                     AccountBranchCode = ca.BranchId_FK.ToString();
                     AccountBalance = ca.Remainder.ToString();
                     AccountOpeningDate = ca.OpeningDate.ToString();
                }


                //--------------------------------------------------------------------------//
                AccountManagementWindow amw = new AccountManagementWindow(accountId, AccountType, AccountBranchCode, AccountOpeningDate, AccountBalance);
                amw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Message: Invalid Account Number");
            }
        }

    }
}
