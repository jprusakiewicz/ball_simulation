using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using Priority_Queue;

namespace ball_simulation
{
    public class CollisionSystem
    {
        private SimplePriorityQueue<Event> _pq = new SimplePriorityQueue<Event>();
        private double t = 0;
        private Ball[] _balls;

        public CollisionSystem(Ball[] balls)
        {
            _balls = balls.Clone() as Ball[];
        }

        void Predict(Ball a)
        {
            if(a == null) return;
            for (int i = 0; i < _balls.Length; i++)
            {
                double dt = a.TimeToHit(_balls[i]);
                
                    //dt = 0.2;//todo
                    //Debug.WriteLine("dt"+dt);
                
               if(t + dt <= 1000) // just a limit
                    _pq.Enqueue(new Event(t + dt, a, _balls[i]), (float) (t+dt));
            }

            double dtX = a.TimeToHitVerticalWall();
            double dtY = a.TimeToHitHorizontalWall();
            if(t + dtX <= 1000) _pq.Enqueue(new Event(t+dtX,a,null),(float) (t+dtX) );
            if(t + dtY <= 1000) _pq.Enqueue(new Event(t+dtY,null,a),(float) (t+dtY) );
        }

        public void Simulate(int actualWidth, int actualHeight)
        {
          _pq = new SimplePriorityQueue<Event>();
          for (int i = 0; i < _balls.Length; i++) Predict(_balls[i]);

          Debug.WriteLine(_pq.Count);
              while (_pq.Count != 0)
              {
                  // ReSharper disable once InconsistentNaming
                  Event _event = _pq.Dequeue();
                  if (!_event.isValid(t)) continue; //todo may need to refactor
                  Ball a = _event.A;
                  Ball b = _event.B;

                  for (int i = 0; i < _balls.Length; i++)
                  {
                      _balls[i].MoveBall(_event.Time - t);
                  }

                  t = _event.Time;
                  
                  if (a != null && b != null) a.BounceOff(b);
                  else if (a!= null && b== null) a.BounceOffVerticalWall();
                  else if (a == null && b != null) b.BounceOffHorizontalWall();

                  Predict(a);
                  Predict(b);
              }
              for ( int i = 0; i < _balls.Length; i++)
                  _balls[i].DrawBall();
        }
        
    }
}