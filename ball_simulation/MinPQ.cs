using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ball_simulation
{
    public class MinPQ<Event> : IEnumerable<Event> where Event : class
    {
        private Event[] pq;                    // store items at indices 1 to n
        private static int n;                       // number of items on priority queue
        private Comparer<Event> comparator;// private Comparator<Key> comparator;  // optional comparator

    /**
     * Initializes an empty priority queue with the given initial capacity.
     *
     * @param  initCapacity the initial capacity of this priority queue
     */
    public MinPQ(int initCapacity) {
        pq = (Event[]) new Event[initCapacity + 1];
        n = 0;
    }
    


    /**
     * Initializes an empty priority queue with the given initial capacity,
     * using the given comparator.
     *
     * @param  initCapacity the initial capacity of this priority queue
     * @param  comparator the order in which to compare the keys
     */
    public MinPQ(int initCapacity, Comparer<Event> comparator) {
        this.comparator = comparator;
        pq = (Event[]) new Object[initCapacity + 1];
        n = 0;
    }

    /**
     * Initializes a priority queue from the array of keys.
     * <p>
     * Takes time proportional to the number of keys, using sink-based heap construction.
     *
     * @param  keys the array of keys
     */
    public MinPQ(Event[] pq) {
        n = pq.Length;
        this.pq = new Event[pq.Length + 1];

        for (int i = 0; i < n; i++)
        {
            this.pq[i + 1] = pq[i];//shifts over array indicies 1 higher; pq starts @ index 1
        }

        for (int i = n / 2; i >= 1; i--)//creates heap bottom up
        {
            sink(i);
        }
    }

    /**
     * Initializes an empty priority queue.
     */
    //public MinPQ() => this.pq = new Event[100];
    public MinPQ() => this.pq = new Event[1000];

    /**
     * Returns true if this priority queue is empty.
     *
     * @return {@code true} if this priority queue is empty;
     *         {@code false} otherwise
     */
    public bool isEmpty() {
        return n == 0;
    }

    /**
     * Returns the number of keys on this priority queue.
     *
     * @return the number of keys on this priority queue
     */
    public static int size() {
        return n;
    }

    /**
     * Returns a smallest key on this priority queue.
     *
     * @return a smallest key on this priority queue
     * @throws NoSuchElementException if this priority queue is empty
     */
    public Event min() {
        if (isEmpty()) throw new System.ArgumentException("Priority queue underflow");
        return pq[1];
    }

    // helper function to double the size of the heap array
    private void resize(int capacity) {
        //assert capacity > n;
        if (capacity > n)
        {
            Event[] temp = (Event[]) new Event[capacity];
            for (int i = 1; i <= n; i++)
            {
                temp[i] = pq[i];
            }

            pq = temp;
        }
        else
            throw new System.ArgumentException("assert custom exception", "capcity");
    }

    /**
     * Adds a new key to this priority queue.
     *
     * @param  x the key to add to this priority queue
     */
    public void insert(Event x) {
        // double size of array if necessary
        if (n == pq.Length - 1) resize(2 * pq.Length);

        // add x, and percolate it up to maintain heap invariant
        pq[++n] = x;
        swim(n);
        if (!isMinHeap())
            throw new System.ArgumentException("assert custom exception");
    }

    /**
     * Removes and returns a smallest key on this priority queue.
     *
     * @return a smallest key on this priority queue
     * @throws NoSuchElementException if this priority queue is empty
     */
    public Event delMin() {
        if (isEmpty()) throw new System.ArgumentException("Priority queue underflow");
        Event min = pq[1];
        exch(1, n--);
        sink(1);
        pq[n+1] = null;     // to avoid loiterig and help with garbage collection
        if ((n > 0) && (n == (pq.Length - 1) / 4)) resize(pq.Length / 2);
        if(!isMinHeap())
            throw new System.ArgumentException("assert custom exception");
        return min;
    }


   /***************************************************************************
    * Helper functions to restore the heap invariant.
    ***************************************************************************/

    private void swim(int k) {
        while (k > 1 && greater(k/2, k)) {
            exch(k, k/2);
            k = k/2;
        }
    }

    private void sink(int k) {
        while (2*k <= n) {
            int j = 2*k;
            if (j < n && greater(j, j+1)) j++;
            if (!greater(k, j)) break;
            exch(k, j);
            k = j;
        }
    }

   /***************************************************************************
    * Helper functions for compares and swaps.
    ***************************************************************************/
   private bool greater(int i, int j) {

           return comparator.Compare(pq[i], pq[j]) > 0;
   }

   private void exch(int i, int j) {
        Event swap = pq[i];
        pq[i] = pq[j];
        pq[j] = swap;
    }

    // is pq[1..n] a min heap?
    private bool isMinHeap() {
        for (int i = 1; i <= n; i++) {
            if (pq[i] == null) return false;
        }
        for (int i = n+1; i < pq.Length; i++) {
            if (pq[i] != null) return false;
        }
        if (pq[0] != null) return false;
        return isMinHeapOrdered(1);
    }

    // is subtree of pq[1..n] rooted at k a min heap?
    private bool isMinHeapOrdered(int k) {
        if (k > n) return true;
        int left = 2*k;
        int right = 2*k + 1;
        if (left  <= n && greater(k, left))  return false;
        if (right <= n && greater(k, right)) return false;
        return isMinHeapOrdered(left) && isMinHeapOrdered(right);
    }


    /**
     * Returns an iterator that iterates over the keys on this priority queue
     * in ascending order.
     * <p>
     * The iterator doesn't implement {@code remove()} since it's optional.
     *
     * @return an iterator that iterates over the keys in ascending order
     */
    // public IEnumerable<Event> iterator() {
    //     return new HeapIterator();
    // }
    


    /**
     * Unit tests the {@code MinPQ} data type.
     *
     * @param args the command-line arguments
     */
    public static void main(String[] args) {
        MinPQ<String> pq = new MinPQ<String>();
        while (true) {
            String item = Console.ReadLine();
            if (!item.Equals("-")) pq.insert(item);
            else if (!pq.isEmpty()) Console.Write(pq.delMin() + " ");
        }
       // Console.Write("(" + pq.size() + " left on pq)");
    }
    public IEnumerator<Event> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    }
}