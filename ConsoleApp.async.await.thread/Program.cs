using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.async.await.thread
{
  class Program
  {
    public static object locker = new object();
    static void Main(string[] args)
    {
      Console.WriteLine("Main Start");
      #region thread
      //Thread thread1 = new Thread(new ThreadStart(DoWork));
      //thread1.Start();

      //Thread thread2 = new Thread(new ParameterizedThreadStart(DoWork));
      //thread2.Start(int.MaxValue);

      //int j = 0;
      //for (int i = 0; i < int.MaxValue; i++)
      //{
      //  j++;
      //  if (j % 100000 == 0)
      //  {
      //    Console.WriteLine("Main");
      //  }
      //}
      #endregion

      #region async
      //DoWorkAsync();

      //for (int i = 0; i < 10; i++)
      //{
      //  Console.WriteLine("Main");
      //}
      #endregion

      //var result = SaveFile("D:\\test.txt");
      var result = SaveFileAsync("D:\\test.txt");
      Console.WriteLine($"SaveFile: {result.Result}");

      Console.WriteLine("Main End");
      Console.ReadLine();
    }



    static async Task<bool> SaveFileAsync(string path)
    {
      var result = await Task.Run(() => SaveFile(path));
      return result;
    }
    static bool SaveFile(string path)
    {
      lock (locker)
      {
        var rnd = new Random();
        var text = "";
        for (int i = 0; i < 50000; i++)
        {
          text += rnd.Next();
        }

        using (var sw = new StreamWriter(path, false))
        {
          sw.WriteLine(text);
        }
        return true;
      }
    }
    static async Task DoWorkAsync()
    {
      Console.WriteLine("DoWorkAsync Start");
      await Task.Run(() => DoWork());
      Console.WriteLine("DoWorkAsync End");
    }
    static void DoWork()
    {
      for (int i = 0; i < 10; i++)
      {
        Console.WriteLine("DoWork1");
      }
    }
    static void DoWork(object max)
    {
      int j = 0;
      for (int i = 0; i < (int)max; i++)
      {
        j++;
        if (j % 100000 == 0)
        {
          Console.WriteLine("DoWork2");
        }
      }
    }

  }
}
