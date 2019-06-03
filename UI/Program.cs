using System;
using System.Threading;
using System.Threading.Tasks;
using SharedQueue;

namespace UI
{
    class Program
    {
        static SharedQueue<string> sharedQueue = new SharedQueue<string>();
        static int i = 0;
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            new Timer(Produce, autoEvent, 10, 300);
            new Timer(Consume, autoEvent, 12, 100);
            autoEvent.WaitOne();
        }

        static void Produce(object obj)
        {
            Interlocked.Increment(ref i);
            sharedQueue.Enqueue($"Product #{i}");
            Console.WriteLine($"Thread #{Thread.CurrentThread.ManagedThreadId} produces the Product #{i}");
        }

        static void Consume(object obj)
        {
            Console.WriteLine($"Thread #{Thread.CurrentThread.ManagedThreadId} consumes the {sharedQueue.Dequeue()}");
        }
    }
}
