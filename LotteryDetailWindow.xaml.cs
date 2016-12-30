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
    /// Interaction logic for LotteryDetailWindow.xaml
    /// </summary>
    public partial class LotteryDetailWindow : Window
    {
        public string lotteryId { get; set; }
        public string date { get; set; }
        public string  title { get; set; }
        public string  winnersCount { get; set; }

        public string desc { get; set; }

        public string branch_code { get; set; }

        public string bank_code { get; set; }

        public Lottery lottery { get; set; }

        public bool lottery_already_done { get; set; }
        public LotteryDetailWindow(string lotteryId, string date, string title, string winnersCount,string desc, string branch_code, string bank_code, Lottery lottery)
        {
            InitializeComponent();
            this.lotteryId = lotteryId;
            this.date = date;
            this.title = title;
            this.winnersCount = winnersCount;
            this.branch_code = branch_code;
            this.bank_code = bank_code;
            this.desc = desc;
            this.lottery = lottery;
            this.lottery_already_done = false;

            LotteryDate.Text = this.date;
            LotteryDesc.Text = this.desc;
            LotteryId.Text = this.lotteryId;
            LotteryTitle.Text = this.title;
            LotteryWinnersCount.Text = this.winnersCount;

            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();

            if(lottery.SavingAccounts1.Any())
            {
                this.lottery_already_done = true ;
                string msg1, msg2;
                foreach(var i in lottery.SavingAccounts1.ToList())
                {
                    
                    msg1 = i.Customer.Person.Name;
                    msg2 = i.Customer.Person.SSN;
                    LotteryWinnersInfo.Text = LotteryWinnersInfo.Text + msg1 + " - " + msg2 + "\n";
                }
                
            }


        }

        private void btn_do_lottery_clicked(object sender, RoutedEventArgs e)
        {
            if(this.lottery_already_done)
            {
                MessageBox.Show("Message: Lottery Already Done");
                return;
            }
            var helper = DbHelper.getInstance();
            List<string> winnerAccounts = helper.addRandomWinnersToLottery(this.lotteryId, Int32.Parse(this.winnersCount));

            MessageBox.Show("Message: Lottery Done");
            string msg1, msg2;
            
            var db = SqlWrapper.getInstance().getDbContext();
            foreach (var accountId in winnerAccounts)
            {
                SavingAccount sa = db.SavingAccounts.Where(x => x.AId.ToString() == accountId).FirstOrDefault();
                msg1 = sa.Customer.Person.Name;
                msg2 = sa.Customer.Person.SSN;
                LotteryWinnersInfo.Text = LotteryWinnersInfo.Text + msg1 + " - " + msg2 + "\n";
            }
        }
    }
}
