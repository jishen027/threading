namespace ThreadTestApp.Continuation;

public class ContinuationOne
{
  public static void ContinuationOneTest()
  {
    var getData = Task.Factory.StartNew(() =>
    {
      Random rnd = new Random();

      int[] values = new int[100];

      for (int ctr = 0; ctr <= values.GetUpperBound(0); ctr++)
      {
        values[ctr] = rnd.Next();
      }

      return values;
    });

    var processData = getData.ContinueWith((x) =>
    {
      int n = x.Result.Length;
      long sum = 0;
      double mean;

      for (int ctr = 0; ctr <= x.Result.GetUpperBound(0); ctr++)
      {
        sum += x.Result[ctr];
      }

      mean = sum / n;

      // Store different types in a Tuple, Tuple is a generic class
      return Tuple.Create(n, sum, mean);
    });

    var displayData = processData.ContinueWith((x) =>
    {
      return String.Format("N={0:N0}, Total={1:N0}, Mean={2:N2}",
                           x.Result.Item1, x.Result.Item2, x.Result.Item3);
    });

    Console.WriteLine(displayData.Result);
  }
}