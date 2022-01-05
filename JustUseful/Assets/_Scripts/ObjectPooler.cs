using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Pooler;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public Transform storage;

    private void Awake()
    {
        if (Pooler == null)
        {
            Pooler = this;
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab, storage);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(storage);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        if (poolDictionary[tag].Peek().activeInHierarchy)
        {
            var newObjectInPool = Instantiate(poolDictionary[tag].Peek(), position, rotation);
            poolDictionary[tag].Enqueue(newObjectInPool);
            newObjectInPool.transform.SetParent(storage);
            return newObjectInPool;
        }

        var objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void BackToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = storage.transform.position;
    }
}