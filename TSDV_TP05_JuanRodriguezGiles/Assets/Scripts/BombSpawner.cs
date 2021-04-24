using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject bomb;
    [SerializeField] private Vector3 bounds;
    private float time = 0;
    [SerializeField] private float spawnTimer = 2;
    [SerializeField] private int bombSpawnQuantity = 100;
    void Start()
    {
        bounds = terrain.terrainData.bounds.max;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTimer)
        {
            time = 0;
            SpawnBombs();
        }
    }
    void SpawnBombs()
    {
        for (int i = 0; i < bombSpawnQuantity; i++)
        {
            Vector3 spawnPos;
            do
            {
                spawnPos.x = Random.Range(1, bounds.x);
                spawnPos.y = 0.5f;
                spawnPos.z = Random.Range(1, bounds.z);
            } while (!IsPosValid(spawnPos));
            Instantiate(bomb, spawnPos, Quaternion.identity);
        }
    }
    bool IsPosValid(Vector3 spawnPos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(spawnPos, 5);
        foreach (var hitCollider in hitColliders)
        {
            return hitCollider.attachedRigidbody == null;
        }
        return true;
    }
}