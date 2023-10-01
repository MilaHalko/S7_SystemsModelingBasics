namespace Lab2;

public class Model
{
    private readonly List<Element> _elements;
    private double _tnext, _tcurr;
    private int _event;

    public Model(List<Element> elements)
    {
        this._elements = elements;
        _tnext = 0.0;
        _event = 0;
        _tcurr = _tnext;
    }

    public void Simulate(double time)
    {
        while (_tcurr < time)
        {
            _tnext = double.MaxValue;
            for (int i = 0; i < _elements.Count; i++)
            {
                if (_elements[i].Tnext < _tnext)
                {
                    _tnext = _elements[i].Tnext;
                    _event = i;
                }
            }

            Console.WriteLine($"\nIt's time for event in {_elements[_event].Name}, time = {_tnext}");

            foreach (var e in _elements)
            {
                e.DoStatistics(_tnext - _tcurr);
            }

            _tcurr = _tnext;

            foreach (var e in _elements)
            {
                e.Tcurr = _tcurr;
            }

            _elements[_event].OutAct();

            foreach (var e in _elements)
            {
                if (e.Tnext == _tcurr)
                {
                    e.OutAct();
                }
            }

            PrintInfo();
        }

        PrintResult();
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