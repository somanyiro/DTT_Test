
using System;
using Random = UnityEngine.Random;

public static class Helpers
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
    
    public static T[] InitializeArray<T>(int length) where T : new()
    {
        T[] array = new T[length];
        for (int i = 0; i < length; ++i)
        {
            array[i] = new T();
        }

        return array;
    }
    
    public static T[,] InitializeArray<T>(int lengthX, int lengthY) where T : new()
    {
        T[,] array = new T[lengthX, lengthY];
        for (int i = 0; i < lengthX; ++i)
        {
            for (int j = 0; j < lengthY; j++)
            {  
                array[i,j] = new T();
            }
        }

        return array;
    }
}
