using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SportLife
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="login"/> class.
        /// </summary>
        public login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets current page to registration page
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Registerpage_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            mw.Main.Content = new register();
        }

        /// <summary>
        /// Chechs if the username and password are corrent and if they are user is logged in
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
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