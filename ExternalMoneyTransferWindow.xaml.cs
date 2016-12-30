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
    /// Interaction logic for ExternalMoneyTransferWindow.xaml
    /// </summary>
    public partial class ExternalMoneyTransferWindow : Window
    {
        public string bank_code { get; set; }
        public ExternalMoneyTransferWindow(string bank_code)
        {
            this.bank_code = bank_code;
            InitializeComponent();
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
        }

        private void btn_reset_clicked(object sender, RoutedEventArgs e)
        {
            this.DestinationAccount.Clear();
            this.SourceAccount.Clear();
            this.RequesterName.Clear();
            this.AmountMoney.Clear();
        }

        private void btn_submit_clicked(object sender, RoutedEventArgs e)
        {

            SqlWrapper w = SqlWrapper.getInstance();
            var _ctx = w.getDbContext();

            var amountTransfer = this.AmountMoney.Text;
            var srcTransfer = this.SourceAccount.Text;
            var destTransfer = this.DestinationAccount.Text;
            var reqName = this.RequesterName.Text;
            ACCOUNT_TYPES src_account_type = ACCOUNT_TYPES.Saving_Account;
            ACCOUNT_TYPES dest_account_type = ACCOUNT_TYPES.Saving_Account;

            if (!Validator.validateMoney(amountTransfer))
            {
                MessageBox.Show("Error Message: Invalid transfer amount specified."); return;
            }
            if(!Validator.IsDigitsOnly(srcTransfer))
            {
                MessageBox.Show("Error Message: Invalid source account."); return;
            }
            if (!Validator.IsDigitsOnly(destTransfer))
            {
                MessageBox.Show("Error Message: Invalid destination account."); return;
            }
            if (!Validator.validateChar(reqName))
            {
                MessageBox.Show("Error Message: Invalid requester name."); return;
            }

            var selected_account_obj1 = this.srcAccountTypeCombo.SelectedValue;
            if (selected_account_obj1 == null)
            {
                MessageBox.Show("Error Message: Source Account Type Not Selected"); return;
            }
            string selected_account = selected_account_obj1.ToString();
            if (selected_account == "Deposit") src_account_type = ACCOUNT_TYPES.Deposite_Account;
            else if (selected_account == "Current") src_account_type = ACCOUNT_TYPES.Current_Account;
            else if (selected_account == "ForeignCurrency") src_account_type = ACCOUNT_TYPES.Foreign_Currency_Account;
            else if (selected_account == "Saving") src_account_type = ACCOUNT_TYPES.Saving_Account;


            var selected_account_obj2 = this.srcAccountTypeCombo.SelectedValue;
            if (selected_account_obj2 == null)
            {
                MessageBox.Show("Error Message: Destination Account Type Not Selected"); return;
            }
            selected_account = selected_account_obj2.ToString();
            if (selected_account == "Deposit") dest_account_type = ACCOUNT_TYPES.Deposite_Account;
            else if (selected_account == "Current") dest_account_type = ACCOUNT_TYPES.Current_Account;
            else if (selected_account == "ForeignCurrency") dest_account_type = ACCOUNT_TYPES.Foreign_Currency_Account;
            else if (selected_account == "Saving") dest_account_type = ACCOUNT_TYPES.Saving_Account;

            /* prevent same src and dest account */

            if(src_account_type == dest_account_type && srcTransfer == destTransfer)
            {
                MessageBox.Show("Error Message: Can not transfer money to the same account");
                return;
            }
            /* check account numbers validity and src account remainder */

            string src_account_bank = null;
            string src_account_person = null;
            string src_account_person_ssn = null;
            string dest_account_bank = null;
            string dest_account_person = null;
            string dest_account_person_ssn = null;
            switch (src_account_type)
            {
                case ACCOUNT_TYPES.Saving_Account:
                    SavingAccount sa = _ctx.SavingAccounts.Where(x => x.AId.ToString() == srcTransfer).FirstOrDefault();
                    if(sa == null)
                    {
                        MessageBox.Show("Error Message: source account does not exist.");
                        return;
                    }
                    if(sa.Remainder < decimal.Parse(amountTransfer))
                    {
                        MessageBox.Show(string.Format("Error Message: Source Account Balance= {0} which is less than needed.",amountTransfer));
                        return;
                    }
                    src_account_bank = sa.Branch.Bank.Name;
                    src_account_person = sa.Customer.Person.Name;
                    src_account_person_ssn = sa.Customer.Person.SSN;
                    break;
                case ACCOUNT_TYPES.Current_Account:
                    CurrentAccount ca = _ctx.CurrentAccounts.Where(x => x.AId.ToString() == srcTransfer).FirstOrDefault();
                    if(ca == null)
                    {
                        MessageBox.Show("Error Message: source account does not exist.");
                        return;
                    }
                    if(ca.Remainder < decimal.Parse(amountTransfer))
                    {
                        MessageBox.Show(string.Format("Error Message: Source Account Balance= {0} which is less than needed.",amountTransfer));
                        return;
                    }
                    src_account_bank = ca.Branch.Bank.Name;
                    src_account_person = ca.Customer.Person.Name;
                    src_account_person_ssn = ca.Customer.Person.SSN;
                    break;
                case ACCOUNT_TYPES.Deposite_Account:
                    DepositAccount da = _ctx.DepositAccounts.Where(x => x.AId.ToString() == srcTransfer).FirstOrDefault();
                    if(da == null)
                    {
                        MessageBox.Show("Error Message: source account does not exist.");
                        return;
                    }
                    if(da.Remainder < decimal.Parse(amountTransfer))
                    {
                        MessageBox.Show(string.Format("Error Message: Source Account Balance= {0} which is less than needed.",amountTransfer));
                        return;
                    }
                    src_account_bank = da.Branch.Bank.Name;
                    src_account_person = da.Customer.Person.Name;
                    src_account_person_ssn = da.Customer.Person.SSN;
                    break;
            }
            switch (dest_account_type)
            {
                case ACCOUNT_TYPES.Saving_Account:
                    SavingAccount sa = _ctx.SavingAccounts.Where(x => x.AId.ToString() == destTransfer).FirstOrDefault();
                    if(sa == null)
                    {
                        MessageBox.Show("Error Message: source account does not exist.");
                        return;
                    }
                    dest_account_bank = sa.Branch.Bank.Name;
                    dest_account_person = sa.Customer.Person.Name;
                    dest_account_person_ssn = sa.Customer.Person.SSN;
                    break;
                case ACCOUNT_TYPES.Current_Account:
                    CurrentAccount ca = _ctx.CurrentAccounts.Where(x => x.AId.ToString() == destTransfer).FirstOrDefault();
                    if (ca == null)
                    {
                        MessageBox.Show("Error Message: source account does not exist.");
                        return;
                    }
                    dest_account_bank = ca.Branch.Bank.Name;
                    dest_account_person = ca.Customer.Person.Name;
                    dest_account_person_ssn = ca.Customer.Person.SSN;
                    break;
                case ACCOUNT_TYPES.Deposite_Account:
                    DepositAccount da = _ctx.DepositAccounts.Where(x => x.AId.ToString() == destTransfer).FirstOrDefault();
                    if (da == null)
                    {
                        MessageBox.Show("Error Message: source account does not exist.");
                        return;
                    }
                    dest_account_bank = da.Branch.Bank.Name;
                    dest_account_person = da.Customer.Person.Name;
                    dest_account_person_ssn = da.Customer.Person.SSN;
                    break;
            }
            // --- End Check;


            //-- Transfer the money
            w.transferMoney(srcTransfer, src_account_type, destTransfer, dest_account_type, amountTransfer);
            MessageBox.Show("Transaction Completed Successfully");


            //-- generate recipt
            
            string line0 = "\n===Transaction Report ===\n";
            string line1 = string.Format("+ Requester Name: {0} \n",reqName);
            string line2 = string.Format("+ Transfer Amount: {0} Rials \n",amountTransfer);
            string line3 = string.Format("+ From Bank: {0} to Bank: {1}\n", src_account_bank, dest_account_bank);
            string line4 = string.Format("+ From AId: {0} to AId: {1}\n", srcTransfer, destTransfer);
            string line5 = string.Format("+ Source Account Holder: {0}\n", src_account_person);
            string line6 = string.Format("+ Source Account Holder SSN: {0}\n", src_account_person_ssn);
            string line7 = string.Format("+ Dest Account Holder: {0}\n", dest_account_person);
            string line8 = string.Format("+ Dest Account Holder SSN: {0}\n", dest_account_person_ssn);
            string line9 = "\n===Transaction Report ===\n";
            ResultTxt.Text = line0 + line1 + line2 + line3 + line4 + line5 + line6 + line7 + line8 + line9;

            
        }
    }
}
