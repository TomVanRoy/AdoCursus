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
    /// Interaction logic for WPFOpgave09.xaml
    /// </summary>
    public partial class WPFOpgave09 : Window
    {
        private string GeselecteerdeSoortNaam;
        private List<Plant> gewijzigdePlanten = new List<Plant>();
        private List<Plant> listBoxPlantenLijst = new List<Plant>();

        public WPFOpgave09()
        {
            InitializeComponent();
        }

        private void WijzigingenOpslaan()
        {
            // MessageBox.Show("1");
            var gewijzigdePlanten = new List<Plant>();

            foreach (Plant p in listBoxPlantenLijst)
            {
                if (p.Changed == true)
                {
                    gewijzigdePlanten.Add(p);
                    p.Changed = false;
                }
            }

            if ((gewijzigdePlanten.Count > 0) && (MessageBox.Show("Gewijzigde planten van soort " + GeselecteerdeSoortNaam + " opslaan?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes))
            {
                var manager = new TuinManager();
                try
                {
                    manager.GewijzigdePlantenOpslaan(gewijzigdePlanten);
                    MessageBox.Show("Planten opgeslagen", "Opslaan", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Er is een fout opgetreden: " + ex.Message, "Opslaan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void buttonOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (!PlantHasErrors())
            {
                WijzigingenOpslaan();
            }
        }

        private bool PlantHasErrors()
        {
            bool foutGevonden = false;
            if (Validation.GetHasError(textBoxKleur) || Validation.GetHasError(textBoxPrijs))
            {
                foutGevonden = true;
            }
            return foutGevonden;
        }

        private void comboBoxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WijzigingenOpslaan();
            GeselecteerdeSoortNaam = ((Soort)comboBoxSoort.SelectedItem).SoortNaam;
            try
            {
                var manager = new TuinManager();
                listBoxPlantenLijst = manager.GetPlanten(Convert.ToInt32(comboBoxSoort.SelectedValue));
                listBoxPlanten.ItemsSource = listBoxPlantenLijst;
                listBoxPlanten.DisplayMemberPath = "PlantNaam";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void listBoxPlanten_Selected(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
        }

        private void All_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PlantHasErrors())
            {
                e.Handled = true;
            }
        }

        private void All_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.Enter:
                case Key.Left:
                case Key.Right:
                case Key.Tab:
                case Key.Up:
                    if (PlantHasErrors())
                    {
                        e.Handled = true;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}