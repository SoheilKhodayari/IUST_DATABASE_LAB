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
using System.IO;
using WpfApplication1.Models;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for BossMessagesWindow.xaml
    /// </summary>
    public partial class BossMessagesWindow : Window
    {
        public int bossId { get; set; }
        public string bankId { get; set; }

        public int index { get; set; }


        public List<List<string>> getBossMessages()
        {
            List<List<string>> messages = new List<List<string>>();

            //List<string> msgItem1 = new List<string>( new string [] {"1" , "Interest Notice" ,"01-05-2015", "Content1"});
            //List<string> msgItem2 = new List<string>(new string[] { "2", "Emails Notice", "7-09-2016", "Content2" });
            //List<string> msgItem3 = new List<string>(new string[] { "3", "New Notice", "7-09-2016", "Content3" });
            //List<string> msgItem4 = new List<string>(new string[] { "4", "New Notice", "7-09-2016", "Content4" });

            //messages.Add(msgItem1);
            //messages.Add(msgItem2);
            //messages.Add(msgItem3);
            //messages.Add(msgItem4);
            List<Message> mList = SqlWrapper.getInstance().getDbContext().Messages.Where(x => x.BId_FK == bossId).ToList();
            List<string> stringObjWrapper;
            foreach(var msg in mList)
            {
                stringObjWrapper = new List<string>();
                stringObjWrapper.Add(msg.MsgId.ToString());
                stringObjWrapper.Add(msg.Title);
                DateTime date = (DateTime)msg.Date;
                stringObjWrapper.Add(date.ToString("yyyy-MM-dd"));
                stringObjWrapper.Add(msg.Content.ToString());
                messages.Add(stringObjWrapper);
            }
            return messages;
        }

        public BossMessagesWindow(int bossId, string bankId)
        {
            InitializeComponent();
            this.bossId = bossId;
            this.bankId = bankId;
            this.index = 1;

            var layoutInbox = InboxGrid;

            List<message> msgWindow = new List<message>(); // keep refrence of all windows;
            StackPanel p = new StackPanel();
            foreach(var msgItem in this.getBossMessages())
            {
                message m = new message(this.index, msgItem[0], msgItem[1], msgItem[2],msgItem[3]);
                msgWindow.Add(m);
                Grid.SetRow(m, this.index - 1);
                Grid.SetColumn(m, 0);
                p.Children.Add(m);
                this.index += 1;
                
            }
            Grid.SetRow(p, 0);
            Grid.SetColumn(p, 0);
            layoutInbox.Children.Add(p);

           
        }

        public void open_selected_message(object sender, RoutedEventArgs e, string msgId,string title,string date,string content)
        {
            MessageIdTxtBox.Text = msgId;
            TitleTxtBox.Text = title;
            DateTxtBox.Text = date;
            ContentTxtBox.Text = content;
        }
    }
}
