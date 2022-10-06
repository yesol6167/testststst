using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int hostCount;
    public int maxCount;
    public float spawnTime;
    public float curTime;
    public Transform[] spawnPoints;
    public bool[] isSpawn;
    public GameObject host;

    public static SpawnManager _instance;
    private void Start()
    {
        isSpawn = new bool[spawnPoints.Length];
        for(int i = 0; i < isSpawn.Length; ++i)
        {
            isSpawn[i] = false;
        }
        _instance = this;
    }

    private void Update()
    {
        if (curTime >= spawnTime && hostCount < maxCount)
        {
            int x = Random.Range(0, spawnPoints.Length);
            if (!isSpawn[x])
            {
                SpawnHost(x);
            }
        }
        curTime += Time.deltaTime;
    }

    public void SpawnHost(int ranNum)
    {
        curTime = 0;
        hostCount++;
        Instantiate(host, spawnPoints[ranNum]);
        isSpawn[ranNum] = true;
    }
}
