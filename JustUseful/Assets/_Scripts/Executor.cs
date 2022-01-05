using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executor : MonoBehaviour
{
    [SerializeField] private int arrayLength;
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;

    [SerializeField] private bool isSelectionSortActive;
    [SerializeField] private bool isInsertionSortActive;
    [SerializeField] private bool isBubbleSortActive;



    
    private void Update()
    {
        if(isSelectionSortActive)
        {
            ExecuteSort(SelectionSort.Sort, out isSelectionSortActive);
        }
        if(isInsertionSortActive)
        {
            ExecuteSort(InsertionSort.Sort, out isInsertionSortActive);
        }
        if(isBubbleSortActive)
        {
            ExecuteSort(BubbleSort.Sort, out isBubbleSortActive);
        }
    }

    private int[] CreateIntArray(int length, int min, int max)
    {
        var array = new int[length];

        for(int i=0;i<array.Length;i++)
        {
            array[i] = Random.Range(min, max);
        }

        return array;
    }

    private string PrintArray<T>(T[] array)
    {
        var text = "";
        foreach(var i in array)
        {
            text += i.ToString() + " ";
        }

        return text;
    }

    private void ExecuteSort(System.Action<int[]> SortMethod, out bool sortFlag)
    {
        var newArray = CreateIntArray(arrayLength, minValue, maxValue);
        Debug.Log($"New array: {PrintArray(newArray)}");
        SortMethod(newArray);
        Debug.Log($"Sorted array: {PrintArray(newArray)}");
        sortFlag = false;
    }
}
