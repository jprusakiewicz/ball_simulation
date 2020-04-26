using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
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
        public static int numOfBalls = 10;
        Ellipse[] _balls = new Ellipse[numOfBalls];
        public MainWindow()
        {
            InitializeComponent();
            spawnBalls();

            _timer.Interval = TimeSpan.FromSeconds(0.05);
            _timer.IsEnabled = true;
            _timer.Tick += animation;

        }

        public void spawnBalls()
        {
            
            for (int i = 0; i < numOfBalls-1; i++)
            {
                _balls[i] = new Ellipse();
                _balls[i].Width = 10;
                _balls[i].Height = 10;
                _balls[i].Fill = Brushes.Green;
                Canvas.SetTop(_balls[i], i * 15);
                Canvas.SetLeft(_balls[i], i * 15);
                KubasCanvas.Children.Add(_balls[i]);
            }
        }
        void animation(object sender, EventArgs e)
        {
            for (int i = 0; i < numOfBalls-1; i++)
            {
                double _x = Canvas.GetLeft(_balls[i]);
                _x += vx * _timer.Interval.TotalSeconds;
                if (_x <= 0.0 || _x >= KubasCanvas.ActualWidth - _balls[i].Height)
                {
                    vx = -vx;
                }
                Canvas.SetLeft(_balls[i], _x);
            
                double _y = Canvas.GetTop(_balls[i]);
                _y += vy * _timer.Interval.TotalSeconds;

                if (_y <= 0.0 || _y >= KubasCanvas.ActualHeight - _balls[i].Height)
                {
                    vy = -vy;
                }
                Canvas.SetTop(_balls[i], _y);
            }
            
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