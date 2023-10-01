using Lab2;

Create c = new Create(2.0, "CREATOR");
Process p = new Process(1.0, "PROCESSOR", maxQueue: 5);
Console.WriteLine("id0 = " + c.Name + " id1=" + p.Name);

c.NextElement = p;

List<Element> list = new List<Element>() { c, p };

Model model = new Model(list);
model.Simulate(1000.0);