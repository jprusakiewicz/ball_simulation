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
        

        public MainWindow()
        {
            
            // var elydupa = new Ellipse();
            // var elyupa = new Ellipse();
            
            InitializeComponent();
            
            spawnBalls(10);
            
            // elydupa.Height = 10;
            // elydupa.Width = 10;
            // elydupa.Stroke = Brushes.Blue;
            // Canvas.SetLeft(elydupa, 5);
            // Canvas.SetTop(elydupa, 5);
            // KubasCanvas.Children.Add(elydupa);
            //
            
            // elyupa.Height = 10;
            // elyupa.Width = 10;
            // elyupa.Stroke = Brushes.MediumVioletRed;
            // Canvas.SetLeft(elyupa, 215);
            // Canvas.SetTop(elyupa, 115);
            // KubasCanvas.Children.Add(elyupa);
            
            
            _timer.Interval = TimeSpan.FromSeconds(0.05);
            _timer.IsEnabled = true;
            _timer.Tick += animation;

        }

        public void spawnBalls(int numOfBalls)
        {
            Ellipse[] _balls = new Ellipse[numOfBalls];
            for (int i = 0; i < numOfBalls-1; i++)
            {
                _balls[i] = new Ellipse();
                _balls[i].Width = 10;
                _balls[i].Height = 10;
                _balls[i].Fill = Brushes.Brown;
                Canvas.SetTop(_balls[i], i * 15);
                Canvas.SetLeft(_balls[i], i * 15);
                KubasCanvas.Children.Add(_balls[i]);
            }
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