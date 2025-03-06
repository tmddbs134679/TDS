using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enermyPrefab;
    [SerializeField][Range(0.1f, 30f)] float spawnTime = 1f;

    [SerializeField][Range(0, 50)] int poolSize = 5;

    GameObject[] pool;

    float[] randomValues = { -3f, -3.15f, -3.3f, -3.45f, -3.6f };

    void Awake()
    {
        PopulatePool();
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; ++i)
        {
            pool[i] = Instantiate(enermyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }



    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; ++i)
        {
            if (pool[i].activeInHierarchy == false)
            {

                float randomValue = randomValues[UnityEngine.Random.Range(0, randomValues.Length)];
                Vector3 spawnPosition = transform.position + new Vector3(0, randomValue, 0);
                pool[i].transform.position = spawnPosition;
                
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {

        while (true)
        {
            EnableObjectInPool();

            yield return new WaitForSeconds(spawnTime);
        }
    }


}