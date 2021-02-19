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
        public running()
        {
            InitializeComponent();

            databaseEntities db = new databaseEntities();

            var stats = from d in db.RUNSSTATISTICS
                        select d;
            /*
            select new
            {
                Distance = d.distance,
                Time = d.time,
                Date = d.date
            };*/

            this.gridruns.ItemsSource = stats.ToList();
            startingDate.SelectedDate = new DateTime(2021,01,01);
            endDate.SelectedDate = DateTime.Today;

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            databaseEntities db = new databaseEntities();

            if (this.distTextbox.Text == "" || this.timeTextBox.Text == "")
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("Please enter all data");
            }
            else
            {

            
            RUNSSTATISTICS newRecord = new RUNSSTATISTICS()
            {
                distance = int.Parse(distTextbox.Text),
                time = TimeSpan.ParseExact(timeTextBox.Text, "hh\\:mm\\:ss", null),
                date = Calendar.SelectedDate

            };

            db.RUNSSTATISTICS.Add(newRecord);
            db.SaveChanges();

            var stats = from d in db.RUNSSTATISTICS
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
                    if(this.gridruns.SelectedItems[0].GetType()==typeof(RUNSSTATISTICS))
                    {
                        RUNSSTATISTICS x = (RUNSSTATISTICS)this.gridruns.SelectedItems[0];
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
            databaseEntities db = new databaseEntities();

            var x = from d in db.RUNSSTATISTICS
                    where d.Id == this.updatingRunID
                    select d;

            RUNSSTATISTICS obj = x.SingleOrDefault();

            if(obj!=null)
            {
                obj.distance = int.Parse(distTextboxUpadte.Text);
                obj.time = TimeSpan.ParseExact(timeTextboxUpdate.Text, "hh\\:mm\\:ss", null);
                obj.date = DateUpdate.SelectedDate;
                db.SaveChanges();
            }
            
            var stats = from d in db.RUNSSTATISTICS
                       select d;
            this.gridruns.ItemsSource = stats.ToList();
        }


        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            databaseEntities db = new databaseEntities();
            var x = from d in db.RUNSSTATISTICS
                    where d.Id == this.updatingRunID
                    select d;

            RUNSSTATISTICS obj = x.SingleOrDefault();
            if (obj != null)
            {
                db.RUNSSTATISTICS.Remove(obj);
                db.SaveChanges();
            }
            var stats = from d in db.RUNSSTATISTICS
                        select d;
            this.gridruns.ItemsSource = stats.ToList();
        }



        private void distTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void timeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);

        }


        private void startingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            databaseEntities db = new databaseEntities();
            var stats = from d in db.RUNSSTATISTICS
                        where d.date >= startingDate.SelectedDate && d.date <=endDate.SelectedDate
                        select d;

            this.gridruns.ItemsSource = stats.ToList();

        }

        private void endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            databaseEntities db = new databaseEntities();
            var stats = from d in db.RUNSSTATISTICS
                        where d.date >= startingDate.SelectedDate && d.date <= endDate.SelectedDate
                        select d;

            this.gridruns.ItemsSource = stats.ToList();
        }
    }
}
