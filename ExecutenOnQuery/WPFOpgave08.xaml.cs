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
    /// Interaction logic for WPFOpgave8.xaml
    /// </summary>
    public partial class WPFOpgave08 : Window
    {
        public WPFOpgave08()
        {
            InitializeComponent();
        }

        private void FillComboBox()
        {
            try
            {
                var manager = new TuinManager();
                comboBoxSoort.DisplayMemberPath = "SoortNaam";
                comboBoxSoort.SelectedValuePath = "SoortNr";
                comboBoxSoort.ItemsSource = manager.GetSoorten();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
        }

        private void comboBoxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                listBoxPlanten.Items.Clear();
                int soortNr = Convert.ToInt32(comboBoxSoort.SelectedValue);
                var manager = new TuinManager();
                var allePlanten = manager.GetPlanten(soortNr);
                foreach (var plant in allePlanten)
                {
                    listBoxPlanten.Items.Add(plant);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}