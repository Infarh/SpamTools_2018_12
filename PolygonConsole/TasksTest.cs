using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PolygonConsole
{
    internal static class TasksTest
    {
        public static void Test()
        {
            //Task
            //Task<>

            //var task = new Task(TaskMethod);
            //task.Start();


            var func_task = new Task<int>(TaskFunction);
            func_task.Start();

            var continue_task = func_task
                .ContinueWith(t => Console.WriteLine($"Продолжение задачи с результатом {t.Result}")/*, TaskContinuationOptions.OnlyOnRanToCompletion*/);
            //func_task.Wait();

            var action_task = Task.Run(() => TaskAction());
            var func_taks2 = Task.Run(() => TaskFunction())
                .ContinueWith(t => Console.WriteLine($"Result = {t.Result}"));

            var task_parameter = "Hello World!";
            Task.Factory.StartNew(obj => Console.WriteLine((string) obj), task_parameter);

            Action hello_world_printer = () => Console.WriteLine("Hello World! (Printer)");
            var hello_world_printer_task = Task.Factory
                .FromAsync(hello_world_printer.BeginInvoke, hello_world_printer.EndInvoke, null);

            Console.WriteLine("Основной поток завершён");
            action_task.Wait();
            Console.WriteLine("Результат выполнения задачи {0}", func_task.Result);
            func_taks2.Wait();
        }

        private static void TaskAction()
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            var task_id = Task.CurrentId;
            Console.WriteLine($"Поток {thread_id} запущен. Задача {task_id}");

            //throw new Exception("Аварийное завершение задачи");

            Thread.Sleep(1500);
            Console.WriteLine($"Поток {thread_id} завершён. Задача {task_id}");
        }

        private static int TaskFunction()
        {
            var thread_id = Thread.CurrentThread.ManagedThreadId;
            var task_id = Task.CurrentId;
            Console.WriteLine($"Поток {thread_id} запущен. Задача {task_id}");

            //throw new Exception("Аварийное завершение задачи");

            Thread.Sleep(1500);
            Console.WriteLine($"Поток {thread_id} завершён. Задача {task_id}");

            return 42;
        }
    }
}
