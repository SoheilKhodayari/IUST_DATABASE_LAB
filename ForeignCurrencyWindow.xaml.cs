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
    /// Interaction logic for ForeignCurrencyWindow.xaml
    /// </summary>
    public partial class ForeignCurrencyWindow : Window
    {
        public ForeignCurrencyWindow()
        {
            InitializeComponent();
            DateLabel.Content = "TIME: " + DateTime.Now.ToString("hh:mm:ss tt");
            TimeLabel.Content = "DATE: " + DateTime.Now.ToShortDateString();
        }
        private double GetConvertedCurrencyValue(string inputCurrency, string outputCurrency, double value) 
        {
            string request = string.Format("http://www.xe.com/ucc/convert.cgi?Amount={0}&From={1}&To={2}", value, inputCurrency, outputCurrency);

            System.Net.WebClient wc = new System.Net.WebClient();
            string apiResponse = wc.DownloadString(request);    // This is a blocking operation.
            wc.Dispose();
            /* Formatting */
            // Typical response: "XE.com: curr1 to curr2 rate: x curr1 = y curr2"
            // The first part, up until "x curr1" is basically a constant
            string header = string.Format("XE.com: {0} to {1} rate:", inputCurrency, outputCurrency);

            // Removing the header
            // The response now looks like this: x curr1 = y curr2
            apiResponse = apiResponse.Replace(header, "");

            // Let's split the response at '=', to retrieve the right part
            string outValue = apiResponse.Split('=')[1];

            // Getting rid of the 'curr2' part
            outValue = outValue.Replace(outputCurrency, "");

            return Double.Parse(outValue, System.Globalization.CultureInfo.InvariantCulture);
        }
        private void btn_calculate_clicked(object sender, RoutedEventArgs e)
        {
            // TO DO: call a webservice to convert values , then set amount text box
            string src = SourceAmount.Text;
            int type_convert = 0;
            decimal dollar_rate=4125 ;
            decimal euro_rate=4315;
            if(!Validator.validateMoney(src))
            {
                MessageBox.Show("Error Message: invalid source amount"); return;
            }
            var selected_account_obj = this.MoneyTypeCombo.SelectedValue;
            if (selected_account_obj == null)
            {
                MessageBox.Show("Money Type Not Selected"); return;
            }
            string selected_type = selected_account_obj.ToString();
            if (selected_type != "DOLLAR-US" && selected_type!="EURO")
            {
                MessageBox.Show("Message: This type of money for conversion is currently unavailabe, only EURO/DOLLAR-US is available now"); return;
            }
            decimal rials = decimal.Parse(src);
            decimal result ;
            if (selected_type == "DOLLAR-US")
            {
                type_convert = 0;
                rials = rials * dollar_rate;
            }
            else if (selected_type == "EURO")
            {
                type_convert = 1;
                rials = rials * euro_rate;
            }

            
           DestAmount.Text = rials.ToString();
           
            //string src ="EUR";
            //string target = "DOL";
            //double c;
            //c = GetConvertedCurrencyValue(src, target, 500);
        }
    }
}
