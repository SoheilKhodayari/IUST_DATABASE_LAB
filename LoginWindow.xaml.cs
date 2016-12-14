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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
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

                /* do login from db here */
                var bank_name = "SADERAT";
                var branch_name = "NARMAK";
                var staff_name = "ALI NASIRI";
                int staffId = 45;
                var bank_code = "bankId";

                MainStaffWindow window = new MainStaffWindow(staffId, bank_name,branch_name,bank_code,branch_code,staff_name);
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
