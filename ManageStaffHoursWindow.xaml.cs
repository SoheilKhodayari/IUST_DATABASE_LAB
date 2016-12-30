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
    /// Interaction logic for ManageStaffHoursWindow.xaml
    /// </summary>
    public partial class ManageStaffHoursWindow : Window
    {
        public string branch_code { get; set; }
        public ManageStaffHoursWindow(string branch_code)
        {
            InitializeComponent();
            this.branch_code = branch_code;
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
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
            if(!Validator.validateChar(staffName))
            {
                MessageBox.Show("Error Message: invalid name parameter");
                return;
            }
            if(!Validator.IsDigitsOnly(staffSSN))
            {
                MessageBox.Show("Error Message: invalid ssn parameter");
                return;
            }
            var helper = DbHelper.getInstance();
            Staff s = helper.getStaffBySSN(staffSSN);
            if(s == null)
            {
                MessageBox.Show("Error Message: no such staff with SSN specified");
                return;
            }
            if(s.BranchId_FK.ToString() != this.branch_code)
            {
                MessageBox.Show("Error Message: You are only authorized to set absence for your own staff (in your branch)!!");
                return;
            }
            Person p = helper.getPersonBySSN(staffSSN);
            if(p.Name != staffName)
            {
                MessageBox.Show("Error Message: Name and SSN does not match!!");
                return;
            }
            int status = helper.addAbsence(staffSSN);
            if (status < 0) MessageBox.Show("FatalError: try again");
            MessageBox.Show("Absence Has Been Set Successfully");
        }
    }
}
