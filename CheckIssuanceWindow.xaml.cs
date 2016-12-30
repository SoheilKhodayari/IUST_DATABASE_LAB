using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for CheckIssuanceWindow.xaml
    /// </summary>
    public partial class CheckIssuanceWindow : Window
    {
        public CheckIssuanceWindow()
        {
            InitializeComponent();
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
        }

        private void btn_cancel_clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_issue_clicked(object sender, RoutedEventArgs e)
        {

            string AccountNo = AccountNumber.Text;
            if (!Validator.IsDigitsOnly(AccountNo))
            {
                MessageBox.Show("Error Message: Invalid Current Account Number.");
                return;
            }
            DbHelper helper = DbHelper.getInstance();
            CurrentAccount ca1 = helper.getCurrentAccDetails(AccountNo);
            CurrentAccount ca2 = helper.getDbContext().CurrentAccounts.Where(a => a.AId.ToString() == AccountNo).FirstOrDefault();
            if(ca1 == null || ca2 == null)
            {
                MessageBox.Show("Error Message: Account number does not exist.");
                return;
            }


            var result = helper.checkIssuance(AccountNo);
            if(result.Count() == 0)
            {
                MessageBox.Show("Error Message: Check for this account number has already been issued");
                return;
            }
            
            // -- generate recipt;
            string recipt = string.Format("===Check Generation===\n\nAccount number:{0}\nCheck Id: {1}\nExpirationDate: {2} \nNumber of Papers: {3}\n\n===Check Generation===",result[0],result[1],result[2],result[3]);
            MessageBox.Show("Check Issuance Confirmed!");
            IssuanceResult.Text = recipt;
        }
    }
}
