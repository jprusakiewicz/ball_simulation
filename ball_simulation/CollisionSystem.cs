using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
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
        public Canvas c;

        public CollisionSystem(Ball[] balls)
        {
            _balls = balls.Clone() as Ball[];
        }

        void Predict(Ball a, double limit)
        {
            if(a == null) return;
            for (int i = 0; i < _balls.Length; i++)
            {
                double dt = a.TimeToHit(_balls[i]);
                if (t + dt <= limit)
                    _pq.Enqueue(new Event(t + dt, a, _balls[i]), (float) (t + dt));
            }

            double dtX = a.TimeToHitVerticalWall();
            double dtY = a.TimeToHitHorizontalWall();
            if (t + dtX <= limit+1) _pq.Enqueue(new Event(t + dtX, a, null), (float) (t + dtX));
            if (t + dtY <= limit+1) _pq.Enqueue(new Event(t + dtY, null, a), (float) (t + dtY));
        }
        
        void Redraw(){}

        public void Simulate(int actualWidth, int actualHeight, Canvas kubasCanvas)
        {
            c = kubasCanvas;
         _pq = new FastPriorityQueue<Event>(50000000);
         for (int i = 0; i < _balls.Length-1; i++) Predict(_balls[i], 10000000);

         
           while (_pq.Count !=0)
          {

             // // ReSharper disable once InconsistentNaming
             Event _event = _pq.Dequeue();
             if (!_event.isValid(t)) continue;
             
             
                 Ball a = _event.A;
                 Ball b = _event.B;

                 if (a == null && b == null)continue;
                     
                 
                 for (int i = 0; i < _balls.Length; i++)
                 {
                     _balls[i].MoveBall(_event.Time-t);
                     _balls[i].Draw();
                 }
                 t = _event.Time;
                
                 CompositionTarget.Rendering += DoUpdates;
                 

                 if (a != null && b != null) a.BounceOff(b);
                 else if (a != null && b == null) a.BounceOffVerticalWall(actualWidth);
                 else if (a == null && b != null) b.BounceOffHorizontalWall(actualHeight);

                  Predict(a, 1000000);
                  Predict(b, 1000000);
         }
        }
        
        private void DoUpdates(object sender, EventArgs e)
        {
            c.Background = Brushes.Aqua;
            c.UpdateLayout();
            c.InvalidateVisual();
        }
        
    }
}