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
using System.Data.OleDb;

namespace SportLife
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }

        private void registerpage_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            mw.Main.Content = new register();

        }

        

        private void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            databaseEntities db = new databaseEntities();

            var myUser = db.users.FirstOrDefault(u => u.login == usernametextbox.Text && u.password == passwordtextbox.Password);

            if (myUser != null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("You are logged in");
                var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;
                mw.isLoggedIn = true;
                mw.currentuserID = myUser.Id;
            }
            else
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Invalid password or username");
            }

        }
    }
}
