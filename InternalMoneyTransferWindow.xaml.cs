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
    /// Interaction logic for InternalMoneyTransferWindow.xaml
    /// </summary>
    public partial class InternalMoneyTransferWindow : Window
    {

        public string bank_code { get; set; } // in internal transfer, money transfer within this bank is allowed only!!

        public InternalMoneyTransferWindow(string bank_code)
        {
            this.bank_code = bank_code;
            InitializeComponent();
        }

        private void btn_reset_clicked(object sender, RoutedEventArgs e)
        {
            this.DestinationAccount.Clear();
            this.SourceAccount.Clear();
            this.RequesterName.Clear();
            this.AmountMoney.Clear();
        }

        private void btn_submit_clicked(object sender, RoutedEventArgs e)
        {
            var amountTransfer = this.AmountMoney.Text;
            var srcTransfer = this.SourceAccount.Text;
            var destTransfer = this.DestinationAccount.Text;
            var reqName = this.RequesterName.Text;

            // to do
            MessageBox.Show("Transaction Confirmed");

        }
    }
}
