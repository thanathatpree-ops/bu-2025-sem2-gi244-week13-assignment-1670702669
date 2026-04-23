using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();
    }

    public GameObject Acquire(int obstacleType)
    {
        List<GameObject> pool = GetPool(obstacleType);
        GameObject prefab = GetPrefab(obstacleType);

        // ค้นหา object ที่ inactive อยู่ใน pool
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        
        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        obstacle.SetActive(false);
        
    }

    private List<GameObject> GetPool(int obstacleType)
    {
        switch (obstacleType)
        {
            case 0: return obstacleBarrelPool;
            case 1: return obstacleBarrierPool;
            case 2: return obstacleStoneWallPool;
            default: return obstacleBarrelPool;
        }
    }

    private GameObject GetPrefab(int obstacleType)
    {
        switch (obstacleType)
        {
            case 0: return obstacleBarrelPrefab;
            case 1: return obstacleBarrierPrefab;
            case 2: return obstacleStoneWallPrefab;
            default: return obstacleBarrelPrefab;
        }
    }
}
