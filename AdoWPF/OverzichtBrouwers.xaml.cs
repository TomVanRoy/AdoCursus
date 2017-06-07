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
        private CollectionViewSource brouwerViewSource;

        public OverzichtBrouwers()
        {
            InitializeComponent();
        }

        private void buttonGoToFirst_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToFirst();
            goUpdate();
        }

        private void buttonGoToLast_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToLast();
            goUpdate();
        }

        private void buttonGoToNext_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToNext();
            goUpdate();
        }

        private void buttonGoToPrevious_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToPrevious();
            goUpdate();
        }

        private void buttonZoeken_Click(object sender, RoutedEventArgs e)
        {
            VulGrid();
        }

        private void goUpdate()
        {
            buttonGoToPrevious.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);
            buttonGoToFirst.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);
            buttonGoToNext.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);
            buttonGoToLast.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);

            if (brouwerDataGrid.Items.Count != 0)
            {
                if (brouwerDataGrid.SelectedItem != null)
                {
                    brouwerDataGrid.ScrollIntoView(brouwerDataGrid.SelectedItem);
                }
            }

            textBoxGo.Text = (brouwerViewSource.View.CurrentPosition + 1).ToString();
        }

        private void textBoxZoeken_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VulGrid();
            }
        }

        private void VulGrid()
        {
            brouwerViewSource = ((CollectionViewSource)(this.FindResource("brouwerViewSource")));
            var manager = new BrouwerManager();
            List<Brouwer> brouwersOb = new List<Brouwer>();
            brouwersOb = manager.GetBrouwersBeginNaam(textBoxZoeken.Text);
            brouwerViewSource.Source = brouwersOb;
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
            goUpdate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulGrid();
            textBoxZoeken.Focus();
        }

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            int position;
            int.TryParse(textBoxGo.Text, out position);
            if (position > 0 && position <= brouwerDataGrid.Items.Count)
            {
                brouwerViewSource.View.MoveCurrentToPosition(position - 1);
            }
            else
            {
                MessageBox.Show("The input index is not valid");
            }
            goUpdate();
        }

        private void brouwerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goUpdate();
        }
    }
}