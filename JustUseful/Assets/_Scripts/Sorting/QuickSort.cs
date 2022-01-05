using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuickSort
{
    public static void Sort<T>(T[] array) where T:System.IComparable
    {
        Sort(array, 0, array.Length - 1);
    }

    private static T[] Sort<T>(T[] array, int lower, int upper) where T: System.IComparable
    {
        if(lower<upper)
        {
            var p = Partition(array, lower, upper);
            Sort(array, lower, p);
            Sort(array, p + 1, upper);
        }

        return array;
    }

    private static int Partition<T>(T[] array, int lower, int upper) where T:System.IComparable
    {
        var i = lower;
        var j = upper;
        T pivot = array[(lower+upper)/2];

        do
        {
            while (array[i].CompareTo(pivot) < 0)
            {
                i++;
            }
            while (array[j].CompareTo(pivot) > 0)
            {
                j--;
            }

            if (i >= j)
            {
                break;
            }
            Swap(array, i, j);
        }
        while (i <= j);

        return j;
    }

    private static void Swap<T>(T[] array, int first, int second)
    {
        var temp = array[first];
        array[first] = array[second];
        array[second] = temp;
    }
}
