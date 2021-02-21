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
    ///<summary>
    ///Interaction logic for MainWindow
    ///</summary>
    public partial class MainWindow : Window
    {

        /// <summary>isLoggedin property represents whether the user is logged in </summary>
        /// <value>isLoggedin property set initiali false, because user has to log in</value>
        public bool isLoggedIn { get; set; } = false;
        /// <summary>isLoggedin property represents current user ID </summary>
        public int currentuserID { get; set; }


        /// <summary>
        /// Constructor for Main window. Loading login page.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new login();
        }

        /// <summary>
        /// changes the current page to TDEE calculations
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
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
        /// <summary>
        /// Changes the current page to Timer
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        private void timer_Click(object sender, RoutedEventArgs e)
        {
            if(isLoggedIn==true)
            {
                Main.Content = new timer();
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You need to log in");

            }

        }
        /// <summary>
        /// Changes the current page to Running calendar
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
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
        /// <summary>
        /// Changes the bool isLoggedin to false, user is logged out
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        private void logoutbutton_Click(object sender, RoutedEventArgs e)
        {
            isLoggedIn = false;
            Xceed.Wpf.Toolkit.MessageBox.Show("You are now logged out");
            Main.Content = new login();
        }
    }
}
