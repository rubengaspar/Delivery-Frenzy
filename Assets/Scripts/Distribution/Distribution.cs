using UnityEngine;

public static class Distribution
{
    public static int GeneratePoisson(float lambda)
    {
        float L = Mathf.Exp(-lambda);
        float p = 1.0f;
        int k = 0;

        do
        {
            k++;
            p *= UnityEngine.Random.value;
        }
        while (p > L);

        return k - 1;
    }

    public static float PoissonProbability(float lambda, int k)
    {
        return Mathf.Exp(-lambda) * Mathf.Pow(lambda, k) / Factorial(k);
    }

    private static int Factorial(int n)
    {
        int result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }
}