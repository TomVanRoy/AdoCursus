using AdoGemeenschap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace Taken
{
    /// <summary>
    /// Interaction logic for WPFOpgave10.xaml
    /// </summary>
    public partial class WPFOpgave10 : Window
    {
        private CollectionViewSource leverancierViewSource;
        public List<Leverancier> gewijzigdeLeveranciers = new List<Leverancier>();
        public List<Leverancier> nieuweLeveranciers = new List<Leverancier>();
        public List<Leverancier> oudeLeveranciers = new List<Leverancier>();
        public ObservableCollection<Leverancier> leverancierOb = new ObservableCollection<Leverancier>();

        public WPFOpgave10()
        {
            InitializeComponent();
        }

        private void goUpdate()
        {
            if (leverancierDataGrid.Items.Count != 0)
            {
                if (leverancierDataGrid.SelectedItem != null)
                {
                    leverancierDataGrid.ScrollIntoView(leverancierDataGrid.SelectedItem);
                }
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                MessageBox.Show("net een item verwijderd");
                foreach (Leverancier oudeLeverancier in e.OldItems)
                {
                    oudeLeveranciers.Add(oudeLeverancier);
                }
            }
            if (e.NewItems != null)
            {
                MessageBox.Show("net een item toegevoegd");
                foreach (Leverancier nieuweLeverancier in e.NewItems)
                {
                    nieuweLeveranciers.Add(nieuweLeverancier);
                }
            }
        }

        private void VulDataGrid()
        {
            leverancierViewSource = (CollectionViewSource)(this.FindResource("leverancierViewSource"));
            var manager = new LeverancierManager();
            leverancierOb = manager.GetLeveranciers();
            leverancierViewSource.Source = leverancierOb;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulDataGrid();
            VulComboBox();
            leverancierOb.CollectionChanged += this.OnCollectionChanged;
        }

        private void VulComboBox()
        {
            var nummers = (from b in leverancierOb orderby b.PostNr select b.PostNr.ToString()).Distinct().ToList();
            nummers.Insert(0, "alles");
            comboBoxPostNummer.ItemsSource = nummers;
            comboBoxPostNummer.SelectedIndex = 0;
        }

        private void comboBoxPostNummer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPostNummer.SelectedIndex != 0)
            {
                leverancierDataGrid.Items.Filter = new Predicate<object>(PostnummerFilter);
            }
            else
            {
                leverancierDataGrid.Items.Filter = null;
            }
        }

        public bool PostnummerFilter(object lev)
        {
            Leverancier l = lev as Leverancier;
            bool result = (l.PostNr == comboBoxPostNummer.SelectedValue.ToString());
            return result;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u alles wegschrijven naar de database ?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                leverancierDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                var manager = new LeverancierManager();
                List<Leverancier> resultaatLeveranciers = new List<Leverancier>();
                StringBuilder gelukt = new StringBuilder();
                StringBuilder gefaald = new StringBuilder();
                if (oudeLeveranciers.Count > 0)
                {
                    resultaatLeveranciers = manager.SchrijfVerwijderingen(oudeLeveranciers);
                    if (resultaatLeveranciers.Count > 0)
                    {
                        gefaald.AppendLine("niet verwijderd:");
                        foreach (Leverancier l in resultaatLeveranciers)
                        {
                            gefaald.AppendLine(l.LevNr + " : " + l.Naam);
                        }
                    }
                    gelukt.AppendLine(oudeLeveranciers.Count - resultaatLeveranciers.Count + " " + (oudeLeveranciers.Count - resultaatLeveranciers.Count > 1 ? "leveranciers" : "leverancier") + " verwijderd van de database");
                }

                if (nieuweLeveranciers.Count > 0)
                {
                    resultaatLeveranciers = manager.SchrijfToevoegingen(nieuweLeveranciers);
                    if (resultaatLeveranciers.Count > 0)
                    {
                        gefaald.AppendLine("niet toegevoegd:");
                        foreach (Leverancier l in resultaatLeveranciers)
                        {
                            gefaald.AppendLine(l.LevNr + " : " + l.Naam);
                        }
                    }
                    gelukt.AppendLine(nieuweLeveranciers.Count - resultaatLeveranciers.Count + " " + (nieuweLeveranciers.Count - resultaatLeveranciers.Count > 1 ? "leveranciers" : "leverancier") + " toegevoegd aan de database");
                }

                foreach (Leverancier l in leverancierOb)
                {
                    if ((l.Changed == true) && (l.LevNr != 0))
                    {
                        gewijzigdeLeveranciers.Add(l);
                        l.Changed = false;
                    }
                }

                resultaatLeveranciers.Clear();
                MessageBox.Show(gewijzigdeLeveranciers.Count.ToString());
                if (gewijzigdeLeveranciers.Count > 0)
                {
                    MessageBox.Show("Meer dan 1 gewijzigde");
                    resultaatLeveranciers = manager.SchrijfWijzigingen(gewijzigdeLeveranciers);
                    if (resultaatLeveranciers.Count > 0)
                    {
                        MessageBox.Show("Meer dan 1 Schrijfwijziging result");
                        gefaald.AppendLine("Niet gewijzigd:");
                        foreach (var l in resultaatLeveranciers)
                        {
                            gefaald.AppendLine(l.LevNr + " : " + l.Naam);
                        }
                    }
                    gelukt.AppendLine(gewijzigdeLeveranciers.Count - resultaatLeveranciers.Count + " leverancier(s) gewijzigd in de database");
                }
                if ((gelukt.ToString() != string.Empty) || (gefaald.ToString() != string.Empty))
                {
                    MessageBox.Show(gelukt.ToString() + (gefaald.ToString() != string.Empty ? "\n\n" : "") + gefaald.ToString(), "Info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }
                oudeLeveranciers.Clear();
                nieuweLeveranciers.Clear();
                gewijzigdeLeveranciers.Clear();

                leverancierViewSource = ((CollectionViewSource)this.FindResource("leverancierViewSource"));
                leverancierOb = manager.GetLeveranciers();
                leverancierViewSource.Source = leverancierOb;
            }
        }
    }
}