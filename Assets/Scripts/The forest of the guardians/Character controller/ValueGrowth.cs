using UnityEngine;

[System.Serializable]
public struct ValueGrowth {

    public GROWTH_FUNCTION growthFunction;
    public LinearGrowthFunction linearGrowth;
    public PolynomialGrowthFunction polynomialGrowth;
    public ExponentialGrowthFunction exponentialGrowth;
}

[System.Serializable]
public struct ExponentialGrowthFunction
{
    public float initialValue;
    [Range(0f, 1f)]
    public float growRate;

    public ExponentialGrowthFunction(float initialValue, float growRate)
    {
        this.initialValue = initialValue;
        this.growRate = growRate;
    }

    public float ExponentialGrowth(float elapsedTime)
    {
        return initialValue * Mathf.Pow((1 + growRate), elapsedTime);
    }
}

[System.Serializable]
public struct LinearGrowthFunction
{
    public float slope;
    public float yOffset;

    public LinearGrowthFunction(float slope, float yOffset)
    {
        this.slope = slope;
        this.yOffset = yOffset;
    }

    public float LinearGrowth(float elapsedTime)
    {
        return slope * elapsedTime + yOffset;
    }
}

[System.Serializable]
public struct PolynomialGrowthFunction{
    public float[] coefficients;

    public PolynomialGrowthFunction(float[] coefficients, float yOffset)
    {
        this.coefficients = coefficients;
    }

    public float PolynomialGrowth(float elapsedTime)
    {
        int length = coefficients.Length;
        float sum = 0f;
        for (int i = 0; i < length; i++)
        {
            sum += coefficients[i] * Mathf.Pow(elapsedTime, i);
        }

        return sum;
    }
}


public enum GROWTH_FUNCTION
{
    LINEAR,
    POLYNOMIAL,
    EXPONENTIAL
}