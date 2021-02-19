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
    /// <summary>
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Page
    {
        public register()
        {
            InitializeComponent();
        }

        private void registeruser_Click(object sender, RoutedEventArgs e)
        {
            if(username.Text.Length==0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Enter a login");
                username.Focus();
            }
            else if(pass.Password.Length==0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Enter a password");
                pass.Focus();
            }
            else if (confirmpass.Password.Length==0)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Enter a password");
                confirmpass.Focus();
            }
            else if(pass.Password!=confirmpass.Password)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Passwords are not the same");
                pass.Focus();
            }
            else
            {
                databaseEntities db = new databaseEntities();
                users newuser = new users()
                {
                    login = username.Text,
                    password = pass.Password
                };

                db.users.Add(newuser);
                db.SaveChanges();
                Xceed.Wpf.Toolkit.MessageBox.Show("Registration completed");
            }
        }
    }
}
