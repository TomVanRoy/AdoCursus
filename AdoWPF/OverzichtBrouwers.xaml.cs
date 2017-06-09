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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AdoWPF
{
    /// <summary>
    /// Interaction logic for OverzichtBrouwers.xaml
    /// </summary>
    public partial class OverzichtBrouwers : Window
    {
        public ObservableCollection<Brouwer> brouwersOb = new ObservableCollection<Brouwer>();
        public List<Brouwer> oudeBrouwers = new List<Brouwer>();
        private CollectionViewSource brouwerViewSource;

        public OverzichtBrouwers()
        {
            InitializeComponent();
        }

        public bool PostCodeFilter(object br)
        {
            Brouwer b = br as Brouwer;
            bool result = (b.Postcode == Convert.ToInt16(comboBoxPostCode.SelectedValue));
            return result;
        }

        private void brouwerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goUpdate();
        }

        private void buttonGo_Click(object sender, RoutedEventArgs e)
        {
            Go();
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

        private void checkBoxPostcode0_Click(object sender, RoutedEventArgs e)
        {
            Binding binding1 = BindingOperations.GetBinding(postcodeTextBox, TextBox.TextProperty);
            binding1.ValidationRules.Clear();

            var binding2 = (postcodeColumn as DataGridBoundColumn).Binding as Binding;
            binding2.ValidationRules.Clear();

            brouwerDataGrid.RowValidationRules.Clear();

            switch (checkBoxPostcode0.IsChecked)
            {
                case true:
                    binding1.ValidationRules.Add(new PostcodeRangeRule0());
                    binding2.ValidationRules.Add(new PostcodeRangeRule0());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule0());
                    break;

                case false:
                    binding1.ValidationRules.Add(new PostcodeRangeRule());
                    binding2.ValidationRules.Add(new PostcodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule());
                    break;

                default:
                    binding1.ValidationRules.Add(new PostcodeRangeRule());
                    binding2.ValidationRules.Add(new PostcodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule());
                    break;
            }
        }

        private void comboBoxPostCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPostCode.SelectedIndex == 0)
            {
                brouwerDataGrid.Items.Filter = null;
            }
            else
            {
                brouwerDataGrid.Items.Filter = new Predicate<object>(PostCodeFilter);
            }
            goUpdate();
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
        }

        private void Go()
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
                    listBoxBrouwers.ScrollIntoView(brouwerDataGrid.SelectedItem);
                }
            }

            textBoxGo.Text = (brouwerViewSource.View.CurrentPosition + 1).ToString();
        }

        private Boolean testOpFouten()
        {
            bool foutGevonden = false;
            foreach (var c in gridDetail.Children)
            {
                if (c is AdornerDecorator)
                {
                    if (Validation.GetHasError(((AdornerDecorator)c).Child))
                    {
                        foutGevonden = true;
                    }
                }
                else if (Validation.GetHasError((DependencyObject)c))
                {
                    foutGevonden = true;
                }
            }

            foreach (var c in brouwerDataGrid.ItemsSource)
            {
                var d = brouwerDataGrid.ItemContainerGenerator.ContainerFromItem(c);
                if (d is DataGridRow)
                {
                    if (Validation.GetHasError(d))
                    {
                        foutGevonden = true;
                    }
                }
            }

            return foutGevonden;
        }

        private void testOpFouten_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                case Key.Enter:
                case Key.Left:
                case Key.Right:
                case Key.Tab:
                case Key.Up:
                    e.Handled = testOpFouten();
                    break;

                default:
                    break;
            }
        }

        private void testOpFouten_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = testOpFouten();
        }

        private void textBoxGo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Go();
            }
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
            brouwersOb = new ObservableCollection<Brouwer>();
            brouwersOb = manager.GetBrouwersBeginNaam(textBoxZoeken.Text);
            brouwerViewSource.Source = brouwersOb;
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
            goUpdate();
            brouwersOb.CollectionChanged += this.OnCollectionChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulGrid();
            textBoxZoeken.Focus();

            var nummers = (from b in brouwersOb orderby b.Postcode select b.Postcode.ToString()).Distinct().ToList();
            nummers.Insert(0, "alles");
            comboBoxPostCode.ItemsSource = nummers;
            comboBoxPostCode.SelectedIndex = 0;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Brouwer oudeBrouwer in e.OldItems)
                {
                    oudeBrouwers.Add(oudeBrouwer);
                }
            }
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            List<Brouwer> resultaatBrouwers = new List<Brouwer>();
            var manager = new BrouwerManager();
            if (oudeBrouwers.Count() != 0)
            {
                resultaatBrouwers = manager.SchrijfVerwijderingen(oudeBrouwers);
                if (resultaatBrouwers.Count > 0)
                {
                    StringBuilder boodschap = new StringBuilder();
                    boodschap.Append("Niet verwijderd: \n");
                    foreach (var b in resultaatBrouwers)
                    {
                        boodschap.Append("Nummer: " + b.BrouwerNr + " : " + b.BrNaam + " niet\n");
                    }
                    MessageBox.Show(boodschap.ToString());
                }
            }
            MessageBox.Show(oudeBrouwers.Count - resultaatBrouwers.Count + $" {((oudeBrouwers.Count - resultaatBrouwers.Count) > 1 ? "Brouwers" : "Brouwer")} verwijderd in de database", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            oudeBrouwers.Clear();
        }
    }
}