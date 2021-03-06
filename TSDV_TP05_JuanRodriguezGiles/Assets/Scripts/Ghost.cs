using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
public class Ghost : MonoBehaviour, GameManager.IEnemy
{
    //--------------------------------------------------------------------------------
    public void OnDie()
    {
        GameManager.Get().OnGhostDeath(this);
    }
    public void OnAttack()
    {
        GameManager.Get().OnGhostAttack(this);
    }
    //--------------------------------------------------------------------------------
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
    const float chaseRange = 15;
    [SerializeField] Vector3 targetPos;
    [SerializeField] Vector3 bounds;
    Terrain terrain;
    public Material[] randomMaterials;
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        id = this.gameObject.GetInstanceID();
        this.gameObject.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)];
        terrain = FindObjectOfType<Terrain>();
        bounds = terrain.terrainData.bounds.center;
        currentState = EnemyState.Moving;
        targetPos = transform.position;
        moveStart = Time.time;
        stopTime = Random.Range(5, 15);

        GameManager.onGhostDamaged += OnGhostDamaged;
        GameManager.onGhostDeath += OnGhostDeath;
        GameManager.onGhostAttack += OnGhostAttack;
    }
    void OnDisable()
    {
        GameManager.onGhostDamaged -= OnGhostDamaged;
        GameManager.onGhostDeath -= OnGhostDeath;
        GameManager.onGhostAttack -= OnGhostAttack;
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
                if (IsPlayerClose())
                    currentState = EnemyState.ChasingPlayer;
                break;
            case EnemyState.ChasingPlayer:
                ChasePlayer();
                if (!IsPlayerClose())
                {
                    currentState = EnemyState.Idle;
                    idleStart = Time.time;
                    idlePos = transform.position;
                }
                break;
            case EnemyState.Idle:
                Hover();
                if (Time.time > idleStart + stopTime)
                {
                    targetPos = transform.position;
                    moveStart = Time.time;
                    currentState = EnemyState.Moving;
                }
                if (IsPlayerClose())
                    currentState = EnemyState.ChasingPlayer;
                break;
            default:
                break;
        }
    }
    //--------------------------------------------------------------------------------
    void OnGhostDamaged(Ghost ghost,int damage)
    {
        if (ghost.gameObject.GetInstanceID() != this.id) return;
        Hp -= damage;
        currentState = EnemyState.Idle;
        idleStart = Time.time;
        idlePos = transform.position;
        if (Hp == 0)
            OnDie();
    }
    void OnGhostDeath(Ghost ghost)
    {
        if (ghost.gameObject.GetInstanceID() != this.id) return;
        GameManager.Get().OnPlayerScoreChange(ghost.Points);
        Destroy(this.gameObject);
    }
    void OnGhostAttack(Ghost ghost)
    {
        if (ghost.gameObject.GetInstanceID() != this.id) return;
        GameManager.Get().OnPlayerHpChange(-ghost.Damage);
    }
    //--------------------------------------------------------------------------------
    void GetNewTargetPos()
    {
        targetPos.x = Random.Range(bounds.x - 30, bounds.x + 30);
        targetPos.z = Random.Range(bounds.z - 30, bounds.z + 30);
        targetPos.y = terrain.terrainData.GetHeight((int)targetPos.x, (int)targetPos.z) + 2;
    }
    void MoveGhost()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x,
            terrain.terrainData.GetHeight((int)transform.position.x, (int)transform.position.z) + 2,
            transform.position.z);

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
    bool IsPlayerClose()
    {
        return Vector3.Distance(transform.position, FindObjectOfType<CharacterController>().transform.position) <
               chaseRange;
    }
    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            FindObjectOfType<CharacterController>().transform.position, moveSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x,
            terrain.terrainData.GetHeight((int)transform.position.x, (int)transform.position.z) + 2,
            transform.position.z);

        transform.LookAt(FindObjectOfType<CharacterController>().transform.position, Vector3.up);
    }
    //--------------------------------------------------------------------------------
}