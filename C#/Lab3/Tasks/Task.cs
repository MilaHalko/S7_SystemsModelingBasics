using MassServiceModeling;

namespace Lab3;

public abstract class Task
{
    protected Model model;
    public void Simulate(double time = 1000) => model.Simulate(time);
}