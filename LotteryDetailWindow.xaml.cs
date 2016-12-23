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
        public LotteryDetailWindow(string lotteryId, string date, string title, string winnersCount,string desc, string branch_code, string bank_code)
        {
            InitializeComponent();
            this.lotteryId = lotteryId;
            this.date = date;
            this.title = title;
            this.winnersCount = winnersCount;
            this.branch_code = branch_code;
            this.bank_code = bank_code;
            this.desc = desc;
            
            LotteryDate.Text = this.date;
            LotteryDesc.Text = this.desc;
            LotteryId.Text = this.lotteryId;
            LotteryTitle.Text = this.title;
            LotteryWinnersCount.Text = this.winnersCount;
        }

        private void btn_do_lottery_clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Lottery Done");
            LotteryWinnersInfo.Text = "Soheil Khodayari - Account number: 6512651502";  // to be done with db operations
        }
    }
}
