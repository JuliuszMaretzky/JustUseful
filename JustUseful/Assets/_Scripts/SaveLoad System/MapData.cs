using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public int rows;
    public int columns;
    public bool[] signedArray;

    public MapData(Tile[,] map)
    {
        rows = map.GetLength(1); //x value
        columns = map.GetLength(0); // z value
        
        signedArray = new bool[rows * columns];

        var counter = -1;
        
        for(int i=0;i<columns;i++)
        {
            for(int j=0;j<rows;j++)
            {
                counter++;
                signedArray[counter] = map[i, j].GetComponent<Tile>().isSigned;
            }
        }
    }
}
