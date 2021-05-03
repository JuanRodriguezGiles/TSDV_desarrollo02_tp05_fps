using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
public class Ghost : MonoBehaviour, GameManager.IEnemy
{
    //--------------------------------------------------------------------------------
    public void OnDie()
    {

    }
    public void OnAttack()
    {

    }
    //--------------------------------------------------------------------------------
    public Material[] randomMaterials;
    public int id;
    enum EnemyState
    {
        Idle,
        Moving,
        ChasingPlayer
    }
    [SerializeField] private EnemyState currentState;
    public int Hp { get; set; } = 30;
    public int Damage { get; } = 10;
    public int Points { get; } = 200;
    float moveStart;
    float idleStart;
    float stopTime;
    Vector3 idlePos;
    const float moveSpeed = 5;
    const float chaseRange = 5;
    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 bounds;
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        this.gameObject.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)];
        bounds = FindObjectOfType<Terrain>().terrainData.bounds.center;
        currentState = EnemyState.Moving;
        targetPos = transform.position;
        moveStart = Time.time;
        stopTime = Random.Range(5, 15);
    }
    //--------------------------------------------------------------------------------
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                if (Vector3.Distance(transform.position, targetPos) < 5f)
                    GetNewTargetPos();
                MoveGhost();
                if (Time.time > moveStart + stopTime)
                {
                    currentState = EnemyState.Idle;
                    idleStart = Time.time;
                    idlePos = transform.position;
                }
                break;
            case EnemyState.ChasingPlayer:
                break;
            case EnemyState.Idle:
                Hover();
                if (Time.time > idleStart + stopTime)
                {
                    targetPos = transform.position;
                    moveStart = Time.time;
                    currentState = EnemyState.Moving;
                }
                break;
            default:
                break;
        }
    }
    //--------------------------------------------------------------------------------
    void GetNewTargetPos()
    {
        targetPos.x = Random.Range(bounds.x - 50, bounds.x + 50);
        targetPos.z = Random.Range(bounds.z - 50, bounds.z + 50);
        targetPos.y = FindObjectOfType<Terrain>().terrainData.GetHeight((int)targetPos.x, (int)targetPos.z) + 2;
    }
    void MoveGhost()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, FindObjectOfType<Terrain>().terrainData
            .GetHeight((int)transform.position.x, (int)transform.position.z) + 2, transform.position.z);

        transform.LookAt(targetPos, Vector3.up);
    }
    void Hover()
    {
        float amplitude = 0.5f;
        float frequency = 1f;
        Vector3 tempPos = idlePos;

        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}