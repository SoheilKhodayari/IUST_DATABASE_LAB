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
        }

        private void btn_calculate_clicked(object sender, RoutedEventArgs e)
        {
            // TO DO: call a webservice to convert values , then set amount text box
            DestAmount.Text = "5000000";
        }
    }
}
