namespace ball_simulation
{
    public class Event
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
            if (a != null) _countA = a.Count;
            else _countA = -1;
            if (b != null) _countA = b.Count;
            else _countB = -1;
        }

        public bool isValid(double t) { //todo
            if (Time < t) return false;
            if (A != null && A.Count != _countA) return false;
            if (B != null && B.Count != _countB) return false;
            return true;
        }
        
       
    }
}