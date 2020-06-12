namespace ball_simulation
{
    public class Event
    {
        private int _countA, _countB;

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
            if      (Time < that.Time) return -1;
            else if (Time > that.Time) return +1;
            else                       return  0;
        }
        
        public bool isValid() {
            if (A != null && A.Count != _countA) return false;
            if (B != null && B.Count != _countB) return false;
            return true;
        }
        
        public Ball A { get; }

        public Ball B { get; }

        public double Time { get; }
    }
}