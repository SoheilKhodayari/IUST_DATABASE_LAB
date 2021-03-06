﻿using System;
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
    /// Interaction logic for MainStaffWindow.xaml
    /// </summary>
    public partial class MainBossWindow : Window
    {
        public string staff_name { get; set; }
        public string branch_name { get; set; }

        public string branch_code { get; set; }
        public string bank_name { get; set; }

        public string bank_code { get; set; }
        public string is_central { get; set; } // 1 for true, 0 for false

        public int loginId { get; set; } //boss id in tbl

        public Person person { get; set;}
        public Boss boss { get; set; }

        public Branch branch { get; set; }

        public Bank bank { get; set; }

        public MainBossWindow(Boss boss, Branch branch, Bank bank)
        {
            InitializeComponent();

            this.boss = boss;
            this.branch = branch;
            this.bank = bank;
            this.person = boss.Person;

            this.loginId = this.person.PId; ;
            this.staff_name = this.person.Name;
            this.branch_name = this.branch.Name;
            this.branch_code = this.branch.BranchId.ToString();
            this.bank_name = this.bank.Name;
            this.bank_code = this.bank.BankId.ToString();


            this.TopHeadingStaff.Content = this.bank_name.ToString().ToUpper() + " BANK " + this.branch_name.ToString().ToUpper() + " BRANCH";

            this.LeftHeadingBranchCode.Content = "Branch Code: " + this.branch_code;
            this.LeftHeadingStaffName.Content = "Login: " + this.staff_name;

        }
        private void toggle_staff_name(object sender, RoutedEventArgs e)
        {
            MenuItem i = (MenuItem)sender;
            if (!i.IsChecked)
            {
                this.LeftHeadingStaffName.Content = "Login: " + this.staff_name;

            }
            else
            {
                this.LeftHeadingStaffName.Content = "Login: Anonymous";
            }
        }
        private void go_to_AccountOpeningWindow(object sender, RoutedEventArgs e)
        {
            AccountOpeningWindow account_window = new AccountOpeningWindow(this.branch);
            account_window.Show();
        }

        private void open_AccountManagementInputDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new AccountManagementInputDialog();
            dialog.Show();
        }

        private void open_WithdrawalInputDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new WithdrawelInputDialog();
            dialog.Show();
        }

        private void go_to_InternalTransferMoneyWindow(object sender, RoutedEventArgs e)
        {
            var window = new InternalMoneyTransferWindow(this.bank_code,this.bank_name);
            window.Show();
        }

        private void go_to_ExternalTransferMoneyWindow(object sender, RoutedEventArgs e)
        {
            var window = new ExternalMoneyTransferWindow(this.bank_code);
            window.Show();
        }

        private void go_to_LoanRequestWindow(object sender, RoutedEventArgs e)
        {
            var window = new LoanRequestWindow();
            window.Show();
        }

        private void go_to_CreditCardIssuanceWindow(object sender, RoutedEventArgs e)
        {
            var window = new CreditCardIssueWindow();
            window.Show();
        }

        private void go_to_CheckIssuanceWindow(object sender, RoutedEventArgs e)
        {
            var window = new CheckIssuanceWindow();
            window.Show();
        }

        private void go_to_CheckClearingWindow(object sender, RoutedEventArgs e)
        {
            var window = new CheckClearingWindow();
            window.Show();
        }

        private void go_to_InterestWindow(object sender, RoutedEventArgs e)
        {
            var window = new InterestWindow();
            window.Show();
        }

        private void go_to_ForeignCurrencygWindow(object sender, RoutedEventArgs e)
        {
            var window = new ForeignCurrencyWindow();
            window.Show();
        }

        private void go_to_PresentWorkingHoursWindow(object sender, RoutedEventArgs e)
        {
             // absenceCount of staffId = this.loginId
            int absenceCount;
            int? cnt = boss.AbsenceCount;
            if (cnt == null) absenceCount = 0;
            else absenceCount = (int)cnt;
            var window = new PresentWorkingHoursWindow(absenceCount);
            window.Show();
        }
        private void go_to_LoginWindow(object sender, RoutedEventArgs e)
        {
            var w = new LoginWindow();
            w.Show();
            this.Close();
        }
        public void exit_all(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void go_to_ManageSaffWorkingHoursWindow(object sender, RoutedEventArgs e)
        {
            var w = new ManageStaffHoursWindow(this.branch_code);
            w.Show();
        }

        private void go_to_MessageWindow(object sender, RoutedEventArgs e)
        {
            var w = new BossMessagesWindow(this.loginId, this.bank_code);
            w.Show();
        }
        private void set_branch_central_status()
        {
             // set  this.is_central based on db and bank_code and branch_code
            if(this.branch.IsCentral)
            {
                this.is_central = "1";
            }else
            {
                this.is_central = "0";
            }
            
        }
        private void go_to_CreateLotteryCentralBranch(object sender , RoutedEventArgs e)
        {
            if(this.is_central == null)
            {
                set_branch_central_status();
            }
            if(this.is_central=="1")
            {
                var w = new CreateLotteryOfCentralBranches(this.branch_code);
                w.Show();
            }
            else
            {
                MessageBox.Show("You Are Not Authorized to Create Lottery, Only Central Branches Have Neccessary Permissions!!");
            }


        }

        private void go_to_LotteryListView(object sender, RoutedEventArgs e)
        {
            var w = new LotteryListView(this.branch_code,this.bank_code);
            w.Show();
        }

    }
}
