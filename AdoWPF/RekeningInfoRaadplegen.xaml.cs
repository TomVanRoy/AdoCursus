﻿using System;
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
    /// Interaction logic for RekeningInfoRaadplegen.xaml
    /// </summary>
    public partial class RekeningInfoRaadplegen : Window
    {
        public RekeningInfoRaadplegen()
        {
            InitializeComponent();
        }

        private void buttonInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new RekeningenManager();
                var info = manager.RekeningInfoRaadplegen(textBoxRekNr.Text);
                labelKlantNaam.Content = info.Saldo.ToString("N");
                labelSaldo.Content = info.KlantNaam;
                labelStatus.Content = string.Empty;
            }
            catch (Exception ex)
            {
                labelSaldo.Content = string.Empty;
                labelKlantNaam.Content = string.Empty;
                labelStatus.Content = ex.Message;
            }
        }
    }
}