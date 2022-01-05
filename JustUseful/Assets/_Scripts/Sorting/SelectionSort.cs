using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionSort
{
    public static void Sort<T>(T[] array) where T : System.IComparable
    {
        for(int i=0;i<array.Length-1;i++)
        {
            var minIndex = i;
            var minValue = array[i];

            for(int j=i+1;j<array.Length;j++)
            {
                if(array[j].CompareTo(minValue)<0)
                {
                    minIndex = j;
                    minValue = array[j];
                }
            }

            Swap(array, i, minIndex);
        }
    }

    private static void Swap<T>(T[] array, int first, int second)
    {
        var temp = array[first];
        array[first] = array[second];
        array[second] = temp;
    }
}
