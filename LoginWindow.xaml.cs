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
        public LoginWindow()
        {
            InitializeComponent();

            BOSS_UsernameTxtBox.Text = "7877745111";
            BOSS_PasswordTxtBox.Password = "8CCZPV0Z";
            BOSS_BranchCodeTxtBox.Text = "1";

            STAFF_BranchCodeTxtBox.Text = "1";
            STAFF_PasswordTxtBox.Password = "JDBNELKS";
            STAFF_UsernameTxtBox.Text = "3255645685";

        }



        private void AdminArea_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_sumbit_boss_clicked(object sender, RoutedEventArgs e)
        {
            string username = BOSS_UsernameTxtBox.Text;
            string password = BOSS_PasswordTxtBox.Password;
            string branch_code = BOSS_BranchCodeTxtBox.Text;

            if (username.Equals("") || password.Equals("") || branch_code.Equals(""))
            {
                MessageBox.Show("Error Message: Form can not be empty.");
                return;
            }
            if (!Validator.IsDigitsOnly(username))
            {
                MessageBox.Show("Error Message: username must be an SSN which is numeral");
                return;
            }
            if(!Validator.IsDigitsOnly(branch_code))
            {
                MessageBox.Show("Error Message: Branch code can not contain characters.");
                return;
            }

            DbHelper helper = DbHelper.getInstance();
            Boss boss_instance  = helper.getBossBySSN(username);
            if(boss_instance == null)
            {
                MessageBox.Show("Wrong Credentials, Please Try Again");
                return;
            }
            if(boss_instance.SystemPassword == password && boss_instance.BranchId_FK.ToString() == branch_code)
            {
                BankDBContext ctx = helper.getDbContext();
                Branch branch = ctx.Branches.Find(boss_instance.BranchId_FK);
                Bank bank = branch.Bank;
                Boss boss = ctx.Bosses.Find(boss_instance.BId);
                MainBossWindow window = new MainBossWindow(boss, branch, bank);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Credentials, Please Try Again");
                return;
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

            if (username.Equals("") || password.Equals("") || branch_code.Equals(""))
            {
                MessageBox.Show("Error Message: Form can not be empty.");
                return;
            }
            if (!Validator.IsDigitsOnly(username))
            {
                MessageBox.Show("Error Message: username must be an SSN which is numeral");
                return;
            }
            if (!Validator.IsDigitsOnly(branch_code))
            {
                MessageBox.Show("Error Message: Branch code can not contain characters.");
                return;
            }

            DbHelper helper = DbHelper.getInstance();
            Staff staff_instance = helper.getStaffBySSN(username);
            if (staff_instance == null)
            {
                MessageBox.Show("Wrong Credentials, Please Try Again");
                return;
            }
            if (staff_instance.SystemPassword == password && staff_instance.BranchId_FK.ToString() == branch_code)
            {
                BankDBContext ctx = helper.getDbContext();
                Branch branch = ctx.Branches.Find(staff_instance.BranchId_FK);
                Bank bank = branch.Bank;
                Staff staff = ctx.Staffs.Find(staff_instance.SId);
                MainStaffWindow window = new MainStaffWindow(staff, branch, bank);
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Credentials, Please Try Again");
                return;
            }

        }
    }
}
