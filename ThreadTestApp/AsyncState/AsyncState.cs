using ThreadTestApp.Data;
namespace ThreadTestApp.AsyncState;
public class AsyncState
{
  public void ThreadTest()
  {
    Task[] taskArray = new Task[10];

    for (int i = 0; i < taskArray.Length; i++)
    {

      taskArray[i] = Task.Factory.StartNew((object obj) =>
      {
        // cast the obj to CustomData
        CustomData data = obj as CustomData;
        // if cast failed, return null
        if (data == null)
          return;
        // set the data properties, CurrentThread.ManagedThreadId returns the thread ID
        data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
      },

      new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks });
    }

    Task.WaitAll(taskArray);

    foreach (var task in taskArray)
    {
      var data = task.AsyncState as CustomData;
      if (data != null)
        Console.WriteLine("Task #{0} created at {1}, ran on thread #{2}.",
                          data.Name, data.CreationTime, data.ThreadNum);
    }
  }
}