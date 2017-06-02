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
using AdoGemeenschap;

namespace AdoWPF
{
    /// <summary>
    /// Interaction logic for Overschrijven.xaml
    /// </summary>
    public partial class Overschrijven : Window
    {
        public Overschrijven()
        {
            InitializeComponent();
        }

        private void ButtonOverschrijven_Click(object sender, RoutedEventArgs e)
        {
            Decimal bedrag;
            if (Decimal.TryParse(TextBoxBedrag.Text.ToString(), out bedrag))
            {
                try
                {
                    var manager = new RekeningenManager();
                    manager.Overschrijven(bedrag, TextBoxVanRekening.Text, TextBoxNaarRekening.Text);
                    LabelStatus.Content = "Ok";
                }
                catch (Exception ex)
                {
                    LabelStatus.Content = ex;
                }
            }
            else
            {
                LabelStatus.Content = "Bedrag bevat geen geldig getal";
            }
        }
    }
}