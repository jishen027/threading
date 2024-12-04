namespace ThreadTestApp.Iteration;

using ThreadTestApp.Data;

public class IterationOne
{
  public static void IterationOneTest()
  {
    Task[] taskArray = new Task[10];

    for (int i = 0; i < taskArray.Length; i++)
    {
      taskArray[i] = Task.Factory.StartNew((object obj) =>
      {
        CustomData data = obj as CustomData;

        if (data == null)
          return;

        // data.ThreadNum = Environment.CurrentManagedThreadId;
        data.ThreadNum = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.", data.Name, data.CreationTime, data.ThreadNum);

      }, new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
    }

    Task.WaitAll(taskArray);
  }
}