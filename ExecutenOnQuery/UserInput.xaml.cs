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
    /// Interaction logic for USerInput.xaml
    /// </summary>
    public partial class UserInput : Window
    {
        public UserInput()
        {
            InitializeComponent();
        }

        public int OudeLeverancier
        {
            get { return int.Parse(TextBoxOldLev.Text); }
            set { TextBoxOldLev.Text = value.ToString(); }
        }

        public int NieuweLeverancier
        {
            get { return int.Parse(TextBoxNewLev.Text); }
            set { TextBoxNewLev.Text = value.ToString(); }
        }

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}