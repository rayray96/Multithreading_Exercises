using System.Collections.Generic;
using System.Threading;

namespace SharedQueue
{
    public class SharedQueue<T>
    {
        private Queue<T> _queue;
        private object _locker;

        public SharedQueue()
        {
            _queue = new Queue<T>();
            _locker = new object();
        }

        /// <summary>
        /// Adds an item to the queue 
        /// and notifies a thread in the waiting queue of a change in the locked object's state.
        /// </summary>
        /// <param name="item">Data</param>
        public void Enqueue(T item)
        {
            lock (_locker)
            {
                _queue.Enqueue(item);
                Monitor.Pulse(_locker);
            }
        }

        /// <summary>
        /// Blocks the calling thread in case the queue is empty
        /// and returns control as soon as a piece of data is added to the queue.
        /// </summary>
        /// <returns>Data</returns>
        public T Dequeue()
        {
            lock (_locker)
            {
                if (_queue.Count == 0)
                {
                    Monitor.Wait(_locker);
                }

                return _queue.Dequeue();
            }
        }
    }
}
