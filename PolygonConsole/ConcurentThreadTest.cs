using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace PolygonConsole
{
    internal static class ConcurentThreadTest
    {
        public static void Test()
        {
            var count = 25;
            Thread[] threads = new Thread[count];
            for (var i = 0; i < threads.Length; i++)
                //threads[i] = new Thread(ConsolePrinter);
                //threads[i] = new Thread(ConsolePrinter_lock);
                threads[i] = new Thread(ConsolePrinter_simple_lock);

            foreach (var thread in threads)
            {
                thread.Start();
            }

            //Semaphore sm = new Semaphore(10, 10);
            //sm.WaitOne();
            //sm.Release();

            //System.Threading.Mutex mx = new Mutex(false, "TestMutex");

            ManualResetEvent manual_reset_event = new ManualResetEvent(false);
            AutoResetEvent auto_reset_event = new AutoResetEvent(false);

            Thread[] test_threads = new Thread[5];
            for (var i = 0; i < test_threads.Length; i++)
            {
                test_threads[i] = new Thread(() =>
                {
                    auto_reset_event.WaitOne();
                    Console.WriteLine("Поток {0} завершился", Thread.CurrentThread.ManagedThreadId);
                });
                test_threads[i].Start();
            }

            Console.ReadLine();
            auto_reset_event.Set();
            Console.ReadLine();
            auto_reset_event.Set();
            Console.ReadLine();
            auto_reset_event.Set();
            Console.ReadLine();
            auto_reset_event.Set();
            Console.ReadLine();
            auto_reset_event.Set();

            //var timer = new Timer(new TimerCallback(TimerMethod), null, 250, 1000);
            //var timer = new Timer(TimerMethod, null, 250, 1000);
            //timer.Change(0, 200);

            //System.Collections.Concurrent.BlockingCollection<int> blocking_collection = new BlockingCollection<int>();
        }

        //private static void TimerMethod(object p)
        //{

        //}

        private static void ConsolePrinter()
        {
            for (var i = 0; i < 20; i++)
            {
                Console.Write(i);
                Console.Write(",");
            }
            Console.WriteLine("20");
        }

        public static readonly object __SyncRoot = new object();
        private static void ConsolePrinter_lock()
        {
            lock (__SyncRoot)
            {
                for (var i = 0; i < 20; i++)
                {
                    Console.Write(i);
                    Console.Write(",");
                }

                Console.WriteLine("20");
            }
        }

        private static void ConsolePrinter_Monitor()
        {
            Monitor.Enter(__SyncRoot);
            try
            {
                for (var i = 0; i < 20; i++)
                {
                    Console.Write(i);
                    Console.Write(",");
                }

                Console.WriteLine("20");
            }
            finally
            {
                Monitor.Exit(__SyncRoot);
            }
        }

        private static void ConsolePrinter_simple_lock()
        {
            for (var i = 0; i < 20; i++)
                lock (__SyncRoot)
                {
                    Console.Write(i);
                    Console.Write(",");
                }

            Console.WriteLine("20");
        }
    }

    [Synchronization]
    internal class Logger : ContextBoundObject
    {
        private readonly string _FileName;

        public Logger(string LogFileName) => _FileName = LogFileName;

        public void Log(string entry)
        {
            File.AppendAllText(_FileName, entry);
        }
    }
}
