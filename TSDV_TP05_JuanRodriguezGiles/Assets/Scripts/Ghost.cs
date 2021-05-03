using System.Collections;
using System.Collections.Generic;
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
    public int id;
    enum EnemyState
    {
        Idle,
        Moving,
        ChasingPlayer
    }
    [SerializeField] private EnemyState currentState = EnemyState.Moving;
    public int Hp { get; set; } = 30;
    public int Damage { get; } = 10;
    public int Points { get; } = 200;

    float moveSpeed = 5;
    float chaseRange;
    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 bounds;
    //--------------------------------------------------------------------------------
    void Start()
    {
        bounds = FindObjectOfType<Terrain>().terrainData.bounds.center;
        targetPos = transform.position;
    }
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Moving:
                if (Vector3.Distance(transform.position, targetPos) < 5f)
                {
                    targetPos.x = Random.Range(bounds.x - 50, bounds.x + 50);
                    targetPos.z = Random.Range(bounds.z - 50, bounds.z + 50);
                    targetPos.y = FindObjectOfType<Terrain>().terrainData
                        .GetHeight((int)targetPos.x, (int)targetPos.z);
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPos,
                    moveSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, FindObjectOfType<Terrain>().terrainData
                    .GetHeight((int)transform.position.x, (int)transform.position.z) + 2, transform.position.z);
                transform.LookAt(targetPos, Vector3.up);
                break;
            case EnemyState.ChasingPlayer:
                break;
            case EnemyState.Idle:
            default:
                break;
        }
    }
    //--------------------------------------------------------------------------------
}
