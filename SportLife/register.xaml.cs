using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SportLife
{
    /// <summary>
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="register"/> class.
        /// </summary>
        public register()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Registers new user, stores user login and password in database
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        private void registeruser_Click(object sender, RoutedEventArgs e)
        {
            databaseEntities db = new databaseEntities();

            var usernameexists = from d in db.users
                                 where d.login == username.Text
                                 select d.login;

            if (username.Text.Length == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Enter a login");
                username.Focus();
            }
            else if (pass.Password.Length == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Enter a password");
                pass.Focus();
            }
            else if (confirmpass.Password.Length == 0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Enter a password");
                confirmpass.Focus();
            }
            else if (pass.Password != confirmpass.Password)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Passwords are not the same");
                pass.Focus();
            }
            else if (usernameexists!=null)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Username is already taken");
                username.Focus();
            }
            else
            {
                users newuser = new users()
                {
                    login = username.Text,
                    password = pass.Password
                };

                db.users.Add(newuser);
                db.SaveChanges();
                Xceed.Wpf.Toolkit.MessageBox.Show("Registration completed");
                var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;
                mw.Main.Content = new login();
            }
        }
    }
}