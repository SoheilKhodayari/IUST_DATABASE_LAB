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
    /// Interaction logic for ManageStaffHoursWindow.xaml
    /// </summary>
    public partial class ManageStaffHoursWindow : Window
    {
        public string branch_code { get; set; }
        public ManageStaffHoursWindow(string branch_code)
        {
            InitializeComponent();
            this.branch_code = branch_code;
        }

        private void btn_reset_clicked(object sender, RoutedEventArgs e)
        {
            StaffName.Text = "";
            StaffSSN.Text = "" ;
        }

        private void btn_cancel_clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_submit_clicked(object sender, RoutedEventArgs e)
        {
            var staffName = StaffName.Text;
            var staffSSN = StaffSSN.Text;
            MessageBox.Show("form submitted!");
        }
    }
}
