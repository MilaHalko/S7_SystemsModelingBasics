using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace MassServiceModeling.Models;

public class ModelHelper
{
    public static Model CreateSequential(int processCount)
    {
        List<Element> elements = new() { new Create() };
        for (int n = 0; n < processCount; n++)
        {
            elements.Add(new Process());
            elements[^2].NextElementsContainer = new NextElementContainer(elements[^1]);
        }

        return new Model(elements);
    }

    public static Model CreateBranching2Levels(int processCount, int nextProcessCount)
    {
        List<Element> allElements = new() { new Create() };
        NextElementsContainerByProbability createContainer = new();
        allElements[^1].NextElementsContainer = createContainer;

        for (int n = 0; n < processCount; n++)
        {
            allElements.Add(new Process());
            createContainer.AddNextElement(allElements[^1], 1.0 / processCount);
            NextElementsContainerByProbability processContainer = new();
            allElements[^1].NextElementsContainer = processContainer;
            for (int m = 0; m < nextProcessCount; m++)
            {
                allElements.Add(new Process(name: "  PROCESS_" + (n+1) + "_" + m));
                processContainer.AddNextElement(allElements[^1], 1.0 / nextProcessCount);
            }
        }

        return new Model(allElements);
    }
}