using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enermyPrefab;
    [SerializeField] float spawnTime = 1f;

    [SerializeField][Range(0, 100)] int poolSize = 5;
    [SerializeField][Range(1, 4)] int Ground = 1;
    GameObject[] pool;

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
                pool[i].transform.position = gameObject.transform.position;
                int randomValue = UnityEngine.Random.Range(0, Ground);
                MonsterInitLayer(pool[i], randomValue);
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


    //몬스터 땅 Layer 및 몬스터 Layer 처리하는 함수 
    public void MonsterInitLayer(GameObject monster, int randomValue)
    {

        int excludeLayers = 0;
        const int totalLayers = 5;

        for (int i = 0; i < totalLayers; i++)
        {
            if (i != randomValue) 
            {
                int layerIndex = LayerMask.NameToLayer("Layer" + i);
                if (layerIndex != -1)
                {
                    excludeLayers |= (1 << layerIndex);
                }
            }
        }
        // Exclude Layers 설정
        CapsuleCollider2D collider = monster.GetComponent<CapsuleCollider2D>();
        collider.excludeLayers = excludeLayers;

        //몬스터 레이어 처리 
        string layerName = "Layer" + randomValue;
        monster.gameObject.layer = LayerMask.NameToLayer(layerName);
        
    }

}