using Lab2;
using Lab2.Elements;

Create c = new Create(1);
Process p1 = new Process(3, delay:2.5, maxQueue: 10);
Process p2 = new Process(1, delay: 1, maxQueue: 10);
Process p3 = new Process(1, delay: 3, maxQueue: 10);

c.SetNextElement(p1);
p1.SetNextElement(p2, 0.8);
p1.SetNextElement(p3, 0.2);

Model model = new Model(new List<Element>() {c, p1, p2, p3});
model.Simulate(3000000.0);