using Lab1;
using Lab1.Generators;

GeneratorAnalyser analyser = new();

Console.Out.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.Out.WriteLine("EXPONENTIAL");
// analyser.GetChiAfterAnalysisWithPrint(new ExponentialGenerator(lambda: 90));
// for (double lambda = 0.1; lambda < 100; lambda += 10)
//       analyser.TestChiIsOkPercent(new ExponentialGenerator(lambda: lambda));

Console.Out.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.Out.WriteLine("NORMAL");
// analyser.GetChiAfterAnalysisWithPrint(new NormalGenerator(sigma:0.1, alpha:0.1));
// for (double sigma = 0.1; sigma < 100; sigma += 25)
//      for (double alpha = 0.1; alpha < 100; alpha += 25)
//          analyser.TestChiIsOkPercent(new NormalGenerator(sigma: sigma, alpha: alpha));

Console.Out.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.Out.WriteLine("UNIFORM");
analyser.GetChiAfterAnalysisWithPrint(new UniformGenerator(a: Math.Pow(5,13), c: Math.Pow(6, 15)));
// double[] a = { Math.Pow(3, 4), Math.Pow(4, 10), Math.Pow(5, 13) };
// double[] c = { Math.Pow(6, 15), Math.Pow(6, 10), Math.Pow(3, 17) };
// foreach (var a_ in a)
//      foreach (var c_ in c)
//          analyser.TestChiIsOkPercent(new UniformGenerator(a: a_, c: c_));
        