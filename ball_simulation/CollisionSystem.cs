using System;
using System.Diagnostics;
using System.Windows.Threading;
using Priority_Queue;

namespace ball_simulation
{
    public class CollisionSystem
    {
        private SimplePriorityQueue<Event> _pq = new SimplePriorityQueue<Event>();
        private float t = 0;
        private Ball[] _balls;

        public CollisionSystem(Ball[] balls)
        {
            _balls = balls.Clone() as Ball[];
        }

        void Predict(Ball a)
        {
            if(a == null) return;
            for (int i = 0; i < _balls.Length-1; i++)
            {
                double dt = a.TimeToHit(_balls[i]);
                _pq.Enqueue(new Event(t + dt, a, _balls[i]), t);
            }

            _pq.Enqueue(new Event(t + a.TimeToHitVerticalWall(), a, null), t);
            _pq.Enqueue(new Event(t + a.TimeToHitHorizontalWall(), null, a), t);
        }
        
        void Redraw(){}

        public void Simulate(int actualWidth, int actualHeight, double timer)
        {
         _pq = new SimplePriorityQueue<Event>();
          for (int i = 0; i < _balls.Length-1; i++) Predict(_balls[i]);
          _pq.Enqueue(new Event(0, null, null), t);
         //
         while (_pq.Count != 0)
         {
         // ReSharper disable once InconsistentNaming
         Event _event = _pq.Dequeue();
         //
         //
          Ball a = _event.A;
          Ball b = _event.B;

         for (int i = 0; i < _balls.Length-1; i++)
            _balls[i].MoveBall(0.5f);
         t = (float) _event.Time;

          
         if(a != null && b!= null) a.BounceOff(b);
         else if (a!= null && b== null) a.BounceOffVerticalWall(actualWidth);
         else if (a == null && b != null) b.BounceOffHorizontalWall(actualHeight);
         //  //else if (a == null && b == null) redraw();
         //
         Predict(a);
         Predict(b);
         //
         }
        }
        
    }
}