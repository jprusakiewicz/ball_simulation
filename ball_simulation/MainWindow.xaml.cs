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
        private double[] vx = new double[numOfBalls];
        private double[] vy = new double[numOfBalls];
        public static int numOfBalls = 14;
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
            Random rand = new Random(); 
            for (int i = 0; i < numOfBalls - 1; i++)
            {
                vx[i] = rand.Next(-55, 55);
            }
            
            for (int i = 0; i < numOfBalls - 1; i++)
            {
                vy[i] = rand.Next(-55, 55);
            }
            for (int i = 0; i < numOfBalls-1; i++)
            {
                _balls[i] = new Ellipse();
                _balls[i].Width = 10;
                _balls[i].Height = 10;
                _balls[i].Fill = Brushes.Green;
                Canvas.SetTop(_balls[i], i * 15 + 10);
                Canvas.SetLeft(_balls[i], i * 15 + 10);
                KubasCanvas.Children.Add(_balls[i]);
            }
        }
        void animation(object sender, EventArgs e)
        {
            for (int i = 0; i < numOfBalls-1; i++)
            {
                double _x = Canvas.GetLeft(_balls[i]);
                _x += vx[i] * _timer.Interval.TotalSeconds;
                if (_x <= 0.0 || _x >= KubasCanvas.ActualWidth - _balls[i].Height)
                {
                    vx[i] *= -1;
                }
                Canvas.SetLeft(_balls[i], _x);
            
                double _y = Canvas.GetTop(_balls[i]);
                _y += vy[i] * _timer.Interval.TotalSeconds;

                if (_y <= 0.0 || _y >= KubasCanvas.ActualHeight - _balls[i].Height)
                {
                    vy[i] *= -1;
                }
                Canvas.SetTop(_balls[i], _y);
            }
            
        }
    }
}