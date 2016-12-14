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

        }

        private void PresentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("present confirmed!!");
            CheckBox ch = (CheckBox)sender;
            ch.IsEnabled = false;
        }
    }
}
