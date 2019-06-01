using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SecondTaskUI
{
    class Program
    {
        static long counter;

        static void Main(string[] args)
        {
            // Multiply each element by itself from the array of 10 million elements.
            // MultiplyAllElementsByItself(); // ***** Remove comment *****

            // Reading large amount of data and displaying on UI while not preventing user from using application.
            //var task1 = Task.Factory.StartNew(() =>              // ***** Remove comment *****
            //{
            //    Thread.Sleep(5000);
            //    Console.WriteLine("First task has finished!");
            //});

            //var task2 = Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(7000);
            //    Console.WriteLine("Second task has finished!");
            //});

            //Thread.Sleep(3000); // User work simulation while the first two tasks are in progess.
            //Console.WriteLine("Method Main has finished!");

            //Task.WaitAll(task1, task2); // User can do some work and reading large amount of data will be an asynchronously.


            // Having shared collection which is used from different threads:
            //
            //      o users can only read from it
            // Answer:  We can use System.Collection.Generic namespace - if we do not do any modification with collection.
            //          These classes provide improved type safety and performance compared to the.NET Framework 1.0 classes.
            //
            //      o users can read and modify it
            // Answer:  We can use System.Collections.Concurrent namespace - includes several collection classes that are both thread-safe and scalable.
            //          To achieve thread-safety, these new types use various kinds of efficient locking and lock-free synchronization mechanisms.
            //
            //      o users can only operate with items in collection
            // Answer:  We can use BlockingCollection<T> - it is a thread-safe collection class that provides the following features:
            //          * An implementation of the Producer - Consumer pattern.
            //          * Concurrent adding and taking of items from multiple threads.
            //          * Optional maximum capacity.
            //          * Insertion and removal operations that block when collection is empty or full.
            //          * Insertion and removal "try" operations that do not block or that block up to a specified period of time.
            //          * Encapsulates any collection type that implements IProducerConsumerCollection<T>
            //          * Cancellation with cancellation tokens.
            //          * Two kinds of enumeration with foreach (For Each in Visual Basic):
            //          * Read - only enumeration.
            //          * Enumeration that removes items as they are enumerated.


            // Having shared resource make sure concurrent users can use it appropriately.
            //Console.WriteLine("Expected = 10000000");  // ***** Remove comment *****
            //Thread[] threads = new Thread[10];

            //for (int i = 0; i < 10; ++i)
            //    (threads[i] = new Thread(IncreaseCounter)).Start();

            //for (int i = 0; i < 10; ++i)
            //    threads[i].Join();

            //Console.WriteLine($"Result  = {counter}");

            // Run some part of work in a separate thread.
            Console.WriteLine($"First thread Id: {Thread.CurrentThread.ManagedThreadId}"); // ***** Remove comment *****

            Thread thread = new Thread(DrawPoints);
            thread.Start();
            thread.Join();

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 160; i++)
            {
                Thread.Sleep(20);
                Console.Write("-");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nFirst thread has finished.");
        }

        static void MultiplyAllElementsByItself()
        {
            long[] array = new long[10000000];

            // When processing a collection PLINQ uses the capabilities of all processors in the system. 
            // The data source is divided into segments, and each segment is processed in a separate stream.
            // This allows you to make a request on multi - core machines much faster.
            Parallel.For(0, array.Length, x => array[x] = x);

            var query = from n in array.AsParallel()
                        select n * n;
        }

        static void IncreaseCounter()
        {
            for (int i = 0; i < 1000000; i++)
            {
                // Interlocked - provides atomic operations for variables that are shared by multiple threads.
                // This allows us to synchronize access to a variable from different threads.
                Interlocked.Increment(ref counter);
            }
        }

        static void DrawPoints()
        {
            Console.WriteLine($"Second thread Id: {Thread.CurrentThread.ManagedThreadId}");
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < 160; i++)
            {
                Thread.Sleep(20);
                Console.Write(".");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nSecond thread has finished.");
        }
    }
}
