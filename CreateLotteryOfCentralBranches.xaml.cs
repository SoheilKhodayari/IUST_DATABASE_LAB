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
    /// Interaction logic for CreateLotteryOfCentralBranches.xaml
    /// </summary>
    public partial class CreateLotteryOfCentralBranches : Window
    {
        public bool participateAllSavingAccounts {get;set;}
        public List<SavingAccount> participantAccounts { get; set; }

        public string branch_code { get; set; }
        public CreateLotteryOfCentralBranches(string branch_code)
        {
            InitializeComponent();
            this.participateAllSavingAccounts = false;
            this.participantAccounts = new List<SavingAccount>();
            this.branch_code = branch_code;

            BranchIdHeading.Content = "BRANCH ID: " + this.branch_code;
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
            
        }

        private void btn_submit_lottery_creation_clicked(object sender, RoutedEventArgs e)
        {
            if(LotteryName.Text.Equals(""))
            {
                MessageBox.Show("Error Message: lottery name can not be empty");
                return;
            }
            if(!Validator.validateDate(LotteryDate.Text))
            {
                MessageBox.Show("Error Message: invalid lottery date specified");
                return;
            }
            if(!Validator.IsDigitsOnly(LotteryWinnersCount.Text))
            {
                MessageBox.Show("Error Message: invalid input for number of winners specified");
                return;
            }
            string desc = string.Format("NoWinners:{0}_\n" + LotteryDescription.Text, LotteryWinnersCount.Text);
            var helper = DbHelper.getInstance();
            int id = helper.createLottery(LotteryName.Text, LotteryDate.Text, desc, branch_code);

            if(this.participateAllSavingAccounts)
            {
                helper.addAllAccountAsParticipator(id.ToString());
            }else
            {
               foreach(SavingAccount sa in this.participantAccounts)
               {
                   helper.addParticipator(id.ToString(), sa.AId.ToString());
               }
            }

            MessageBox.Show(string.Format("Lottery Created with Code [LotteryId={0}]",id));
            this.Close();
        }

        private void btn_cancel_lottery_creation_clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_reset_lottery_creation_clicked(object sender, RoutedEventArgs e)
        {
            LotteryName.Text = "";
            LotteryDate.Text = "";
            LotteryDescription.Text = "";
            LotteryWinnersCount.Text = "";
        }

        private void btn_add_particpant_account_clicked(object sender, RoutedEventArgs e)
        {
            var AId = LotteryParticipantAId.Text;
            if(!Validator.IsDigitsOnly(AId))
            {
                MessageBox.Show("Error:: invalid input given, AId should be a number!!"); return;
            }
            var helper = DbHelper.getInstance();
            SavingAccount sa = helper.getSavingAccDetails(AId);
            if(sa == null)
            {
                MessageBox.Show("Error Message: specified account does not exist."); return;
            }
            
            this.participantAccounts.Add(sa);
            MessageBox.Show("Participant registered and will be added after lottery creation");
            LotteryParticipantAId.Text = "";

        }

        private void LotteryAddAll_Checked(object sender, RoutedEventArgs e)
        {
            this.participateAllSavingAccounts = true;
            MessageBox.Show("All Saving Accounts Selected");
        }
        private void LotteryAddAll_UnChecked(object sender, RoutedEventArgs e)
        {
            this.participateAllSavingAccounts = false;
            MessageBox.Show("All Saving Accounts Unselected");
        }
    }
}
