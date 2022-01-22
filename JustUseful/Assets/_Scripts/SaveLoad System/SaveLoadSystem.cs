using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoadSystem
{
    public static void SaveMap(Tile[,] map)
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/map.bobo";
        var stream = new FileStream(path, FileMode.Create);

        var data = new MapData(map);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MapData LoadMap()
    {
        var path = Application.persistentDataPath + "/map.bobo";
        if(File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            var data= formatter.Deserialize(stream) as MapData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log($"Save file not found in {path}");
            return null;
        }
    }
}
