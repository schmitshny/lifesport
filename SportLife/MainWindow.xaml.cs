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

namespace SportLife
{

    public partial class MainWindow : Window
    {
        public bool isLoggedIn { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new login();
        }

        private void TDEEbuttonclick(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn == true)
            {
                Main.Content = new tdee();
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You need to log in");
            }
        }

        private void RunningButtonclick(object sender, RoutedEventArgs e)
        {
            if(isLoggedIn==true)
            {
                Main.Content = new running();
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You need to log in");
            }
            
        }
    }
}
