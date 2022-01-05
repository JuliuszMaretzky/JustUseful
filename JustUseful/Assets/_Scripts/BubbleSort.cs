using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BubbleSort
{
    public static void Sort<T>(T[] array) where T:System.IComparable
    {
        for(int i=0;i<array.Length;i++)
        {
            for(int j=0;j<array.Length-1;j++)
            {
                if(array[j].CompareTo(array[j+1])>0)
                {
                    Swap(array, j, j + 1);
                }
            }
        }
    }
    private static void Swap<T>(T[] array, int first, int second)
    {
        var temp = array[first];
        array[first] = array[second];
        array[second] = temp;
    }
}
