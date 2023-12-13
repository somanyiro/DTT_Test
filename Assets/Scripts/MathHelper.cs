
using System;
using Random = UnityEngine.Random;

public static class MathHelper
{
    public static void KnuthShuffle<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            T holder = array[i];
            int randIndex = Random.Range(i, array.Length);
            array[i] = array[randIndex];
            array[randIndex] = holder;
        }
    }
}
