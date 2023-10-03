using Lab1;
using Lab1.Generators;

GeneratorAnalyser analyser = new();

Console.Out.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.Out.WriteLine("EXPONENTIAL");
analyser.RunFullAnalysis(new ExponentialGenerator());

Console.Out.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.Out.WriteLine("NORMAL");
analyser.RunFullAnalysis(new NormalGenerator());

Console.Out.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
Console.Out.WriteLine("UNIFORM");
analyser.RunFullAnalysis(new UniformGenerator());