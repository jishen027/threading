using System.Net;
using ThreadTestApp.Data;
namespace ThreadTestApp.Iteration;
public class IterationTwo
{
  public static void IterationTwoTest()
  {
    Task[] taskArray = new Task[10];

    for (int i = 0; i < taskArray.Length; i++)
    {
      taskArray[i] = Task.Factory.StartNew((object obj) =>
      {
        var data = new CustomData
        {
          Name = i,
          CreationTime = DateTime.Now.Ticks,
          ThreadNum = Thread.CurrentThread.ManagedThreadId
        };
        Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.", data.Name, data.CreationTime, data.ThreadNum);
      }, i);
    }
    Task.WaitAll(taskArray);
  }
}

// Task #10 created at 638689104703423286, ran on thread #10.
// Task #10 created at 638689104703426042, ran on thread #11.
// Task #10 created at 638689104703421954, ran on thread #8.
// Task #10 created at 638689104703421972, ran on thread #9.
// Task #10 created at 638689104703429426, ran on thread #14.
// Task #10 created at 638689104703429596, ran on thread #13.
// Task #10 created at 638689104703427837, ran on thread #12.
// Task #10 created at 638689104703421987, ran on thread #4.
// Task #10 created at 638689104703421841, ran on thread #6.
// Task #10 created at 638689104703421797, ran on thread #7.