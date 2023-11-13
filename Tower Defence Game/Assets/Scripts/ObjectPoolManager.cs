using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    public List<PoolObjectInfo> ObjectPools = new List<PoolObjectInfo>();

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _particleSystemEmpty;
    private static GameObject _gameObjectsEmpty;

    public enum PoolType
    {
        ParticleSystem,
        GameObject,
        None
    }

    public static PoolType PoolingType;

    private void Awake()
    {
        Instance = this;
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particleSystemEmpty = new GameObject("Particle Effects");
        _particleSystemEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);

        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }

    public GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PoolObjectInfo pool = ObjectPools.Find(p => p.LookUpString == objectToSpawn.name);

        // If the pool doesn't exist, create it
        if (pool == null)
        {
            pool = new PoolObjectInfo() { LookUpString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        //Check if there are any inactive objects in the pool 
        GameObject spawnableObj = pool.InActiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);

            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            Debug.Log("Spawn" + spawnPosition);
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InActiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Replace("(Clone)", string.Empty); //Remove (Clone)
        PoolObjectInfo pool = ObjectPools.Find(p => p.LookUpString == goName);

        if (pool == null)
        {
            Debug.Log("Trying to release an object that is not pooled");
        }
        else
        {
            obj.SetActive(false);
            pool.InActiveObjects.Add(obj);
        }
    }

    private GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.ParticleSystem:
                return _particleSystemEmpty;
            case PoolType.GameObject:
                return _gameObjectsEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }
}

public class PoolObjectInfo
{
    public string LookUpString;
    public List<GameObject> InActiveObjects = new List<GameObject>();
}