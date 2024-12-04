namespace ThreadTestApp.ChildTask;

public class Child
{
  public static void ChildTest()
  {
    var parent = Task.Factory.StartNew(() =>
    {
      Console.WriteLine("Parent task beginning.");

      for (int ctr = 0; ctr < 10; ctr++)
      {
        int taskNo = ctr;
        Task.Factory.StartNew((x) =>
        {
          // Thread spin wait means the thread is busy waiting, it is a low-level synchronization primitive that is useful in various scenarios
          Thread.SpinWait(5000000);
          Console.WriteLine("Attached child #{0} completed.", x);

        }, taskNo, TaskCreationOptions.AttachedToParent);
      }
    });

    parent.Wait();
    Console.WriteLine("Parent task completed.");
  }
}