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
    /// Interaction logic for CreateLotteryOfCentralBranches.xaml
    /// </summary>
    public partial class CreateLotteryOfCentralBranches : Window
    {
        public bool participateAllSavingAccounts {get;set;}
        public List<string> participantAccounts { get; set; }

        public string branch_code { get; set; }
        public CreateLotteryOfCentralBranches(string branch_code)
        {
            InitializeComponent();
            this.participateAllSavingAccounts = false;
            this.participantAccounts = new List<string>();
            this.branch_code = branch_code;

            BranchIdHeading.Content = "BranchId: " + this.branch_code;
            
        }

        private void btn_submit_lottery_creation_clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Lottery Created!");
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
            if(AId != "")
            {
                if(AId != "asasd")
                {
                    this.participantAccounts.Add(AId);
                    MessageBox.Show("Participant Added!!");
                }
                else
                {
                    MessageBox.Show("Error:: Unvalid input given, AId should be a Number!!");
                }  
            }
            else
            {
                MessageBox.Show("Error:: Unvalid input given, AId can not be empty");
            }            
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
