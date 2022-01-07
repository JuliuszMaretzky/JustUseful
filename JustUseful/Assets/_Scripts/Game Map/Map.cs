using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int horizontal, vertical;
    [SerializeField] private Tile[,] map;

    private void Start()
    {
        map = new Tile[vertical, horizontal];

        for (int i = 0; i < vertical; i++)
        {
            for (int j = 0; j < horizontal; j++)
            {
                ObjectPooler.Pooler.SpawnFromPool("Tile", new Vector3(i, 0, j), Quaternion.identity);
            }
        }
    }
}
