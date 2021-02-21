using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
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
    /// Interaction logic for timer.xaml
    /// </summary>
    public partial class timer : Page
    {

        private Stopwatch _stopwatch;
        private Timer _timer;
        private const string _startTime = "00:00:00";

        /// <summary>
        /// Timer construcor, sets new Stopwatch, sets displaying time to 00:00:00, sets time interval on 1 secund
        /// </summary>
        public timer()
        {
            InitializeComponent();
            stopwatchText.Text = _startTime;
            _stopwatch = new Stopwatch();
            _timer = new Timer(interval: 1000);
            _timer.Elapsed += OnTimerElapse;
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>  stopwatchText.Text = _stopwatch.Elapsed.ToString(format: @"hh\:mm\:ss"));
        }
        /// <summary>
        /// Starting the stopwatch
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Start();
            _timer.Start();
            ResetButton.IsEnabled = false;
        }
        /// <summary>
        /// Stops the stopwatch
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">State information and event data associated with a routed event.</param>
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Stop();
            _timer.Stop();
            ResetButton.IsEnabled = true;
        }
        /// <summary>
        /// Resets the stopwatch
        /// </summary>
        /// <param name="sender">The object which invoked the method/event/delegate</param>
        /// <param name="e">tate information and event data associated with a routed event.</param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Reset();
            stopwatchText.Text = _startTime;
        }
    }
}
