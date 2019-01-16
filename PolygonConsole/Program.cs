using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace PolygonConsole
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            //Parallel.Invoke(/*new ParallelOptions { MaxDegreeOfParallelism = 2 },*/
            //    ActionMethod,
            //    ActionMethod,
            //    ActionMethod,
            //    ActionMethod,
            //    ActionMethod,
            //    ActionMethod,
            //    () => { Console.WriteLine("Некий код в потоке {0}", Thread.CurrentThread.ManagedThreadId); });

            //Parallel.For(0, 10, new ParallelOptions { MaxDegreeOfParallelism = 3 }, ForLoopMethod);
            //Parallel.For(0, 10, i => { Console.WriteLine(i); });
            //Parallel.For(0, 10, (i, state) =>
            //{
            //    Console.WriteLine(i);
            //    //Thread.Sleep(1);
            //    if(i == 5) state.Break();
            //});

            var data = Enumerable.Range(0, 10).Select(i => $"Данные {i}");
            //Parallel.ForEach(data, str =>
            //{
            //    var thread_id = Thread.CurrentThread.ManagedThreadId;
            //    Console.WriteLine($"Поток {thread_id} запущен - параметр {str}");
            //    Thread.Sleep(1500);
            //    Console.WriteLine($"Поток {thread_id} завершён - параметр {str}");
            //});
            //Parallel.ForEach(data, (str, state) =>
            //{
            //    if(str.EndsWith("5")) state.Stop();
            //    var thread_id = Thread.CurrentThread.ManagedThreadId;
            //    Console.WriteLine($"Поток {thread_id} запущен - параметр {str}");
            //    Thread.Sleep(750);
            //    Console.WriteLine($"Поток {thread_id} завершён - параметр {str}");
            //});

            //Action<int> action_int = ForLoopMethod;
            //action_int.BeginInvoke(5, OnForLoopMethodComplete, null);

            //Func<int, string> func_int_str = ForLoopMethod_str;
            //func_int_str.BeginInvoke(42, OnForLoopMethodStrComplete, func_int_str);

            //Stream stream = new FileStream("c:\\123.txt", FileMode.Open);
            ////stream.BeginRead();
            ////stream.EndRead();
            ////stream.BeginWrite();
            ////stream.EndWrite();

            //Console.WriteLine("Основной поток завершён");

            //TasksTest.Test();
            AsyncAwaitTest.Test();

            Console.ReadLine();
        }

        private static void ActionMethod()
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Поток {thread_id} запущен");
            Thread.Sleep(1500);
            Console.WriteLine($"Поток {thread_id} завершён");
        }

        private static void ForLoopMethod(int index)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Поток {thread_id} запущен - параметр {index}");
            Thread.Sleep(1500);
            Console.WriteLine($"Поток {thread_id} завершён - параметр {index}");
        }

        private static string ForLoopMethod_str(int index)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Поток {thread_id} запущен - параметр {index}");
            Thread.Sleep(1500);
            Console.WriteLine($"Поток {thread_id} завершён - параметр {index}");

            return $"Результат выполнения = {index}";
        }

        private static void OnForLoopMethodComplete(IAsyncResult result)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Параллельный метод завершён. Мы в потоке {thread_id}");
        }

        private static void OnForLoopMethodStrComplete(IAsyncResult result)
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Параллельный метод завершён. Мы в потоке {thread_id}");

            var d = (Func<int, string>)result.AsyncState;

            var method_result = d.EndInvoke(result);
            Console.WriteLine($"Результат выполнения асинхронной операции:\"{method_result}\"");
        }
    }
}
