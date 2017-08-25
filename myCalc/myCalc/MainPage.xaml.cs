using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Net;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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

            string[] currencyType = File.ReadAllLines("Assets/currencies.txt");
            decimal[] currencyRate = { 1.00m, 1.25129m, 0.847799m, 0.780700m, 59.1626m };

            foreach (var type in currencyType)
            {
                string[] tokens = type.Split(';');
                ComboBox.Items.Add(tokens[0]);
                ComboBox1.Items.Add(tokens[0]);
            }

            foreach (var type in currencyRate)
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

        private async void calc_Click(object sender, RoutedEventArgs e)
        {
            string[] currencyType = { "USD", "CAD", "EUR", "GBP", "RUB" };
            decimal[] currencyRate = { 1.00m, 1.26m, 0.85m, 0.78m, 59.03m };

            try
            {
                if (String.Equals(Convert.ToString(ComboBox.SelectedItem), "USD", System.StringComparison.Ordinal))
                {
                    int index = ComboBox1.Items.IndexOf(Convert.ToString(ComboBox1.SelectedItem));
                    decimal rate = currencyRate[index];

                    decimal amount = sourceUS(Convert.ToDecimal(input.Text), rate);
                    output.Text = Convert.ToString(Math.Round(amount, 2)) + " " + Convert.ToString(ComboBox1.SelectedItem);
                }
                else if (String.Equals(Convert.ToString(ComboBox1.SelectedItem), "USD", System.StringComparison.Ordinal))
                {
                    int index = ComboBox.Items.IndexOf(Convert.ToString(ComboBox.SelectedItem));
                    decimal rate = currencyRate[index];

                    decimal amount = targetUS(Convert.ToDecimal(input.Text), rate);
                    output.Text = Convert.ToString(Math.Round(amount, 2)) + " " + Convert.ToString(ComboBox1.SelectedItem);
                }
                else if (!String.Equals(Convert.ToString(ComboBox.SelectedItem), "USD", System.StringComparison.Ordinal) && !String.Equals(Convert.ToString(ComboBox1.SelectedItem), "USD", System.StringComparison.Ordinal))
                {
                    int i = ComboBox1.Items.IndexOf(Convert.ToString(ComboBox1.SelectedItem));
                    decimal r = currencyRate[i];

                    int index = ComboBox.Items.IndexOf(Convert.ToString(ComboBox.SelectedItem));
                    decimal rate = currencyRate[index];

                    decimal amount = neitherUS(Convert.ToDecimal(input.Text), r, rate);
                    output.Text = Convert.ToString(Math.Round(amount, 2)) + " " + Convert.ToString(ComboBox1.SelectedItem);
                }
                else
                {

                }
            }
            catch
            {
                var dialog = new MessageDialog("There was an error  ¯\\_(ツ)_/¯");
                await dialog.ShowAsync();
            }

            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(output.Text);
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();

        }

        private decimal sourceUS(decimal source, decimal target)
        {
            return source * target;
        }

        private decimal targetUS(decimal source, decimal target)
        {
            return source / target;
        }

        private decimal neitherUS(decimal input, decimal sourcer, decimal targetr)
        {
            decimal converstion = sourcer / targetr;
            return input * converstion;
        }

    }
}