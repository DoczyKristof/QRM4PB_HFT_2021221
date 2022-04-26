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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QRM4PB_HFT_2021221.WPFClient
{
    /// <summary>
    /// Interaction logic for LoadInWindow.xaml
    /// </summary>
    public partial class LoadInWindow : Window
    {
        private const int LOADINTIME = 8; //second
        private const double INTERVAL = 100 / LOADINTIME;

        public LoadInWindow()
        {
            InitializeComponent();
        }

        void TimeService()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pb_main.Value += INTERVAL;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TimeService();
        }

        private void pb_main_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pb_main.Value >= 95 && pb_main.Value < 100)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            if (pb_main.Value >= 100)
            {
                System.Threading.Thread.Sleep(1000);
                this.Close();
            }
        }
    }
}
