using DistributionRandomizer;
using Lab2;
using Lab2.Elements;

Create c = new Create(2.0);
Process p = new Process(1.0, maxQueue: 5);
Console.WriteLine("id0 = " + c.Name + " id1=" + p.Name);

c.NextElement = p;

List<Element> list = new List<Element>() { c, p };

Model model = new Model(list);
model.Simulate(200.0);