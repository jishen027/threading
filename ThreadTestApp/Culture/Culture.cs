using System.Globalization;

namespace ThreadTestApp.Culture;
public class Culture
{
  public static void CultureTest()
  {
    decimal[] values = { 163025412.32m, 18905365.59m };
    string formatString = "C2";

    // Func<String> is a delegate that returns a string, Func is a generic delegate
    Func<String> formatDelegate = () =>
    {
      string output = String.Format("Formatting using the {0} culture on thread {1}.\n",
                                                                           CultureInfo.CurrentCulture.Name,
                                                                           Thread.CurrentThread.ManagedThreadId);
      foreach (var value in values)
        output += String.Format("{0}   ", value.ToString(formatString));

      // Environment.NewLine is a string containing the newline character, NewLine is a property of the Environment class
      output += Environment.NewLine;

      return output;
    };

    Console.WriteLine("The example is running on thread {0}.\n", Thread.CurrentThread.ManagedThreadId);

    Console.WriteLine("The current culture is {0}.\n", CultureInfo.CurrentCulture.Name);
    if (CultureInfo.CurrentCulture.Name != "fr-FR")
    {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
    }
    else
    {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
    }

    Console.WriteLine("Changed the current culture to {0}.\n", CultureInfo.CurrentCulture.Name);

    Console.WriteLine("Executing the delegate synchronously:");
    Console.WriteLine(formatDelegate());

    Console.WriteLine("Executing the delegate asynchronously:");
    var t1 = Task.Run(formatDelegate);
    Console.WriteLine(t1.Result);

    Console.WriteLine("Executing the delegate synchronously:");
    var t2 = new Task<string>(formatDelegate);
    // Start the task and wait for it to complete
    t2.RunSynchronously();
    Console.WriteLine(t2.Result);
  }
}