using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ball_simulation
{
    public class Ball
    {
        private double vx, vy;
        private double pozX, pozY;
        private Ellipse body;

        public Ball(double x, double y, double _vx, double _vy)
        {
           
            vx = _vx;
            vy = _vy;
            pozX = x;
            pozY = y;
            
            body = new Ellipse();
            body.Width = 4;
            body.Height = 4;
            body.Fill = Brushes.Green;
            Canvas.SetTop(body, pozX);
            Canvas.SetLeft(body, pozY);
        }

        public Ellipse getBall()
        {
            return body;
        }

        public void moveBall(double _sec,double _ActualWidth,double _ActualHeight)
        {
            pozX += vx * _sec;
            if (pozX <= 0.0 || pozX >= _ActualWidth - body.Height)
            {
                vx *= -1;
            }
            Canvas.SetLeft(body, pozX);
            
            pozY += vy * _sec;

            if (pozY <= 0.0 || pozY >= _ActualHeight - body.Height)
            {
                vy *= -1;
            }
            Canvas.SetTop(body, pozY);
        }
    }

}