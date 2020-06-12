using System;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ball_simulation
{
    public class Ball
    {
        private double _vx, _vy; //velocity
        private double _pozX, _pozY; //position
        private Ellipse body;
        private int radius;
        private int mass;
        private int _count; //number of collisions

        public Ball(double x, double y, double vx, double vy)
        {
           
            this._vx = vx;
            this._vy = vy;
            _pozX = x;
            _pozY = y;
            
            body = new Ellipse();
            
            body.Width = 14;
            body.Height = 14;
            radius = (int)body.Width;
            mass = (int) ( radius * 1.3);
            
            body.Fill = Brushes.Green;
            Canvas.SetTop(body, _pozX);
            Canvas.SetLeft(body, _pozY);
        }

        public Ball GetBall()
        {
            return this;
        }
        public Ellipse GetBody()
        {
            return body;
        }

        public void MoveBall(double sec)
        {
            _pozX += _vx * sec;
            Canvas.SetLeft(body, _pozX);
            
            _pozY += _vy * sec;
            Canvas.SetTop(body, _pozY);
        }
        public double Dt(Ball b)
        {   Ball a = this;
            if (a == b) return 0;
            double dx  = b._pozX - a._pozX; 
            double dy  = b._pozY - a._pozY;  
            double dvx = b._vx - a._vx;   
            double dvy = b._vy - a._vy;   
            double dvdr = dx*dvx + dy*dvy;    
            if(dvdr > 0) return 0;    
            double dvdv = dvx*dvx + dvy*dvy;    
            double drdr = dx*dx + dy*dy;    
            double sigma = a.radius + b.radius;    
            double d = (dvdr*dvdr) - dvdv * (drdr - sigma*sigma);   
            if (d < 0) return 0;    
            return -(dvdr + Math.Sqrt(d)) / dvdv;
            
        }

        //Prediction
        public double TimeToHit(Ball that)
        {
            if(GetBall() == that.GetBall()) return Double.PositiveInfinity;
            double dx = that._pozX - this._pozX;
            double dy = that._pozY - this._pozY;

            double dvx = that._vx - this._vx;
            double dvy = that._vy - that._vy;
            
            double dvdr = dx * dvx + dy * dvy;
            if(dvdr > 0) return Double.PositiveInfinity;

            double dvdv = dvx * dvx + dvy * dvy;
            double drdr = dx * dx + dy * dy;
            double sigma = this.radius + that.radius;
            double d = (dvdr * dvdr) - dvdv * (drdr - sigma * sigma);
            if(d < 0) return Double.PositiveInfinity;
            return -(dvdr + Math.Sqrt(d)) / dvdv;

        }
        public double TimeToHitVerticalWall()
        {
            if      (_vx > 0) return (1.0 - _pozX - radius) / _vx;
            else if (_vx < 0) return (radius - _pozX) / _vx;  
            else             return Double.PositiveInfinity;
        }
        public double TimeToHitHorizontalWall()
        {
            if      (_vy > 0) return (1.0 - _pozY - radius) / _vy;
            else if (_vy < 0) return (radius - _pozY) / _vy;
            else             return Double.PositiveInfinity;
        }
        //Rezolution
        public void BounceOff(Ball that)
        {
            double dx = that._pozX - this._pozX;
            double dy = that._pozY - this._pozY;

            double dvx = that._vx - this._vx;
            double dvy = that._vy - that._vy;
            
            double dvdr = dx * dvx + dy * dvy;
            double dist = this.radius + that.radius;
            double J = 2 * this.mass * that.mass * dvdr / ((this.mass + that.mass) * dist);
            double Jx = J * dx / dist;
            double Jy = J * dy / dist;
            this._vx += Jx / this.mass;
            this._vy += Jy / this.mass;
            that._vx -= Jx / that.mass;
            that._vy -= Jy / that.mass;
            this._count++;
            that._count++;
        }
        public void BounceOffVerticalWall(double actualWidth)
        {

            _vx = -_vx;
            _count++;
        }
        public void BounceOffHorizontalWall(double actualHeight)
        {
            _vy = -_vy;
            _count++;
        }

        public int Count => _count;
    }
    


}