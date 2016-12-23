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
    /// Interaction logic for message.xaml
    /// </summary>
    public partial class message : UserControl
    {
        public int index;
        public string msgId;
        public string title;
        public string date;
        public string content;

        public message(int index, string msgId, string title, string date, string content)
        {
            InitializeComponent();
            this.index = index;
            this.msgId = msgId;
            this.title = title;
            this.date = date;
            this.content = content;

            Index.Content = string.Format("{0})", this.index);
            MsgId.Content = this.msgId;
            Title.Content = this.title;
            Date.Content = this.date;

        }

        private void open_message(object sender, RoutedEventArgs e)
        {
            // bubble up the message to the parent window , together with message content
            var panel = (StackPanel)this.Parent;
            var grid = (Grid)panel.Parent;
            grid = (Grid)grid.Parent;
            var border = (Border)grid.Parent;
            grid = (Grid)border.Parent;
            var window = (BossMessagesWindow)grid.Parent;
            window.open_selected_message(sender, e, this.msgId,this.title,this.date,this.content);
        }
    }
}
