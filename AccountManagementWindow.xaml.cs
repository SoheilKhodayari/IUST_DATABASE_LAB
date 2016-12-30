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
        public string _Phone
        {
            get { return phone.Text; }
            set { phone.Text = value; }
        }
        public string _Mobile
        {
            get { return mobile.Text; }
            set { mobile.Text = value; }
        }
        public string _Address
        {
            get { return address.Text; }
            set { address.Text = value; }
        }

        public string PureBalance { get; set; }

        public Person person;
        public AccountManagementWindow(string accountId,string _AccountType, string _AccountBranchCode, string _AccountOpeningDate, string _AccountBalance)
        {

            InitializeComponent();

            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
            this._accountId = accountId;
            this._AccountType = _AccountType;
            this._AccountBranchCode = _AccountBranchCode;
            this._AccountOpeningDate = _AccountOpeningDate;
            this.LeftHeadingAccount.Content = "ACCOUNT NUMBER: " + this._accountId;
            DbHelper helper = DbHelper.getInstance();
            BankDBContext ctx = helper.getDbContext();

            if (_AccountType == "Saving Account")
            {
                this._AccountBalance = _AccountBalance + " Rials";
                SavingAccount account = ctx.SavingAccounts.Find(Int32.Parse(accountId));
                BankLabel.Content = "BANK NAME: " + account.Branch.Bank.Name;
                this.person = account.Customer.Person;
                _Address = this.person.Address;
                List<Person_Phone> phones = ctx.Person_Phone.Where(p => p.PId_FK == this.person.PId).ToList();
                foreach(var record in phones)
                {
                    if(record.Phone.StartsWith("09"))
                    {
                        _Mobile = record.Phone;
                    }
                    else
                    {
                        _Phone = record.Phone;
                    }
                    if(Validator.validatePhone(_Phone) && Validator.validatePhone(_Mobile))
                    {
                        break;
                    }
                }

            }
            else if (_AccountType == "Deposit Account")
            {
                this._AccountBalance = _AccountBalance + " Rials";
                DepositAccount account = ctx.DepositAccounts.Find(Int32.Parse(accountId));
                BankLabel.Content = "BANK NAME: " + account.Branch.Bank.Name;
                this.person = account.Customer.Person;
                _Address = this.person.Address;
                List<Person_Phone> phones = ctx.Person_Phone.Where(p => p.PId_FK == this.person.PId).ToList();
                foreach (var record in phones)
                {
                    if (record.Phone.StartsWith("09"))
                    {
                        _Mobile = record.Phone;
                    }
                    else
                    {
                        _Phone = record.Phone;
                    }
                    if (Validator.validatePhone(_Phone) && Validator.validatePhone(_Mobile))
                    {
                        break;
                    }
                }

            }
            else if (_AccountType == "Foreign Currency Account")
            {
                this._AccountBalance = _AccountBalance + " Dollars";
                ForeignCurrencyAccount account = ctx.ForeignCurrencyAccounts.Find(Int32.Parse(accountId));
                BankLabel.Content = "BANK NAME: " + account.Branch.Bank.Name;
                this.person = account.Customer.Person;
                _Address = this.person.Address;
                List<Person_Phone> phones = ctx.Person_Phone.Where(p => p.PId_FK == this.person.PId).ToList();
                foreach (var record in phones)
                {
                    if (record.Phone.StartsWith("09"))
                    {
                        _Mobile = record.Phone;
                    }
                    else
                    {
                        _Phone = record.Phone;
                    }
                    if (Validator.validatePhone(_Phone) && Validator.validatePhone(_Mobile))
                    {
                        break;
                    }
                }
            }
            else
            {
                this._AccountBalance = _AccountBalance + " Rials";
                CurrentAccount account = ctx.CurrentAccounts.Find(Int32.Parse(accountId));
                BankLabel.Content = "BANK NAME: " + account.Branch.Bank.Name;
                this.person = account.Customer.Person;
                _Address = this.person.Address;
                List<Person_Phone> phones = ctx.Person_Phone.Where(p => p.PId_FK == this.person.PId).ToList();
                foreach (var record in phones)
                {
                    if (record.Phone.StartsWith("09"))
                    {
                        _Mobile = record.Phone;
                    }
                    else
                    {
                        _Phone = record.Phone;
                    }
                    if (Validator.validatePhone(_Phone) && Validator.validatePhone(_Mobile))
                    {
                        break;
                    }
                }
            }

            //BankLabel.Content = 


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
