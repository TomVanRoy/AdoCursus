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
    /// Interaction logic for Storten.xaml
    /// </summary>
    public partial class Storten : Window
    {
        public Storten()
        {
            InitializeComponent();
        }

        private void buttonStorten_Click(object sender, RoutedEventArgs e)
        {
            decimal teStorten;
            if (decimal.TryParse(textBoxTeStorten.Text, out teStorten))
            {
                try
                {
                    var manager = new RekeningenManager();
                    if (manager.Storten(teStorten, textBoxRekeningNr.Text))
                    {
                        labelStatus.Content = "OK";
                    }
                    else
                    {
                        labelStatus.Content = "Rekening niet gevonden";
                    }
                }
                catch (Exception ex)
                {
                    labelStatus.Content = ex.Message;
                }
            }
            else
            {
                labelStatus.Content = "Tik een getal bij het storten";
            }
        }
    }
}