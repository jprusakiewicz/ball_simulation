using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ball_simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow
    {
        Random rand = new Random();
        DispatcherTimer _timer = new DispatcherTimer();
        public static int numOfBalls = 100;
        Ball[] balls = new Ball[numOfBalls];
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
            for (int i = 0; i < numOfBalls - 1; i++)
            {
                
                balls[i] = new Ball(rand.Next(15, 480), rand.Next(15, 300), rand.Next(-80, 80), rand.Next(-80, 80));
                KubasCanvas.Children.Add(balls[i].getBall());

            }
        }
        void animation(object sender, EventArgs e)
        {
            for (int i = 0; i < numOfBalls-1; i++)
            {
                balls[i].moveBall(_timer.Interval.TotalSeconds, KubasCanvas.ActualWidth, KubasCanvas.ActualHeight);
            }
            
        }
    }
}