using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ball_simulation
{
    public partial class MainWindow
    {
        Random rand = new Random();
        DispatcherTimer _timer = new DispatcherTimer();
        public static int numOfBalls = 50;
        Ball[] balls = new Ball[numOfBalls];
        private CollisionSystem _system;
        
        public MainWindow()
        {
            InitializeComponent();
            SpawnBalls();
            
            _timer.Interval = TimeSpan.FromSeconds(0.005);
            _timer.IsEnabled = true;
            _timer.Tick += Animation;
        }
        

        private void SpawnBalls()
        {
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball(rand.Next(15, 250), rand.Next(15, 250), rand.Next(-1, 1), rand.Next(-1, 1));
                KubasCanvas.Children.Add(balls[i].GetBody());
                _system = new CollisionSystem(balls);
            }
        }

        private void Animation(object sender, EventArgs e)
        {
            _system.Simulate((int) KubasCanvas.ActualWidth, (int) KubasCanvas.ActualHeight, KubasCanvas);
        }
    }
}