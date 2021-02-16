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
    /// Interaction logic for tdee.xaml
    /// </summary>
    public partial class tdee : Page
    {
        public tdee()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.weight.Text == "" || this.age.Text == "" || this.height.Text == "" || this.weightChange.Text == "" || this.activityfactor.Text == "" || this.weightChange.Text == "")
            {
                MessageBox.Show("Please enter all data");
            }
            else
            {
                this.tdee1.Text = "dsfsdffs";


                double weight = int.Parse(this.weight.Text);
                double age = int.Parse(this.age.Text);
                double height = int.Parse(this.height.Text);
                double activity;

                switch (activityfactor.SelectedIndex)
                {
                    case 0:
                        activity = 1.2;
                        break;
                    case 1:
                        activity = 1.375;
                        break;
                    case 2:
                        activity = 1.55;
                        break;
                    case 3:
                        activity = 1.725;
                        break;
                    default:
                        activity = 1.5;
                        break;
                }

                double tdee = ((9.99 * weight) + (6.25 * height) - (4.92 * age) + 5) * activity;
                this.tdee1.Text = tdee.ToString();

                double def;

                switch (weightChange.SelectedIndex)
                {
                    case 0:
                        def = -1.5;
                        break;
                    case 1:
                        def = -1;
                        break;
                    case 2:
                        def = -0.5;
                        break;
                    case 3:
                        def = 0;
                        break;
                    case 4:
                        def = 0.5;
                        break;
                    case 5:
                        def = 1;
                        break;
                    case 6:
                        def = 1.5;
                        break;
                    default:
                        def = 0;
                        break;
                }

                this.deficyt.Text = (def * 1100).ToString();

                this.needeat.Text = (tdee + def * 1100).ToString();

                this.protein.Text = ((tdee + def * 0.2) / 4).ToString("F0");
                this.fat.Text = ((tdee + def * 0.25) / 9).ToString("F0");
                this.carbs.Text = ((tdee + def * 0.55) / 4).ToString("F0");
            }
        }
    }
}
