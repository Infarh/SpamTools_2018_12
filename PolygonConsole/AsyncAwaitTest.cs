using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PolygonConsole
{
    internal static class AsyncAwaitTest
    {
        public static async void Test()
        {
            Console.WriteLine("Исходный поток {0}", Thread.CurrentThread.ManagedThreadId);

            //var action_task = Task.Run(() => TaskAction());
            //Console.WriteLine("Задача запущена из потока {0}", Thread.CurrentThread.ManagedThreadId);

            //await action_task.ConfigureAwait(false); //action_task.Wait();
            //Console.WriteLine("Задача завершена в потоке {0}", Thread.CurrentThread.ManagedThreadId);

            //var task_result = await Task.Run(() => TaskFunction());
            ////var task_result = Task.Run(() => TaskFunction()).Result;

            var data = Enumerable.Range(0, 10).Select(i => $"Данные {i}");

            //data.AsParallel()
            //    .Select((str, i) => $"{i} {str}")
            //    .OrderByDescending(s => s)
            //    .ForAll(str => Console.WriteLine(str));

            //var parallel_query = data.AsParallel()
            //    .WithDegreeOfParallelism(3)
            //    .WithMergeOptions(ParallelMergeOptions.NotBuffered)
            //    .Select((str, i) => $"{i} {str}")
            //    .OrderByDescending(s => s);
            //foreach (var str in parallel_query)
            //{
            //    Console.WriteLine(str);
            //}

            #region Использование задач
            //var tasks_list = new List<Task>();
            //foreach (var str in data)
            //{
            //    tasks_list.Add(Task.Run(() => Console.WriteLine(str)));
            //}

            //var complete_task = Task.WhenAll(tasks_list);
            //await complete_task; 
            #endregion

            //await Task.WhenAll(data.Select(str => Task.Run(() =>
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine(str);
            //})));

            //var progress = new Progress<double>(p =>
            //{
            //    Console.WriteLine("Прогресс вычисления {0:p2}", p);
            //});

            //var cancellation_token_source = new CancellationTokenSource();

            //var sum = await GetSumAsync(50, progress, cancellation_token_source.Token);
            //cancellation_token_source.Cancel();

            //TaskCompletionSource<string> tcs;

            //Console.WriteLine("Сумма 50 чисел = {0}", sum);

            Process calc_process = Process.Start("cmd");

            var exit_code = await calc_process.WaitExitAsync();

            Console.WriteLine("Процесс завершился с кодом {0}", exit_code);

            Console.WriteLine("Параллельная обработка данных завершена");

        }

        private static void TaskAction()
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            var task_id = Task.CurrentId;
            Console.WriteLine($"Поток {thread_id} запущен. Задача {task_id}");
            Thread.Sleep(1500);
            Console.WriteLine($"Поток {thread_id} завершён. Задача {task_id}");
        }

        private static int TaskFunction()
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            var task_id = Task.CurrentId;
            Console.WriteLine($"Поток {thread_id} запущен. Задача {task_id}");
            Thread.Sleep(1500);
            Console.WriteLine($"Поток {thread_id} завершён. Задача {task_id}");

            return 42;
        }

        private static async void ActionAsync()
        {
            #region Синхронная часть метода
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            var task_id = Task.CurrentId;

            Console.WriteLine($"Поток {thread_id} запущен. Задача {task_id}");
            #endregion

            await Task.Delay(1500);  // Асинхронная операция

            #region Продолжение
            Console.WriteLine($"Поток {thread_id} завершён. Задача {task_id}");
            #endregion
        }

        private static async Task TaskMethodAsync()
        {
            await Task.Delay(1500);
            Console.WriteLine("Асинхронная печать чего-нибудь...");
        }

        private static Task TaskMethod()
        {
            Task.Delay(1500).Wait();

            return Task.Run(() => Console.WriteLine("Асинхронная печать чего-нибудь..."));
        }

        private static async Task<string> ResultMethodAsync(int parameter)
        {
            await Task.Delay(1500);

            return $"Строка с параметром {parameter}";
        }

        private static Task<string> ResultMethod(int parameter)
        {
            Task.Delay(1500).Wait();

            return Task.FromResult($"Строка с параметром {parameter}");
        }

        private static async Task<int> GetSumAsync(int N, IProgress<double> progress = null,
            CancellationToken cancel = default(CancellationToken))
        {
            cancel.ThrowIfCancellationRequested();
            var sum = 0;
            for (var i = 0; i < N; i++)
            {
                await Task.Delay(500);
                sum += i;
                progress?.Report((double)i / N);
                if (cancel.IsCancellationRequested)
                {
                    // Действия по штатному прерыванию операции (закрытие соединений и т.п.)
                    cancel.ThrowIfCancellationRequested();
                }
            }

            progress?.Report(1);
            return sum;
        }
    }
}
