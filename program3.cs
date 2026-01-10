using System;
using System.Threading;
class Program
{
    static int progress = 0;
    static object locker = new object();

    static void Main()
    {
        Console.CursorVisible = false;

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;
        ThreadPool.QueueUserWorkItem(LoadProcess, token);
        Console.WriteLine(" Для отмены нажмите ESC");
        while (!token.IsCancellationRequested && progress < 100)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    cts.Cancel(); 
                }
            }

            DrawProgressBar();
            Thread.Sleep(50);
        }

        Console.WriteLine();

        if (token.IsCancellationRequested)
            Console.WriteLine("Загрузка отменена!");
        else
            Console.WriteLine("Загрузка завершена!");

        Console.CursorVisible = true;
        Console.ReadKey();
    }

    static void LoadProcess(object state)
    {
        CancellationToken token = (CancellationToken)state;

        while (progress < 100)
        {
            if (token.IsCancellationRequested)
                return;

            Thread.Sleep(100);

            lock (locker)
            {
                progress++;
            }
        }
    }

    static void DrawProgressBar()
    {
        lock (locker)
        {
            int total = 20;
            int filled = progress * total / 100;

            Console.SetCursorPosition(0, 1);
            Console.Write("[");
            Console.Write(new string('#', filled));
            Console.Write(new string('.', total - filled));
            Console.Write($"] {progress}%   ");
        }
    }
}
