using Lab1;
using Lab1.Generators;

ExponentialGenerator generator = new();
DistributionAnalyser analyser = new(generator);
analyser.Run();