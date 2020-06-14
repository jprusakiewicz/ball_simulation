using System;
using System.Diagnostics;
using System.Windows.Threading;
using Priority_Queue;
//using MinPQ;

namespace ball_simulation
{
    public class CollisionSystem
    {
        private FastPriorityQueue<Event> _pq;
        private double t = 0.0;
        private Ball[] _balls;

        public CollisionSystem(Ball[] balls)
        {
            _balls = balls.Clone() as Ball[];
        }

        void Predict(Ball a, double limit)
        {
            if(a == null) return;
            for (int i = 0; i < _balls.Length-1; i++)
            {
                double dt = a.TimeToHit(_balls[i]);
                if (t + dt <= limit)
                    _pq.Enqueue(new Event(t + dt, a, _balls[i]), (float) (t + dt));
            }

            double dtX = a.TimeToHitVerticalWall();
            double dtY = a.TimeToHitHorizontalWall();
            if (t + dtX <= limit)_pq.Enqueue(new Event(t + a.TimeToHitVerticalWall(), a, null), (float) (t + a.TimeToHitVerticalWall()));
            if (t + dtY <= limit)_pq.Enqueue(new Event(t + a.TimeToHitHorizontalWall(), null, a), (float) (t + a.TimeToHitHorizontalWall()));
        }
        
        void Redraw(){}

        public void Simulate(int actualWidth, int actualHeight)
        {
         _pq = new FastPriorityQueue<Event>(10000000);
         for (int i = 0; i < _balls.Length-1; i++) Predict(_balls[i], 10);

         
         while (_pq.Count > 0)
         {
             if (_pq.Count > 100000)
                 t = 5;c
             // // ReSharper disable once InconsistentNaming
             Event _event = _pq.Dequeue();
             if (_event.isValid()) ; //continue;
             {

                 Ball a = _event.A;
                 Ball b = _event.B;

                 for (int i = 0; i < _balls.Length - 1; i++)
                 {
                     _balls[i].MoveBall(_event.Time-t);
                     _balls[i].Draw();
                 }

                 t = _event.Time;


                 if (a != null && b != null) a.BounceOff(b);
                 else if (a != null && b == null) a.BounceOffVerticalWall(actualWidth);
                 else if (a == null && b != null) b.BounceOffHorizontalWall(actualHeight);

                  Predict(a, 5);
                  Predict(b, 5);
             }
         }
        }
        
    }
}