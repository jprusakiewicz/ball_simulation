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
        public static int numOfBalls = 55;
        Ball[] balls = new Ball[numOfBalls];
        private CollisionSystem _system;
        public static bool m = false;
        public MainWindow()
        {
            InitializeComponent();
            SpawnBalls();

            _timer.Interval = TimeSpan.FromSeconds(0.05);
            _timer.IsEnabled = true;
            _timer.Tick += Animation;

        }

        private void SpawnBalls()
        {
            for (int i = 0; i < numOfBalls - 1; i++)
            {
                
                balls[i] = new Ball(rand.Next(15, 480), rand.Next(15, 300), rand.Next(-30, 30), rand.Next(-30, 30));
                KubasCanvas.Children.Add(balls[i].GetBody());
                _system = new CollisionSystem(balls);
            }
        }
        
        private void Animation(object sender, EventArgs e)
        {
            _system.Simulate((int) KubasCanvas.ActualWidth, (int) KubasCanvas.ActualHeight);

        }
    }
}