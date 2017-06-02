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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using AdoGemeenschap;

namespace Taken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WPFOpgave03 : Window
    {
        public WPFOpgave03()
        {
            InitializeComponent();
        }

        private void ButtonEindejaarskorting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuinleverancierManager();
                LabelResult.Content = manager.Eindejaarskorting() + " prijzen aangepast";
            }
            catch (Exception ex)
            {
                LabelResult.Content = ex.Message;
            }
        }

        private void ButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            String naam = TextBoxNaam.Text;
            String adres = TextBoxAdres.Text;
            String postcode = TextBoxPostcode.Text;
            String plaats = TextBoxPlaats.Text;
            try
            {
                var manager = new TuinleverancierManager();

                if (manager.Toevoegen(TextBoxNaam.Text, TextBoxAdres.Text, TextBoxPostcode.Text, TextBoxPlaats.Text))
                {
                    LabelResult.Content = "Nieuwe leverancier is toegevoegd";
                }
                else
                {
                    LabelResult.Content = "Leverancier kon niet aangemaakt worden";
                }
            }
            catch (Exception ex)
            {
                LabelResult.Content = ex.Message;
            }
        }
    }
}