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
    /// Interaction logic for PresentWorkingHoursWindow.xaml
    /// </summary>
    public partial class PresentWorkingHoursWindow : Window
    {
        public int absenceCount { get; set; }
        public PresentWorkingHoursWindow(int cnt)
        {
            
            InitializeComponent();
            this.absenceCount = cnt;
            AbsenceCount.Text = String.Format("{0}", this.absenceCount);
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
