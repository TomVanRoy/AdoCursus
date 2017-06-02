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
    /// Interaction logic for WPFOpgave5.xaml
    /// </summary>
    public partial class WPFOpgave05 : Window
    {
        public WPFOpgave05()
        {
            InitializeComponent();
        }

        private void buttonBereken_Click(object sender, RoutedEventArgs e)
        {
            var manager = new TuinleverancierManager();
            try
            {
                labelStatus.Content = "€ " + manager.BerekenGemiddeldeKostprijs(textBoxSoort.Text).ToString("N");
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }
    }
}