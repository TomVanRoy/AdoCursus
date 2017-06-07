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
    /// Interaction logic for OverzichtBrouwers.xaml
    /// </summary>
    public partial class OverzichtBrouwers : Window
    {
        public List<Brouwer> brouwersOb = new List<Brouwer>();

        public OverzichtBrouwers()
        {
            InitializeComponent();
        }

        private void buttonZoeken_Click(object sender, RoutedEventArgs e)
        {
            VulGrid();
        }

        private void VulGrid()
        {
            CollectionViewSource brouwerViewSource = ((CollectionViewSource)(this.FindResource("brouwerViewSource")));
            var manager = new BrouwerManager();
            List<Brouwer> brouwersOb = new List<Brouwer>();
            brouwersOb = manager.GetBrouwersBeginNaam(textBoxZoeken.Text);
            brouwerViewSource.Source = brouwersOb;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulGrid();
            textBoxZoeken.Focus();
        }

        private void textBoxZoeken_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VulGrid();
            }
        }
    }
}