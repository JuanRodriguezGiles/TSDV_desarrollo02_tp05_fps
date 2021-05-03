using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    public Terrain terrain;
    Vector3 bounds;
    enum ObjectTypes
    {
        Bomb,
        Crate,
        Ghost
    }
    //--------------------------------------------------------------------------------
    public GameObject bombGameObject;
    float bombTimer = 0;
    [SerializeField] float bombSpawnTimer = 2;
    [SerializeField] int bombSpawnQuantity = 100;
    //--------------------------------------------------------------------------------
    public GameObject crateGameObject;
    float crateTimer = 0;
    [SerializeField] float crateSpawnTimer = 20;
    [SerializeField] int crateSpawnQuantity = 75;
    //--------------------------------------------------------------------------------
    public GameObject ghostGameObject;
    float ghostTimer = 0;
    [SerializeField] float ghostSpawnTimer = 5;
    [SerializeField] int ghostSpawnQuantity = 1;
    //--------------------------------------------------------------------------------
    void Start()
    {
        bounds = terrain.terrainData.bounds.max;
    }
    void Update()
    {
        bombTimer += Time.deltaTime;
        crateTimer += Time.deltaTime;
        ghostTimer += Time.deltaTime;
        if (bombTimer > bombSpawnTimer)
        {
            bombTimer = 0;
            SpawnObject(ObjectTypes.Bomb);
        }
        if (crateTimer > crateSpawnTimer)
        {
            crateTimer = 0;
            SpawnObject(ObjectTypes.Crate);
        }
        if (ghostTimer > ghostSpawnTimer)
        {
            ghostTimer = 0;
            SpawnObject(ObjectTypes.Ghost);
        }
    }
    //--------------------------------------------------------------------------------
    void SpawnObject(ObjectTypes type)
    {
        int spawnQuantity;
        GameObject go;
        switch (type)
        {
            case ObjectTypes.Bomb:
                spawnQuantity = bombSpawnQuantity;
                go = bombGameObject;
                break;
            case ObjectTypes.Crate:
                spawnQuantity = crateSpawnQuantity;
                go = crateGameObject;
                break;
            case ObjectTypes.Ghost:
                spawnQuantity = ghostSpawnQuantity;
                go = ghostGameObject;
                break;
            default:
                spawnQuantity = 0;
                go = null;
                break;
        }
        for (int i = 0; i < spawnQuantity; i++)
        {
            Vector3 spawnPos;
            do
            {
                spawnPos.x = Random.Range(1, bounds.x);
                spawnPos.z = Random.Range(1, bounds.z);
                spawnPos.y = terrain.terrainData.GetHeight((int)spawnPos.x, (int)spawnPos.z) + 0.25f;
            } while (!IsPosValid(spawnPos));
            Instantiate(go, spawnPos, Quaternion.identity);
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
    //--------------------------------------------------------------------------------
}