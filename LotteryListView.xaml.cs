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
    /// Interaction logic for ActiveLotteryListView.xaml
    /// </summary>
    public partial class LotteryListView : Window
    {

        public string branch_code { get; set; }
        public string bank_code { get; set; }
        public List<List<string>> FetchAllLotteries()
        {
            List<List<string>> AllLotteries = new List<List<string>>();

            // LotteryItem list includes :    <lottery id, date, name>
            //List<string> lotteryItem1 = new List<string>(new string[] {"1","05-10-2015","Jashnvare-Shemsh-Tala-95"});
            //List<string> lotteryItem2 = new List<string>(new string[] { "2", "05-10-2014", "Jashnvare-Shemsh-Tala-96" });
            //List<string> lotteryItem3 = new List<string>(new string[] { "3", "05-10-2016", "Jashnvare-Shemsh-Tala-94" });
            //List<string> lotteryItem4 = new List<string>(new string[] { "4", "05-10-2015", "Jashnvare-Shemsh-Tala-92" });
            //AllLotteries.Add(lotteryItem1);
            //AllLotteries.Add(lotteryItem2);
            //AllLotteries.Add(lotteryItem3);
            //AllLotteries.Add(lotteryItem4);

            var helper = DbHelper.getInstance();
            List<Lottery> all = helper.getAllLotteries(this.branch_code);
            List<string> lotteryItem;
            foreach(var lot in all)
            {
                DateTime time = (DateTime)lot.Date;

                lotteryItem = new List<string>(new string[] { lot.LotteryId.ToString(), 
                                                            time.ToString("yyyy-MM-dd"), 
                                                            lot.Name });

                AllLotteries.Add(lotteryItem);
            }


            return AllLotteries;
        }

        public List<string> findAllLotteryInfo(string lotteryId)
        {
            // note: noOfWinners can be saved in description field when storing lottery;
            // if not saved , pass the string "noOfWinnersUnspecifed" for example
            var helper = DbHelper.getInstance();
            Lottery lottery = helper.getLottery(lotteryId);
            DateTime date = (DateTime)lottery.Date;
            string[] tokens = lottery.Description.Split(new[] { "_" }, StringSplitOptions.None);
            string[] noWinners = tokens[0].Split(':');
            string desc = tokens[0];
            if (noWinners.Count() >1)
            {
                return new List<string>(new string[] { lottery.LotteryId.ToString(),
                                                  lottery.Name,
                                                  date.ToString("yyyy-MM-dd"),
                                                  lottery.Description, // description
                                                  noWinners[1] }); // no of winners
            }
            else
            {
                return new List<string>(new string[] { lottery.LotteryId.ToString(),
                                                  lottery.Name,
                                                  date.ToString("yyyy-MM-dd"),
                                                  lottery.Description, // description
                                                  "1" }); // no of winners ,default
            }
        }
        public LotteryListView(string branch_code, string bank_code)
        {
            InitializeComponent();
            this.bank_code = bank_code;
            this.branch_code = branch_code;

            var helper = DbHelper.getInstance();
            var db = helper.getDbContext();

            BranchIdHeading.Content = "BRANCH ID: " + this.branch_code;
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();

            var all = this.FetchAllLotteries();
            if (all[0].Count() != 0)
            {
                this.LotteryPrimaryMessage.Visibility = Visibility.Collapsed;
            }

            var index = 0;
                
            foreach(List<string> lotteryItem in all)
            {
                Label LotteryId  = new Label  { Content = lotteryItem[0]};
                Label Date = new Label { Content = lotteryItem[1] };
                Label Title = new Label { Content = lotteryItem[2] };
                Button lotteryDetailsBtn = new Button { Content = "View Details", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Width = 75};
                
                lotteryDetailsBtn.Click += (s, e) =>
                {
                    List<string> item = this.findAllLotteryInfo(lotteryItem[0]);
                    string _id = item[0];
                    Lottery lot = db.Lotteries.Where(x => x.LotteryId.ToString() == _id).FirstOrDefault();
                    string _title = item[1];
                    string _date = item[2];
                    string _desc = item[3];
                    string _noWinner = item[4];
                    var w = new LotteryDetailWindow(_id,_date,_title,_noWinner,_desc,this.branch_code,this.bank_code,lot);
                    w.Show();
                };

                LotteryId.Margin = new System.Windows.Thickness(40, 55 + index, index, index);
                Date.Margin = new System.Windows.Thickness(100, 55 + index, index, index);
                Title.Margin = new System.Windows.Thickness(200, 55 + index, index, index);
                lotteryDetailsBtn.Margin = new System.Windows.Thickness(500, 55 +  index, index, index);

                MainGrid.Children.Add(LotteryId);
                MainGrid.Children.Add(Date);
                MainGrid.Children.Add(Title);
                MainGrid.Children.Add(lotteryDetailsBtn);
                index += 35;

            }

        }
    }
}
