using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ball_simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        DispatcherTimer _timer = new DispatcherTimer();
        private double vx = 50.0;
        private double vy = 50.0;
        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromSeconds(0.05);
            _timer.IsEnabled = true;
            _timer.Tick += animation;
        }

        void animation(object sender, EventArgs e)
        {
            double x = Canvas.GetLeft(ball1);
            x += vx * _timer.Interval.TotalSeconds;
            if (x <= 0.0 || x >= KubasCanvas.ActualWidth - ball1.Height)
            {
                vx = -vx;
            }
            Canvas.SetLeft(ball1, x);
            
            double y = Canvas.GetTop(ball1);
            y += vy * _timer.Interval.TotalSeconds;

            if (y <= 0.0 || y >= KubasCanvas.ActualHeight - ball1.Height)
            {
                vy = -vy;
            }
            Canvas.SetTop(ball1, y);
        }
    }
}