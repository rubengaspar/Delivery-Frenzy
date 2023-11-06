using UnityEngine;

public static class Distribution
{

    // Generate a random number from a normal distribution
    #region Normal Distribution
    public static float Normal(float mean, float minValue, float maxValue)
    {
        float stdDev = (maxValue - minValue) / 6;

        float rand1 = Random.Range(0f, 1f);
        float rand2 = Random.Range(0f, 1f);

        float RandomNormal_BoxMuller = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos(2.0f * Mathf.PI * rand2);
        float value = mean + stdDev * RandomNormal_BoxMuller;

        return Mathf.Clamp(value, minValue, maxValue);
    }
    #endregion

    // Generate a random number from an exponential distribution
    #region Exponential Distribution
    public static float Exponential(float rate)
    {
        float uniform = Random.Range(0f, 1f);
        return -Mathf.Log(uniform) / rate;
    }
    #endregion

    // #region Poisson Distribution


}