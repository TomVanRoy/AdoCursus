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
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using AdoGemeenschap;

namespace AdoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonRekeningenAanpassen_Click(object sender, RoutedEventArgs e)
        {
            RekeningenAanpassen newWindow = new RekeningenAanpassen();
            newWindow.Show();
            this.Close();
        }

        private void buttonStorten_Click(object sender, RoutedEventArgs e)
        {
            Storten newWindow = new Storten();
            newWindow.Show();
            this.Close();
        }

        private void buttonOverschrijven_Click(object sender, RoutedEventArgs e)
        {
            Overschrijven newWindow = new Overschrijven();
            newWindow.Show();
            this.Close();
        }

        private void buttonSaldoRekeningRaadplegen_Click(object sender, RoutedEventArgs e)
        {
            SaldoRekeningRaadplegen newWindow = new SaldoRekeningRaadplegen();
            newWindow.Show();
            this.Close();
        }

        private void buttonRekeningInfoRaadplegen_Click(object sender, RoutedEventArgs e)
        {
            RekeningInfoRaadplegen newWindow = new RekeningInfoRaadplegen();
            newWindow.Show();
            this.Close();
        }

        private void buttonOverzichtBrouwers_Click(object sender, RoutedEventArgs e)
        {
            OverzichtBrouwers newWindow = new OverzichtBrouwers();
            newWindow.Show();
            this.Close();
        }
    }
}