using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ball_simulation
{
    public class Ball
    {
        public double _vx { get; private set; } //velocity
        public double _vy { get; private set; } //velocity
        public int _pozX { get; private set; } //position

        public int _pozY; //position
        public Ellipse body { get; private set; }
        public int radius { get; private set; }
        public double mass { get; private set; }
        public int _count { get; private set; } //number of collisions

        public Ball(int x, int y, double vx, double vy)
        {
            _vx = vx;
            _vy = vy;
            _pozX = x;
            _pozY = y;

            body = new Ellipse();

            body.Width = 9;
            body.Height = 9;
            radius = (int) body.Width;
            mass = (radius * 0.8);

            body.Fill = Brushes.Green;
            Canvas.SetTop(body, _pozX);
            Canvas.SetLeft(body, _pozY);
        }

        public Ball GetBall()
        {
            return this;
        }

        public void MoveBall(double sec)
        {
            _pozX += (int) (_vx * sec);
            _pozY += (int) (_vy * sec);
        }

        public void DrawBall()
        {
            Canvas.SetLeft(body, _pozX);
            Canvas.SetTop(body, _pozY);
        }

        //Prediction
        public double TimeToHit(Ball that)
        {
            //Ball current = this;
            if (this == that) return double.PositiveInfinity;

            double dx = that._pozX - _pozX;
            double dy = that._pozY - _pozY;

            double dvx = that._vx - _vx;
            double dvy = that._vy - _vy;

            double dvdr = dx * dvx + dy * dvy;

            if (dvdr > 0) return double.PositiveInfinity;

            double dvdv = dvx * dvx + dvy * dvy;
            double drdr = dx * dx + dy * dy;
            double sigma = this.radius + that.radius;
            double d = (dvdr * dvdr) - dvdv * (drdr - sigma * sigma);

            if (d < 0) return double.PositiveInfinity;

            return -(dvdr + Math.Sqrt(d)) / dvdv;
        }

        public double TimeToHitVerticalWall()
        {
            if (_vx > 0) return (350 - _pozX - radius) / _vx; //todo windowwith
            else if (_pozX < 0) return (radius - _pozX) / _vx;
            else return double.PositiveInfinity;
        }

        public double TimeToHitHorizontalWall()
        {
            if (_vy > 0) return (350 - _pozY - radius) / _vy; //todo window height
            else if (_pozY < 0) return (radius - _pozY) / _vy;
            else return double.PositiveInfinity;
        }

        //Rezolution
        public void BounceOff(Ball that)
        {
            double massSum = this.mass + that.mass;
            double massDiff = this.mass - that.mass;

            double aXThis = massDiff / massSum * this._vx;
            double bXThis = (2 * that.mass) / (massSum) * that._vx;
            this._vx = aXThis + bXThis;

            double aYThis = massDiff / massSum * this._vy;
            double bYThis = (2 * that.mass) / (massSum) * that._vy;
            this._vy = aYThis + bYThis;

            double aXThat = (2 * this.mass) / (massSum) * this._vx;
            double bXThat = (that.mass - this.mass) / massSum * that._vx;
            that._vx = aXThat + bXThat;

            double aYThat = (2 * this.mass) / (massSum) * this._vy;
            double bYThat = (that.mass - this.mass) / massSum * that._vy;
            that._vx = aYThat + bYThat;

            // double dx = that._pozX - this._pozX;
            // double dy = that._pozY - this._pozY;
            //
            // double dvx = that._vx - this._vx;
            // double dvy = that._vy - that._vy;
            //
            // double dvdr = dx * dvx + dy * dvy;
            // double dist = this.radius + that.radius;
            // double J = 2 * this.mass * that.mass * dvdr / ((this.mass + that.mass) * dist);
            //
            // double Jx = J * dx / dist;
            // double Jy = J * dy / dist;
            // this._vx += Jx / this.mass;
            // this._vy += Jy / this.mass;
            // that._vx -= Jx / that.mass;
            // that._vy -= Jy / that.mass;
            this._count++;
            that._count++;
        }

        public void BounceOffVerticalWall()
        {
            _vx = -_vx;
            _count++;
        }

        public void BounceOffHorizontalWall()
        {
            _vy = -_vy;
            _count++;
        }

        public int Count => _count;

        public int getCnt()
        {
            return _count;
        }
    }
}