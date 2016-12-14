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
    /// Interaction logic for AccountManagementInputDialog.xaml
    /// </summary>
    public partial class WithdrawelInputDialog : Window
    {
        public WithdrawelInputDialog()
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
            // DialogResult = true;

            string accountId = this.ResponseText;
            bool account_exists = !accountId.Equals(""); // to be changed with db check
            if (account_exists)
            {

                //---------------------------to be fetch from db ---------------------------//
                string AccountType = "Saving Account";
                string AccountBranchCode = "551521";
                string AccountBalance = "2500000";
                string AccountOpeningDate = "25-09-1996";
                string CustomerName = "Soheil Khodayari";
                string CustomerSSN = "0355546825";

                //--------------------------------------------------------------------------//
                var wid = new WithdrawlDepositForm(accountId, CustomerName, CustomerSSN, AccountBalance , AccountBranchCode, AccountOpeningDate,AccountType);
                wid.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error:: Invalid Account Id");
            }
        }

    }
}
