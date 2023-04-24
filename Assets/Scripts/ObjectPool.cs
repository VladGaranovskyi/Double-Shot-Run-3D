using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PooledObject[] _pool;
    public static ObjectPool instance;

    [System.Serializable]
    private struct PooledObject
    {
        public PooledObject(string m, int amount, GameObject p)
        {
            mention = m;
            amountToPool = amount;
            prefab = p;
            PooledObjects = new List<GameObject>();
        }

        public string mention;
        public int amountToPool;
        public GameObject prefab;
        public List<GameObject> PooledObjects;
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameObject tmp;
        foreach (PooledObject pool_obj in _pool)
        {
            for(int i = 0; i < pool_obj.amountToPool; i++)
            {
                tmp = Instantiate(pool_obj.prefab);
                tmp.SetActive(false);
                pool_obj.PooledObjects.Add(tmp);
            }
        }
    }

    public T GetPooledObject<T>(string m) where T : Component
    {
        PooledObject pool_obj_tmp = FindPooledObject(m);
        foreach (GameObject obj in pool_obj_tmp.PooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                return obj.GetComponent<T>();
            }
        }
        AddBulletToPool(pool_obj_tmp);
        return pool_obj_tmp.PooledObjects[pool_obj_tmp.PooledObjects.Count - 1].GetComponent<T>();
    }

    public void DisablePooledObjects(string m)
    {
        PooledObject pool_obj_tmp = FindPooledObject(m);
        foreach(GameObject obj in pool_obj_tmp.PooledObjects)
        {
            if (obj.activeInHierarchy) obj.SetActive(false);
        }
    }

    public T GetActivePooledObject<T>(string m) where T : Component
    {
        PooledObject pool_obj_tmp = FindPooledObject(m);
        foreach (GameObject obj in pool_obj_tmp.PooledObjects)
        {
            if (obj.activeInHierarchy)
            {
                return obj.GetComponent<T>();
            }
        }
        AddBulletToPool(pool_obj_tmp);
        return pool_obj_tmp.PooledObjects[pool_obj_tmp.PooledObjects.Count - 1].GetComponent<T>();
    }

    private PooledObject FindPooledObject(string m)
    {
        foreach (PooledObject pool_obj in _pool)
        {
            if (pool_obj.mention == m)
            {
                return pool_obj;
            }
        }
        return _pool[0];
    }

    private void AddBulletToPool(PooledObject po)
    {
        GameObject tmp = Instantiate(po.prefab);
        po.PooledObjects.Add(tmp);
    }
}
