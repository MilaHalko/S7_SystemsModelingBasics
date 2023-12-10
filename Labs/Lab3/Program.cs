using Lab3.Tasks.Task1;
using Lab3.Tasks.Task2;
using Lab3.Tasks.Task3;

Task1 task1 = new();
Task2CarBank task2 = new();
Task3Hospital task3 = new();

// task1.Model.Simulate(100, printSteps: true);
// task2.Model.Simulate(10, printSteps: true);
task3.Model.Simulate(1000);