﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
 using System.Windows.Controls;
using System.Windows.Input;

namespace SportLife
{
    /// <summary>
    /// Interaction logic for running.xaml
    /// </summary>
    public partial class running : Page
    {
        /// <summary>
        /// Establish connection with database
        /// </summary>
        private databaseEntities db = new databaseEntities();

        /// <summary>
        /// Constructor which loads our runs statistics in datagrid
        /// </summary>
        public running()
        {
            InitializeComponent();

            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var stats = from d in db.statisticRuns
                        where d.userID == mw.currentuserID
                        select d;

            this.gridruns.ItemsSource = stats.ToList();
            startingDate.SelectedDate = new DateTime(2021, 01, 01);
            endDate.SelectedDate = DateTime.Today;
        }

        /// <summary>
        /// Adding our run stats to database
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        public void AddButton_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Show data which that we select in textboxes for updates
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        private void gridruns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridruns.SelectedIndex >= 0)
            {
                if (this.gridruns.SelectedItems.Count >= 0)
                {
                    if (this.gridruns.SelectedItems[0].GetType() == typeof(statisticRuns))
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

        /// <summary>
        /// Updates selected items and stores in database
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var x = from d in db.statisticRuns
                    where d.Id == this.updatingRunID
                    select d;

            statisticRuns obj = x.SingleOrDefault();

            if (obj != null)
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

        /// <summary>
        /// Deletes selected data from database
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
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

        /// <summary>
        /// allows the user enter only the numbers in distance textbox
        /// </summary>
        private void distTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        /// <summary>
        /// Changes data in datagrid and shows items which values ​​matching the date range
        /// </summary>
        private void startingDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(win => win is MainWindow) as MainWindow;

            var stats = from d in db.statisticRuns
                        where d.date >= startingDate.SelectedDate && d.date <= endDate.SelectedDate && d.userID == mw.currentuserID
                        select d;

            this.gridruns.ItemsSource = stats.ToList();
        }

        /// <summary>
        /// Changes data in datagrid and shows items which values ​​matching the date range
        /// </summary>
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