using System;
using System.Threading;

namespace PolygonConsole
{
    internal static class ThreadPoolTests
    {
        public static void Test()
        {
            //ThreadPool.SetMinThreads(3, 3);
            ThreadPool.SetMaxThreads(20, 20);

            const int threads_count = 40;

            for (var i = 0; i < threads_count; i++)
            {
                ThreadPool.QueueUserWorkItem(PoolThreadMethod);
            }
        }

        private static void PoolThreadMethod(object parameter)
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Работает поток {0}, i = {1}", Thread.CurrentThread.ManagedThreadId, i);
                Thread.Sleep(250);  
            }
        }
    }
}
