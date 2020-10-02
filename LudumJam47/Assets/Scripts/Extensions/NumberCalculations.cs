using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathExtensions
{
    public static class NumberComparisons     
    {
        public static bool IsInRange(int val, int min, int max) => (val >= min && val <= max);
        public static bool IsInRange(float val, float min, float max) => (val >= min && val <= max);

        public static bool IsOutOfRange(int val, int min, int max) => (val <= min || val >= max);
        public static bool IsOutOfRange(float val, float min, float max) => (val <= min || val >= max);

        public static bool IsMultipleOf(int number, int multipleOf) => (number % multipleOf == 0);

        public static bool IsXGreaterThanY(int x, int y) => x > y;
        public static bool IsXGreaterThanY(float x, float y) => x > y;
    }

    public static class NumberInvestigator
    {
        public static bool IsPrime(int val)
        {
            int m = val / 2;
            for (int i = 2; i <= m; i++)
            {
                if(val % i == 0)
                {
                    return false;
                }
            }
            return true;
        }   
    }

    public static class Functions
    {
        public static float Sigmoid(float maxY, float curveGradient, float curX, float xLimit)
        {
            return maxY * (1 / (1+ Mathf.Exp(-curveGradient * (xLimit - curX))));
        }
    }
}