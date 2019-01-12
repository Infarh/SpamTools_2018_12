using System;
using System.IO;
using System.Threading;

namespace PolygonConsole
{
    internal class Program
    {
        private const string data_file = "./data/voinal-i-mir.zip";

        private static void Main(string[] args)
        {
            //SimpleThreadTest();
            //ComplexThreadTest();
            //ConcurentThreadTest.Test();
            ThreadPoolTests.Test();

            lock (ConcurentThreadTest.__SyncRoot)
                Console.WriteLine("Основной потока завершился совсем!");
            Console.ReadLine();
        }

        private static void ComplexThreadTest()
        {
            //var parameterized_thread = new Thread(new ParameterizedThreadStart(ThreadMethod2));
            var parameterized_thread = new Thread(ThreadMethod2);
            parameterized_thread.Start(128);
        }

        private static void ThreadMethod2(object parameter)
        {
            var value = (int)parameter;
            Thread.Sleep(1500);
            //Thread.SpinWait(3000);

            Console.WriteLine("Параметр потока {0}", value);
        }

        private static void SimpleThreadTest()
        {
            //Thread long_thread = new Thread(new ThreadStart(LongOperationMethod));
            Thread long_thread = new Thread(LongOperationMethod);
            var simple_thread = new Thread(() => Console.WriteLine("123"));

            long_thread.Name = "Поток чтения файла";
            long_thread.Priority = ThreadPriority.BelowNormal;
            long_thread.IsBackground = true;

            long_thread.Start();

            var count = 10;
            while (count-- > 0)
            {
                Console.Title = DateTime.Now.ToString();
                Thread.Sleep(250);
            }

            Console.Clear();
            Console.WriteLine("Основной потока завершился");
            //long_thread.Suspend();
            //Console.ReadLine();
            //long_thread.Resume();
            if (!long_thread.Join(2000))
            {
                long_thread.Abort();
            }
        }

        private static void LongOperationMethod()
        {
            try
            {
                var thread_name = Thread.CurrentThread.Name;

                using (var data = File.OpenRead(data_file))
                {
                    var lines_count = 0;
                    using (var zip = new System.IO.Compression.ZipArchive(data))
                    {
                        foreach (var zip_entry in zip.Entries)
                        {
                            using (var zip_entry_data = zip_entry.Open())
                            using (var reader = new StreamReader(zip_entry_data))
                            {
                                while (!reader.EndOfStream)
                                {
                                    reader.ReadLine();
                                    Console.WriteLine($"File: {zip_entry.Name} - Lines count {++lines_count} -- {thread_name}");

                                }
                            }
                        }
                    }
                }
            }
            catch (ThreadAbortException error)
            {
                Console.WriteLine($"Потока прерван {error}");
                throw;
            }
        }
    }
}
