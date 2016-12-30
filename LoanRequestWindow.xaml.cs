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
    /// Interaction logic for LoanRequestWindow.xaml
    /// </summary>
    public partial class LoanRequestWindow : Window
    {

        public int loanMonthCount { get; set; }
        public int loanInterestRate { get; set; }
        public decimal loanAmount { get; set; }
        public bool loanValid { get; set; }

        public int returningDuration { get; set; }
        public enum InterestRates
        {
            threeMonth=20,
            sixMonth = 22, 
            nineMonth = 24,
            twelveMonth = 26,
            fifteenMonth = 28, 
            eighteenMonth = 30, 
            twentyoneMonth = 32,
            twentyfourMonth = 34
        }

        public LoanRequestWindow()
        {
            this.loanValid = false;
            InitializeComponent();

            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
        }

        private void setInterestRate(int monthCount)
        {


            if(monthCount<3) this.loanInterestRate=(int)InterestRates.threeMonth;
            else if (monthCount >= 3 && monthCount < 6) this.loanInterestRate = (int)InterestRates.threeMonth;
            else if (monthCount >= 6 && monthCount < 9) this.loanInterestRate = (int)InterestRates.sixMonth;
            else if (monthCount >= 9 && monthCount < 12) this.loanInterestRate = (int)InterestRates.nineMonth;
            else if (monthCount >= 12 && monthCount < 15) this.loanInterestRate = (int)InterestRates.twelveMonth;
            else if (monthCount >= 15 && monthCount < 18) this.loanInterestRate = (int)InterestRates.fifteenMonth;
            else if (monthCount >= 18 && monthCount < 21) this.loanInterestRate = (int)InterestRates.eighteenMonth;
            else if (monthCount >= 21 && monthCount < 24) this.loanInterestRate = (int)InterestRates.twentyoneMonth;
            else if (monthCount >= 24) this.loanInterestRate = (int)InterestRates.twentyfourMonth;

            InterestRate.Text = this.loanInterestRate.ToString();
        }

        private void ReturningDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int month = 0;

            ComboBoxItem monthItem = (ComboBoxItem) ReturningDuration.SelectedItem;

            if (Int32.TryParse(monthItem.Tag.ToString(), out month))
            {
                this.loanMonthCount = month;
                this.returningDuration = month;
                setInterestRate(month);
                this.loanValid = true;
            }
            else
            {
                // must never happen!!
                this.loanValid = false;
                MessageBox.Show("FatalError:: When Converting Month String To Int");
            }


        }

        private void CalculateInstallments(object sender, RoutedEventArgs e)
        {
            if(!Validator.validateMoney(LoanAmount.Text))
            {
                MessageBox.Show("Error Message: invalid loan amount");
                return;
            }
            this.loanAmount = decimal.Parse(LoanAmount.Text);
            decimal __totalPayable = ((decimal)this.loanInterestRate / 100m) * (decimal)this.loanAmount + (decimal)this.loanAmount;
            decimal __monthlyInstallment = __totalPayable / (decimal)this.loanMonthCount;

            MonthlyInstallment.Text = __monthlyInstallment.ToString("#.##");
            TotalPayable.Text = __totalPayable.ToString("#.##");

        }

        private void CancelLoan(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FinalizeLoan(object sender, RoutedEventArgs e)
        {
            if(!Validator.IsDigitsOnly(SavingAccountNumber.Text))
            {
                MessageBox.Show("Error Messge: invalid account number"); return;
            }
            // to do: interact with db
            
            var db = DbHelper.getInstance();
            SavingAccount sa = db.getSavingAccDetails(SavingAccountNumber.Text);
            if(sa == null)
            {
                MessageBox.Show("Error Messge: saving account does not exists"); return;
            }
            int id = db.loanRequest(this.loanAmount.ToString(), this.loanInterestRate.ToString(), this.returningDuration.ToString(),sa.BranchId_FK.ToString(),sa.CId_FK.ToString());
            MessageBox.Show(string.Format("Loan Finalized with code [loanId = {0}]",id));
        }


    }
}
