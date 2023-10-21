using DistributionRandomizer;
using Lab2;
using Lab2.Elements;

Create c = new Create(0.7);
Process p1 = new Process(2, 2.0, maxQueue: 5);
// Process p2 = new Process(1, 2.0, maxQueue: 5);
// Process p3 = new Process(1, 2.0, maxQueue: 5);

c.NextElement = p1;
// p1.NextElement = p2;
// p2.NextElement = p3;

List<Element> list = new List<Element>() { c, p1, /*p2, p3*/ };

Model model = new Model(list);
model.Simulate(10.0);