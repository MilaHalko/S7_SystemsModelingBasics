using MassServiceModeling.Elements;
using MassServiceModeling.Printers;

namespace MassServiceModeling;

public class Model
{
    private readonly List<Element> _elements;
    private double _tnext, _tcurr;
    private int _event;

    public Model(List<Element> elements, double startTime = 0)
    {
        _elements = elements;
        _tcurr = startTime;
        SetStartTimeForCreateElements();
    }

    private void SetStartTimeForCreateElements()
    {
        foreach (var element in _elements)
            if (element is Create create)
                create.NextT = _tcurr;
    }

    public void Simulate(double time, double startTime = 0, bool printSteps = false, bool printTCurr = false)
    {
        int printTrigger = 0;
        while (_tcurr < time)
        {
            UpdateEventAndNextT();
            if (printSteps) IPrinter.PrintCurrent(_elements[_event]);
            DoStatisticsForAllElements();

            _tcurr = _tnext;
            UpgradeCurrTForAllElements();
            OutActForAllCurrentElements();

            if (printTCurr && _tcurr >= printTrigger)
            {
                printTrigger += 100000;
                Console.WriteLine($"tcurr = {_tcurr}");
            }
            if (printSteps) IPrinter.Info(_elements);
        }

        IPrinter.Result(_elements);
    }

    private void DoStatisticsForAllElements() => _elements.ForEach(e => e.DoStatistics(_tnext - _tcurr));
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
        foreach (var element in _elements)
            if (element.NextT == _tcurr)
                element.OutAct();
    }
}