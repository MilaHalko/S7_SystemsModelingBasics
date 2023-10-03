﻿namespace Lab1;

public static class ChiCriticalValuesHelper
{
    private static readonly Dictionary<int, double> DataDictionary = new Dictionary<int, double>
    {
        { 1, 3.841 },
        { 2, 5.991 },
        { 3, 7.815 },
        { 4, 9.488 },
        { 5, 11.070 },
        { 6, 12.592 },
        { 7, 14.067 },
        { 8, 15.507 },
        { 9, 16.919 },
        { 10, 18.307 },
        { 11, 19.675 },
        { 12, 21.026 },
        { 13, 22.362 },
        { 14, 23.685 },
        { 15, 24.996 },
        { 16, 26.296 },
        { 17, 27.587 },
        { 18, 28.869 },
        { 19, 30.144 },
        { 20, 31.410 }
    };

    public static bool CheckChiSquared(double chi, int v)
    {
        try
        {
            return chi < DataDictionary[v];
        }
        catch (Exception)
        {
            Console.WriteLine("V is out of range for checking Chi-squared");
            throw;
        }
    }

    public static double GetChiCriticalValue(int v) => DataDictionary[v];
}