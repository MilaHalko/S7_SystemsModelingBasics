﻿using MassServiceModeling.Elements;

namespace MassServiceModeling.Printers;

public class CreatePrinter : IPrinter
{
    private Create c;

    public CreatePrinter(Create create)
    {
        c = create;
    }

    public void Info()
    {
        Console.WriteLine($"{c.Name} created={c.QuantityProcessed} ");
        // $"tnext={IPrinter.Format(c.NextT)}\n");
    }
    
    public void Statistics()
    {
        Console.WriteLine($"{c.Name}:");
        Console.WriteLine($"\tQuantity = {c.QuantityProcessed}");
    }
}