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

namespace Taken
{
    /// <summary>
    /// Interaction logic for WPFOpgave4.xaml
    /// </summary>
    public partial class WPFOpgave07 : Window
    {
        public WPFOpgave07()
        {
            InitializeComponent();
            textBoxNaam.Text = textBoxNaam.Tag.ToString();
            textBoxAdres.Text = textBoxAdres.Tag.ToString();
            textBoxPostcode.Text = textBoxPostcode.Tag.ToString();
            textBoxPlaats.Text = textBoxPlaats.Tag.ToString();
        }

        private void buttonEindejaarskorting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new TuinleverancierManager();
                labelStatus.Content = manager.Eindejaarskorting() + " prijzen aangepast";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }

        private void buttonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string naam = textBoxNaam.Text;
            string adres = textBoxAdres.Text;
            string postcode = textBoxPostcode.Text;
            string plaats = textBoxPlaats.Text;
            try
            {
                var manager = new TuinleverancierManager();
                labelStatus.Content = $"Leverancier met nummer {manager.ToevoegenReturnInt(textBoxNaam.Text, textBoxAdres.Text, textBoxPostcode.Text, textBoxPlaats.Text)} is toegevoegd";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }

        private void buttonVervangLeverancier_Click(object sender, RoutedEventArgs e)
        {
            string nieuw = string.Empty;
            string oud = string.Empty;
            try
            {
                var dialog = new UserInput();
                if (dialog.ShowDialog() == true)
                {
                    nieuw = dialog.TextBoxNewLev.Text;
                    oud = dialog.TextBoxOldLev.Text;
                }
                var manager = new TuinleverancierManager();
                manager.VervangLeverancier(oud, nieuw);
                labelStatus.Content = $"Leverancier {oud} is verwijderd en vervangen door leverancier {nieuw}";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }

        private void textBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text == textbox.Tag.ToString())
            {
                textbox.Text = string.Empty;
                textbox.Foreground = Brushes.Black;
            }
        }

        private void textBox_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text == string.Empty)
            {
                textbox.Text = textbox.Tag.ToString();
                textbox.Foreground = Brushes.Gray;
            }
        }
    }
}