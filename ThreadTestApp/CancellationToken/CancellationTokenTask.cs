namespace ThreadTestApp.CancellationToken;

public class CancellationTokenTaskApp()
{
  public static async Task CancellationTokenTaskTest()
  {
    // Create a cancellation token source
    var tokenSource2 = new CancellationTokenSource();
    var ct = tokenSource2.Token;

    var task = Task.Run(() =>
    {
      // Were we already canceled?
      ct.ThrowIfCancellationRequested();

      bool moreToDo = true;

      while (moreToDo)
      {
        // Poll on this property if you have to do
        // other cleanup before throwing.
        if (ct.IsCancellationRequested)
        {

          Console.WriteLine("Task {0} was cancelled before it got started.",
                            Task.CurrentId);
          // Clean up here, then...
          ct.ThrowIfCancellationRequested();
        }
      }
    }, tokenSource2.Token); // Pass same token to Task.Run

    tokenSource2.Cancel();

    try
    {
      await task;
    }
    catch (OperationCanceledException)
    {
      Console.WriteLine($"{nameof(OperationCanceledException)} thrown by CancelTask.");
    }
    finally
    {
      Console.WriteLine("Task status: {0}", task.Status);
      tokenSource2.Dispose();
    }

    Console.WriteLine("Press any key to exit.");
    // console read key means the console will wait for a key to be pressed
    Console.ReadKey();
  }
}