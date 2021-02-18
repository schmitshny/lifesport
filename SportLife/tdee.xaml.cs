using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            databaseEntities db = new databaseEntities();

            var query = (from x in db.MEASURES
                         where x.Id == 1
                         select x).FirstOrDefault();
            
            if(query!=null)
             { 
                weight.Text = query.weight.ToString();
                age.Text = query.age.ToString();
                height.Text = query.height.ToString();
                activityfactor.SelectedIndex = (int)query.activity;
                weightChange.SelectedIndex = (int)query.weightchange;
             }

            var query_2 = (from x in db.macrosandtdee
                           where x.Id == 1
                           select x).FirstOrDefault();

            tdee1.Text = query_2.wtdee.ToString();
            deficyt.Text = query_2.deficyt.ToString();
            needeat.Text = query_2.needeat.ToString();
            protein.Text = query_2.protein.ToString();
            fat.Text = query_2.fat.ToString();
            carbs.Text = query_2.carbs.ToString();

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {




            if (this.weight.Text == "" || this.age.Text == "" || this.height.Text == "" || this.weightChange.Text == "" || this.activityfactor.Text == "" || this.weightChange.Text == "")
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Please enter all data");
            }
            else
            {
                this.tdee1.Text = "dsfsdffs";


                int weight = int.Parse(this.weight.Text);
                int age = int.Parse(this.age.Text);
                int height = int.Parse(this.height.Text);
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

                double tdeee = ((9.99 * weight) + (6.25 * height) - (4.92 * age) + 5) * activity;
                this.tdee1.Text = tdeee.ToString();

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

                this.needeat.Text = (tdeee + def * 1100).ToString();

                this.protein.Text = ((tdeee + def * 0.2) / 4).ToString("F0");
                this.fat.Text = ((tdeee + def * 0.25) / 9).ToString("F0");
                this.carbs.Text = ((tdeee + def * 0.55) / 4).ToString("F0");


                databaseEntities db = new databaseEntities();

                var query = (from x in db.MEASURES
                             where x.Id == 1
                             select x);

                MEASURES obj = query.SingleOrDefault();
                obj.weight = int.Parse(this.weight.Text);
                obj.age = int.Parse(this.age.Text);
                obj.height = int.Parse(this.height.Text);
                obj.activity = activityfactor.SelectedIndex;
                obj.weightchange = weightChange.SelectedIndex;

                /////
                ///
                var query_2 = (from x in db.macrosandtdee
                               where x.Id == 1
                               select x);

                macrosandtdee o = query_2.SingleOrDefault();
                o.wtdee = (int)tdeee ;
                o.deficyt = (int)(def * 1100);
                o.needeat = (int)(tdeee + def * 1100);
                o.protein = (int)((tdeee + def * 0.2) / 4);
                o.fat = (int)((tdeee + def * 0.25) / 9);
                o.carbs = (int)((tdeee + def * 0.55) / 4);


                db.SaveChanges();




            }

            

            

        }

        private void weight_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
           
        }

        private void age_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void height_previewtextinput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


    }
}
