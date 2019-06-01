using System;
using System.Threading;
using SharedQueue;

namespace UI
{
    class Program
    {
        static SharedQueue<string> sharedQueue = new SharedQueue<string>();

        static void Main(string[] args)
        {
            Produce();
            Consume();
        }

        static void Produce()
        {
            var thread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    sharedQueue.Enqueue($"Product #{i}");
                    Console.WriteLine($"Thread #{Thread.CurrentThread.ManagedThreadId} produces the Product #{i}");
                    Thread.Sleep(500);
                }
            });
            thread.Start();
        }

        static void Consume()
        {
            var thread = new Thread(() =>
            {
                for (int i = 0; i < 12; i++)
                {
                    Console.WriteLine($"Thread #{Thread.CurrentThread.ManagedThreadId} consumes the {sharedQueue.Dequeue()}");
                    Thread.Sleep(100);
                }
            });
            thread.Start();
        }
    }
}
