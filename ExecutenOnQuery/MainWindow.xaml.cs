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

namespace Taken
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

        private void buttonOpgave2_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave2 newWindow = new WPFOpgave2();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave3_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave3 newWindow = new WPFOpgave3();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave4_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave4 newWindow = new WPFOpgave4();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave5_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave5 newWindow = new WPFOpgave5();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave6_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave6 newWindow = new WPFOpgave6();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave7_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave7 newWindow = new WPFOpgave7();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave8_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave8 newWindow = new WPFOpgave8();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave9_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave9 newWindow = new WPFOpgave9();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave10_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave10 newWindow = new WPFOpgave10();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave11_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave11 newWindow = new WPFOpgave11();
            newWindow.Show();
            this.Close();
        }
    }
}