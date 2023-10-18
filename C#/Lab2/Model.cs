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
            UpdateEventAndNextT();
            Console.WriteLine($"\nNext event: {_elements[_event].Name}  Time: {_tnext} ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            foreach (var element in _elements)
                element.DoStatistics(_tnext - _tcurr);
            
            _tcurr = _tnext;
            foreach (var e in _elements)
                e.CurrT = _tcurr;

            OutActForAllCurrentElements();
            PrintInfoForAllElements();
        }
        PrintResult();
    }

    private void UpdateEventAndNextT()
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

    private void OutActForAllCurrentElements()
    {
        foreach (var element in _elements)
            if (element.NextT == _tcurr) element.OutAct();
    }

    private void PrintInfoForAllElements()
    {
        foreach (var e in _elements)
            e.PrintInfo();
    }

    private void PrintResult()
    {
        Console.WriteLine("\n-------------RESULTS-------------");
        foreach (var element in _elements)
        {
            element.PrintFinalStatistics();
            if (element is Process p)
            {
                Console.Out.WriteLine($"\tWorkTime = {p.WorkTime / _tcurr}");
                Console.WriteLine($"\tMean length of queue = {p.MeanQueue / _tcurr}");
                Console.WriteLine($"\tFailure probability  = {p.Failure / (double)p.Quantity}");
            }
        }
    }
}