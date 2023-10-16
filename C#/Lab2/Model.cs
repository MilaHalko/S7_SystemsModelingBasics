using Lab2.Elements;

namespace Lab2;

public class Model
{
    private readonly List<Element> _elements;
    private double _tnext, _tcurr;
    private int _event;

    public Model(List<Element> elements)
    {
        this._elements = elements;
    }

    public void Simulate(double time)
    {
        while (_tcurr < time)
        {
            UpdateNextTAndEvent();
            Console.WriteLine($"\nNext event: {_elements[_event].Name}  Time: {_tnext} ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            foreach (var element in _elements)
                element.DoStatistics(_tnext - _tcurr);
            
            _tcurr = _tnext;
            foreach (var e in _elements)
                e.CurrT = _tcurr;

            _elements[_event].OutAct();

            foreach (var e in _elements)
            {
                if (e.NextT == _tcurr)
                {
                    e.OutAct();
                }
            }

            PrintInfo();
        }

        PrintResult();
    }

    private void UpdateNextTAndEvent()
    {
        _tnext = double.MaxValue;
        for (int i = 0; i < _elements.Count; i++)
        {
            if (_elements[i].NextT < _tnext)
            {
                _tnext = _elements[i].NextT;
                _event = i;
            }
        }
    }

    private void PrintInfo()
    {
        foreach (var e in _elements)
        {
            e.PrintInfo();
        }
    }

    private void PrintResult()
    {
        Console.WriteLine("\n-------------RESULTS-------------");
        foreach (var e in _elements)
        {
            e.PrintResult();
            if (e is Process p)
            {
                Console.WriteLine($"mean length of queue = {p.MeanQueue / _tcurr}");
                Console.WriteLine($"failure probability = {p.Failure / (double)p.Quantity}");
            }
        }
    }
}