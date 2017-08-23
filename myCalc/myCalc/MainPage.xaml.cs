using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace myCalc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            string[] currencyType = { "USD", "CAD", "EUR", "GBP", "RUB" };
            decimal[] currencyRate = { 1.00m, 1.26m, 0.85m, 0.78m, 59.03m };

            foreach (var type in currencyType)
            {
                sourceCurrency.Items.Add(type);
                targetCurrency.Items.Add(type);
            }

            foreach (var type in currencyType)
            {
                rate.Items.Add(type);
            }

            /* Code for grabbing current exchange rates from the Web
            try
            {
                string xmlResult = null;
                string url;
                url = "http://www.webservicex.net/CurrencyConvertor.asmx/ConversionRate?FromCurrency=" + comboBox.SelectedItem + "&ToCurrency=" + comboBox1.SelectedItem + "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = await WebRequest.GetResponseAsync();
                StreamReader resStream = new StreamReader(response.GetResponseStream());
                XmlDocument doc = new XmlDocument();
                xmlResult = resStream.ReadToEnd();
                doc.LoadXml(xmlResult);
                output.Text = "Current Exchange Rate for " + Convert.ToString(comboBox.SelectedItem).ToUpper() + " ---> " + Convert.ToString(comboBox1.SelectedItem).ToUpper() + " value " + doc.GetElementsByTagName("double").Item(0).InnerText;
            }
            catch (Exception ex)
            {
                output.Text = "Not a valid Currency or Try again later";
            }
            */
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void calc_Click(object sender, RoutedEventArgs e)
        {
            string[] currencyType = { "USD", "CAD", "EUR", "GBP", "RUB" };
            decimal[] currencyRate = { 1.00m, 1.26m, 0.85m, 0.78m, 59.03m };

            int sourceIndex = sourceCurrency.SelectedIndex;
            int conversionRate = rate[sourceIndex];

            output = input.Text * rate[sourceIndex];

        }

        private void sourceUS()
        {
            decimal convertedAmount;
            convertedAmount = Convert.ToDecimal(input.Text) * Convert.ToDecimal(rate.SelectedItem);
        }

        private void targetUS()
        {
            decimal convertedAmount;
            convertedAmount = Convert.ToDecimal(input.Text) / Convert.ToDecimal(rate.SelectedItem);
        }

        private void neitherUS()
        {
            decimal convertedAmount;
            convertedAmount = Convert.ToDecimal(input.Text) * sourceRate;
            convertedAmount = convertedAmount / Convert.ToDecimal(rate.SelectedItem);
        }
    }
}