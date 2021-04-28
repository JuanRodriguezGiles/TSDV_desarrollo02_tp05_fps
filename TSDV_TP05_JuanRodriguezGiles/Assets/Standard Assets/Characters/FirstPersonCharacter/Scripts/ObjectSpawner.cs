using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject bomb;
    public GameObject crate;
    private Vector3 bounds;
    private float bombTimer = 0;
    private float crateTimer = 0;
    [SerializeField] private float bombSpawnTimer = 10;
    [SerializeField] private int bombSpawnQuantity = 100;
    [SerializeField] private float crateSpawnTimer = 20;
    [SerializeField] private int crateSpawnQuantity = 75;
    void Start()
    {
        bounds = terrain.terrainData.bounds.max;
    }

    void Update()
    {
        bombTimer += Time.deltaTime;
        crateTimer += Time.deltaTime;
        if (bombTimer > bombSpawnTimer)
        {
            bombTimer = 0;
            SpawnBombs();
        }
        if (crateTimer > crateSpawnTimer)
        {
            crateTimer = 0;
            SpawnCrates();
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
                spawnPos.z = Random.Range(1, bounds.z);
                spawnPos.y = terrain.terrainData.GetHeight((int)spawnPos.x, (int)spawnPos.z);
            } while (!IsPosValid(spawnPos));
            Instantiate(bomb, spawnPos, Quaternion.identity);
        }
    }
    void SpawnCrates()
    {
        for (int i = 0; i < crateSpawnQuantity; i++)
        {
            Vector3 spawnPos;
            do
            {
                spawnPos.x = Random.Range(1, bounds.x);
                spawnPos.z = Random.Range(1, bounds.z);
                spawnPos.y = terrain.terrainData.GetHeight((int)spawnPos.x, (int)spawnPos.z);
            } while (!IsPosValid(spawnPos));
            Instantiate(crate, spawnPos, Quaternion.identity);
        }
    }
    bool IsPosValid(Vector3 spawnPos)
    {
        float overlapRadius = 5;
        Collider[] hitColliders = Physics.OverlapSphere(spawnPos, overlapRadius);
        foreach (var hitCollider in hitColliders)
            return hitCollider.attachedRigidbody == null;
        return true;
    }
}