using Lab2;
using Lab2.Elements;

Create c = new Create(1);
Process p1 = new Process(2, 2.0, maxQueue: 3);
Process p2 = new Process(2, 1.0, maxQueue: 3);
Process p3 = new Process(1, 2.0, maxQueue: 3);

c.SetNextElement(p1);
p1.SetNextElement(p2, 0.1);
p1.SetNextElement(p3, 0.9);

List<Element> list = new List<Element>() { c, p1, p2, p3 };

Model model = new Model(list);
model.Simulate(20.0);