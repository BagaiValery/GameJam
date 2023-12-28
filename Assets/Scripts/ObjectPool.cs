using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    //public GameObject objectToPool;
    [SerializeField] private GameObject prefab;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    public void Init()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            //tmp = Instantiate(objectToPool);
            tmp = Instantiate(prefab) as GameObject;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void SetActiveObjectsFalse()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
                pooledObjects[i].SetActive(false);
            else continue;
        }
    }
}
