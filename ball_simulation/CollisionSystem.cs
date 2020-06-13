using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Priority_Queue;

namespace ball_simulation
{
    public class CollisionSystem
    {
        public PriorityQueue pq { get; set; }
        public double t;
        public Ball[] _balls;

        public CollisionSystem(Ball[] balls)
        {
            _balls = balls.Clone() as Ball[];
        }

        void Predict(Ball a)
        {
            if (a == null) return;
            for (int i = 0; i < _balls.Length; i++)
            {
                double dt = a.TimeToHit(_balls[i]);

                if (t + dt <= 10000000) // just a limit
                    pq.insertEvent(new Event(t + dt, a, _balls[i]));
            }

            double dtX = a.TimeToHitVerticalWall();
            double dtY = a.TimeToHitHorizontalWall();
            if (t + dtX <= 10000000) pq.insertEvent(new Event(t + dtX, a, null));
            if (t + dtY <= 10000000) pq.insertEvent(new Event(t + dtY, null, a));
        }

        public void Simulate()
        {
            pq = new PriorityQueue();
            for (int i = 0; i < _balls.Length; i++) Predict(_balls[i]);

            while (pq.getNumItems() != 0)
            {
                // ReSharper disable once InconsistentNaming
                Event _event = pq.delMin();
                if (!_event.isValid(t)) continue;
                Ball a = _event.getA();
                Ball b = _event.getB();

                for (int i = 0; i < _balls.Length; i++)
                {
                     _balls[i].MoveBall(0.0015f);
                    //_balls[i].MoveBall(_event.Time - t);
                }

                t = _event.Time;
                for (int i = 0; i < _balls.Length; i++)
                    _balls[i].DrawBall();

                if (a != null && b != null) a.BounceOff(b);
                else if (a != null && b == null) a.BounceOffVerticalWall();
                else if (a == null && b != null) b.BounceOffHorizontalWall();

                Predict(a);
                Predict(b);
            }
        }
    }
}