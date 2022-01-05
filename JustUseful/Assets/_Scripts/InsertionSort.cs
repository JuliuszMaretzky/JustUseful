using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InsertionSort
{
    public static void Sort<T>(T[] array) where T: System.IComparable
    {
        for(int i=1;i<array.Length;i++)
        {
            int j = i;
            while(j>0 && array[j].CompareTo(array[j-1])<0)
            {
                Swap(array, j, j - 1);
                j--;
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
