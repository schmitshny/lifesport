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
using Xceed.Wpf.Toolkit;

namespace SportLife
{
    /// <summary>
    /// Interaction logic for running.xaml
    /// </summary>
    public partial class running : Page
    {
        databaseEntities db = new databaseEntities();

        public running()
        {
            InitializeComponent();

            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var stats = from d in db.statisticRuns
                        where d.userID == mw.currentuserID
                     select d;
            

            this.gridruns.ItemsSource = stats.ToList();
            startingDate.SelectedDate = new DateTime(2021,01,01);
            endDate.SelectedDate = DateTime.Today;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;


            if (this.distTextbox.Text == "" || this.timeTextBox.Text == "")
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Please enter all data");
                
            }
            else
            {

            
            statisticRuns newRecord = new statisticRuns()
            {
                distance = int.Parse(distTextbox.Text),
                time = TimeSpan.ParseExact(timeTextBox.Text, "hh\\:mm\\:ss", null),
                date = Calendar.SelectedDate,
                userID = mw.currentuserID
                
            };

            db.statisticRuns.Add(newRecord);
            db.SaveChanges();

                var stats = from d in db.statisticRuns
                            where d.userID == mw.currentuserID
                            select d;


                this.gridruns.ItemsSource = stats.ToList();
            }
        }




        private int updatingRunID = 0;



        private void gridruns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.gridruns.SelectedIndex>=0)
            {
                if(this.gridruns.SelectedItems.Count>=0)
                {
                    if(this.gridruns.SelectedItems[0].GetType()==typeof(statisticRuns))
                    {
                        statisticRuns x = (statisticRuns)this.gridruns.SelectedItems[0];
                        this.distTextboxUpadte.Text = x.distance.ToString(); ;
                        this.timeTextboxUpdate.Text = x.time.ToString();
                        this.DateUpdate.SelectedDate = x.date;
                        this.updatingRunID = x.Id;
                    }
                }    
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var x = from d in db.statisticRuns
                    where d.Id == this.updatingRunID
                    select d;

            statisticRuns obj = x.SingleOrDefault();

            if(obj!=null)
            {
                obj.distance = int.Parse(distTextboxUpadte.Text);
                obj.time = TimeSpan.ParseExact(timeTextboxUpdate.Text, "hh\\:mm\\:ss", null);
                obj.date = DateUpdate.SelectedDate;
                db.SaveChanges();
            }
            var stats = from d in db.statisticRuns
                        where d.userID == mw.currentuserID
                        select d;


            this.gridruns.ItemsSource = stats.ToList();
        }


        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var x = from d in db.statisticRuns
                    where d.Id == this.updatingRunID
                    select d;

            statisticRuns obj = x.SingleOrDefault();
            if (obj != null)
            {
                db.statisticRuns.Remove(obj);
                db.SaveChanges();
            }
            var stats = from d in db.statisticRuns
                        where d.userID == mw.currentuserID
                        select d;


            this.gridruns.ItemsSource = stats.ToList();
        }



        private void distTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


        private void startingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var stats = from d in db.statisticRuns
                        where d.date >= startingDate.SelectedDate && d.date <=endDate.SelectedDate && d.userID==mw.currentuserID
                        select d;

            this.gridruns.ItemsSource = stats.ToList();

        }

        private void endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var stats = from d in db.statisticRuns
                        where d.date >= startingDate.SelectedDate && d.date <= endDate.SelectedDate && d.userID == mw.currentuserID
                        select d;

            this.gridruns.ItemsSource = stats.ToList();
        }
    }
}
