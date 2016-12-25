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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.Models;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class LoginWindow : Window
    {
        private BankDBContext _db;
        public LoginWindow()
        {
            InitializeComponent();
            _db = new BankDBContext();
        }


        private void AdminArea_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sumbit_boss_clicked(object sender, RoutedEventArgs e)
        {
            string username = BOSS_UsernameTxtBox.Text;
            string password = BOSS_PasswordTxtBox.Password;
            string branch_code = BOSS_BranchCodeTxtBox.Text;


            if (!username.Equals("") && !password.Equals("") && !branch_code.Equals(""))
            {

                /* do login from db here */
                var bank_name = "SADERAT";
                var branch_name = "VALIASR";
                var staff_name = "SADEGH KARIMIYAN";
                int staffId = 45;
                var bank_code = "bankId";

                MainBossWindow window = new MainBossWindow(staffId, bank_name, branch_name, bank_code, branch_code, staff_name);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Credentials, Please Try Again");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void reset_login_staff(object sender, RoutedEventArgs e)
        {
            STAFF_BranchCodeTxtBox.Clear();
            STAFF_UsernameTxtBox.Clear();
            STAFF_PasswordTxtBox.Clear();
        }

        private void submit_login_staff(object sender, RoutedEventArgs e)
        {
            string username = STAFF_UsernameTxtBox.Text;
            string password = STAFF_PasswordTxtBox.Password;
            string branch_code = STAFF_BranchCodeTxtBox.Text;


            if(!username.Equals("") && !password.Equals("") && !branch_code.Equals(""))
            {
//                string authenticationQuery = @"Select p.PId,p.SSN,p.Name,s.BranchId_FK, s.SystemPassword From Staff as s inner join Person as p on s.SId = p.PId
//                where p.SSN = '{0}' and s.SystemPassword = '{1}' ";
//                authenticationQuery = string.Format(authenticationQuery, username, password);
//                var matchedCredentialRow = _db.Database.SqlQuery<List<string>>(authenticationQuery).FirstOrDefault();
//                if(matchedCredentialRow == null || matchedCredentialRow.Count() == 0)
//                {
//                    MessageBox.Show("Wrong Credentials");
//                    return;
//                }
//                /* Logged In, Fetch Other Properties  */
//                string otherDataQuery = @"Select ba.BankId,ba.Name,br.Name,br.IsCentral from Branch as br inner join Bank as ba on br.BankId_FK = ba.BankId
//                where BranchId = {0}";
//                otherDataQuery = string.Format(otherDataQuery, matchedCredentialRow[3]);
//                var otherDataRow = _db.Database.SqlQuery<List<string>>(otherDataQuery).FirstOrDefault();

//                if(otherDataQuery == null)
//                {
//                    MessageBox.Show("Fatal error detected, Try again!!");
//                    return;
//                }

//                var bank_code = otherDataRow[0];
//                var bank_name = otherDataRow[1];
//                var branch_name = otherDataRow[2];
//                var is_central = otherDataRow[3];
//                var staff_name = matchedCredentialRow[2];
//                int staffId = Int32.Parse(matchedCredentialRow[0]);

                /* do login from db here */
                var bank_name = "SADERAT";
                var branch_name = "NARMAK";
                var staff_name = "ALI NASIRI";
                int staffId = 45;
                var bank_code = "bankId";
                string is_central = "1";

                MainStaffWindow window = new MainStaffWindow(staffId, bank_name,branch_name,bank_code,branch_code,staff_name,is_central);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Credentials, Please Try Again");
            }
            

        }
    }
}
