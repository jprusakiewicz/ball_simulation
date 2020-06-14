using Priority_Queue;

namespace ball_simulation
{
    public class Event : FastPriorityQueueNode
    {
        private int _countA, _countB;
        
        public Ball A { get; }

        public Ball B { get; }

        public double Time { get; }
        
        public Event(double t, Ball a, Ball b)
        {
            Time = t;
            A = a;
            B = b;
            if (a != null)_countA = a.Count;
            else _countA = -1;
            if (b != null) _countB = b.Count;
            else _countB = -1;
        }

        public double CompareTo(Event that)
        {
            return this.Time - that.Time;
        }
        
        public bool isValid(double t)
        {
            if (A != null && A.Count != _countA) return false;
            if (B != null && B.Count != _countB) return false;
            return true;
        }
    }
}