using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class Pool : MonoBehaviour
{
    public static Pool instance;

    public List<PoolItem> poolItems;
    public List<GameObject> poolObjects;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolObjects = new List<GameObject>();
        foreach(PoolItem item in poolItems)
        {
            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                poolObjects.Add(obj);
            }
        }
    }

    public GameObject GetObject(string tag)
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            if (!poolObjects[i].activeInHierarchy && poolObjects[i].CompareTag(tag))
            {
                return poolObjects[i];
            }
        }

        foreach(PoolItem item in poolItems)
        {
            if (item.prefab.CompareTag(tag) && item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                poolObjects.Add(obj);
                return obj;
            }
        }
        return null;
    }
}
