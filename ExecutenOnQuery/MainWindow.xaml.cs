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

        private void buttonOpgave02_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave02 newWindow = new WPFOpgave02();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave03_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave03 newWindow = new WPFOpgave03();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave04_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave04 newWindow = new WPFOpgave04();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave05_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave05 newWindow = new WPFOpgave05();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave06_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave06 newWindow = new WPFOpgave06();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave07_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave07 newWindow = new WPFOpgave07();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave08_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave08 newWindow = new WPFOpgave08();
            newWindow.Show();
            this.Close();
        }

        private void buttonOpgave09_Click(object sender, RoutedEventArgs e)
        {
            WPFOpgave09 newWindow = new WPFOpgave09();
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