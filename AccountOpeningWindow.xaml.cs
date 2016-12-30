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
    /// Interaction logic for AccountOpeningWindow.xaml
    /// </summary>
    public partial class AccountOpeningWindow : Window
    {
        public Branch branch { get; set; }

        public AccountOpeningWindow(Branch branch)
        {

            InitializeComponent();
            this.branch = branch;
            this.BranchCodeTxt.Text = branch.BranchId.ToString();
        }


        private void cancel_form_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void reset_form_click(object sender, RoutedEventArgs e)
        {
            NameTxt.Clear();
            SSNTxt.Clear();
            BirthdateTxt.Text = "yyyy-mm-dd";
            MaleCh.IsChecked = false;
            FemaleCh.IsChecked = false;
            phoneTxt.Clear();
            mobileTxt.Clear();
            addressTxt.Clear();
            PrimaryBalanceTxt.Clear();
        }

        private void submit_form_click(object sender, RoutedEventArgs e)
        {
            
            string Name = NameTxt.Text;
            string SSN = SSNTxt.Text;
            string birthdate = BirthdateTxt.Text;
            string gender;
            if(this.MaleCh.IsChecked ?? false)
            {
                gender = "0";
            }
            else
            {
                gender = "1";
            }
            string phone = phoneTxt.Text;
            string mobile = mobileTxt.Text;
            string address = addressTxt.Text;


            string primary_balance = this.PrimaryBalanceTxt.Text;

            if(!Validator.validateChar(Name))
            {
                MessageBox.Show("Error Message: Invalid Name parameter.");
                return;
            }
            if(!Validator.IsDigitsOnly(SSN))
            {
                MessageBox.Show("Error Message: Invalid SSN parameter."); return;
            }
            if (!Validator.validateDate(birthdate))
            {
                MessageBox.Show("Error Message: Invalid birthDate inserted."); return;
            }
            if (!Validator.validatePhone(phone))
            {
                MessageBox.Show("Error Message: Invalid Phone number structure."); return;
            }
            if (!Validator.validatePhone(mobile))
            {
                MessageBox.Show("Error Message: Invalid mobile number structure."); return;
            }
            if (address.Equals(""))
            {
                MessageBox.Show("Error Message: Invalid mobile number structure."); return;
            }
            if (!Validator.validateMoney(primary_balance))
            {
                MessageBox.Show("Error Message: Invalid primary balance"); return;
            }
            if ((bool)this.MaleCh.IsChecked == false && (bool)this.FemaleCh.IsChecked == false)
            {
                    MessageBox.Show("Error Message: Gender not specified"); return;
            }
            ACCOUNT_TYPES account_type = ACCOUNT_TYPES.Saving_Account;
            var selected_account_obj = this.AccountTypeCombo.SelectedValue;
            if (selected_account_obj == null)
            {
                MessageBox.Show("Account Type Not Selected"); return;
            }
            string selected_account = selected_account_obj.ToString();
            if (selected_account == "Deposit") account_type = ACCOUNT_TYPES.Deposite_Account;
            else if (selected_account == "Current") account_type = ACCOUNT_TYPES.Current_Account;
            else if (selected_account == "ForeignCurrency") account_type = ACCOUNT_TYPES.Foreign_Currency_Account;
            else if (selected_account == "Saving") account_type = ACCOUNT_TYPES.Saving_Account;

            /* all form inputs passed the check */
            DbHelper helper = DbHelper.getInstance();
            int result = helper.openAccount(Name, SSN, gender, birthdate, phone, mobile, address, account_type, primary_balance, this.branch.BranchId.ToString());
            if(result<0)
            {
                MessageBox.Show("Operation unsuccessfull, Check Entries!!");
            }else
            {
                MessageBox.Show("Operation successful");
                this.Close();
            }

        }

        private void Male_Checked(object sender, RoutedEventArgs e)
        {
            this.FemaleCh.IsChecked = false;
        }
        private void Female_Checked(object sender, RoutedEventArgs e)
        {
            this.MaleCh.IsChecked = false;
        }


    }
}
