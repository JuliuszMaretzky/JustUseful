using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int horizontal, vertical;
    [SerializeField] private Tile[,] map;
    [SerializeField] private bool isCreatingConnections;
    [SerializeField] private bool saveMap;
    [SerializeField] private bool deleteMap;
    [SerializeField] private bool loadMap;

    private void Start()
    {
        map = new Tile[horizontal, vertical];

        for (int i = 0; i < horizontal; i++)
        {
            for (int j = 0; j < vertical; j++)
            {
                map[i, j] = 
                    ObjectPooler.Pooler.SpawnFromPool("Tile", new Vector3(i, 0, j), Quaternion.identity).GetComponent<Tile>();
            }
        }
    }

    private void Update()
    {
        if (isCreatingConnections)
        {
            CreateConnections();
            isCreatingConnections = false;
        }
        if(saveMap)
        {
            SaveMap();
            saveMap = false;
        }
        if(deleteMap)
        {
            DeleteMap();
            deleteMap = false;
        }
        if(loadMap)
        {
            LoadMap();
            loadMap = false;
        }

    }

    private void CreateConnections()
    {
        for (int i = 0; i < horizontal; i++)
        {
            for (int j = 0; j < vertical; j++)
            {
                if (!map[i, j].isSigned)
                {
                    continue;
                }

                var counter = 0;

                for (int k = i - 1; k < i + 2; k++)
                {
                    for (int l = j - 1; l < j + 2; l++)
                    {
                        if (k < 0 || k >= horizontal || l < 0 || l >= vertical || (k == i && l == j))
                        {
                            counter++;
                            continue;
                        }

                        if (map[k, l].isSigned)
                        {
                            DirectionEnum dir=DirectionEnum.None;

                            switch (counter)
                            {
                                case 0:
                                    dir = DirectionEnum.SouthWest;
                                    break;
                                case 1:
                                    dir = DirectionEnum.West;
                                    break;
                                case 2:
                                    dir = DirectionEnum.NorthWest;
                                    break;
                                case 3:
                                    dir = DirectionEnum.South;
                                    break;
                                case 5:
                                    dir = DirectionEnum.North;
                                    break;
                                case 6:
                                    dir = DirectionEnum.SouthEast;
                                    break;
                                case 7:
                                    dir = DirectionEnum.East;
                                    break;
                                case 8:
                                    dir = DirectionEnum.NorthEast;
                                    break;
                            }

                            if (dir != DirectionEnum.None)
                            {
                                map[i, j].SetConnection(dir, map[k, l]);
                            }
                        }

                        counter++;
                    }
                }
            }
        }
    }

    public List<Tile> ReturnData()
    {
        return new List<Tile>() { map[0, 0], map[horizontal - 1, vertical - 1]};
    }

    public Tile[,] GetMap()
    {
        return map;
    }

    public void SaveMap()
    {
        SaveLoadSystem.SaveMap(map);
    }

    public void LoadMap()
    {
        var loadedData = SaveLoadSystem.LoadMap();

        var columns = loadedData.columns;
        var rows = loadedData.rows;
        var newMap = new Tile[columns, rows];
        var counter = -1;

        for(int i=0;i<columns;i++)
        {
            for (int j = 0; j < rows; j++)
            {
                counter++;
                newMap[i, j] = 
                    ObjectPooler.Pooler.SpawnFromPool("Tile", new Vector3(i, 0f, j), Quaternion.identity).GetComponent<Tile>();
                newMap[i, j].isSigned = loadedData.signedArray[counter];
            }
        }

        map = newMap;
        CreateConnections();
    }

    private void DeleteMap()
    {
        for (int i = 0; i < map.GetLength(0);i++)
        {
            for (int j = 0; j < map.GetLength(1);j++)
            {
                ObjectPooler.Pooler.BackToPool(map[i, j].gameObject);
            }
        }

        map = null;
    }
}
