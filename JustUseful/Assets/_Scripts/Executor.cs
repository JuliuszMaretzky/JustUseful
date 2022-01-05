using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executor : MonoBehaviour
{
    [Header("Selection Sort")]
    [SerializeField] private bool isSelectionSortActive;
    [SerializeField] private int arrayLength;
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;

    
    private void Update()
    {
        if(isSelectionSortActive)
        {
            var newArray = CreateIntArray(arrayLength, minValue, maxValue);
            Debug.Log($"New array: {PrintArray(newArray)}");
            SelectionSort.Sort(newArray);
            Debug.Log($"Sorted array: {PrintArray(newArray)}");
            isSelectionSortActive = false;
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
}
