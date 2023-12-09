using Lab2.Elements;
using Lab2.Print;

namespace Lab2;

public class Model
{
    private readonly List<Element> _elements;
    private double _tnext, _tcurr;
    private int _event;

    public Model(List<Element> elements)
    {
        _elements = elements;
    }

    public void Simulate(double time)
    {
        int printTrigger = 0;
        while (_tcurr < time)
        {
            UpdateEventAndNextT();
            // IPrinter.PrintCurrent(_elements[_event]);
            foreach (var element in _elements)
                element.DoStatistics(_tnext - _tcurr);
            
            _tcurr = _tnext;
            UpgradeCurrTForAllElements();
            OutActForAllCurrentElements();
            // IPrinter.Info(_elements);
            if (_tcurr >= printTrigger)
            {
                printTrigger += 100000;
                Console.WriteLine($"tcurr = {_tcurr}");
            }
        }
        IPrinter.Result(_elements);
    }

    private void UpgradeCurrTForAllElements() => _elements.ForEach(e => e.CurrT = _tcurr);

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
        foreach (var element in _elements) if (element.NextT == _tcurr) element.OutAct();
    }
}