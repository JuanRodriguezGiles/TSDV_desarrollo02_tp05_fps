using UnityEngine;
public class Bomb : MonoBehaviour,GameManager.IEnemy
{
    //--------------------------------------------------------------------------------
    public void OnDie()
    {
        GameManager.Get().OnBombDestroyed(this);
    }
    public void OnAttack()
    {
        GameManager.Get().OnBombExploded(this);
    }
    //--------------------------------------------------------------------------------
    public int id;
    public enum EnemyState
    {
        Idle,
        FuseLit,
        Explode
    }
    [SerializeField] private EnemyState currentState = EnemyState.Idle;
    public int Damage { get; } = 50;
    public int Points { get; } = 100;
    float explosionRadius = 5;
    float fuseTimer = 5;
    float triggeredTime;
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        id = this.gameObject.GetInstanceID();
        GameManager.onBombTriggered += OnBombTriggered;
        GameManager.onBombExploded += OnBombExploded;
        GameManager.onBombDestroyed += OnBombDestroyed;
    }
    void OnDisable()
    {
        GameManager.onBombTriggered -= OnBombTriggered;
        GameManager.onBombExploded -= OnBombExploded;
        GameManager.onBombDestroyed -= OnBombDestroyed;
        Destroy(transform.parent.gameObject);
    }
    //--------------------------------------------------------------------------------
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.FuseLit:
                if (Time.time > triggeredTime + fuseTimer) currentState = EnemyState.Explode;
                break;
            case EnemyState.Explode:
                GameManager.Get().OnBombExploded(this);
                break;
            default:
                break;
        }
    }
    //--------------------------------------------------------------------------------
    void OnBombTriggered(Bomb bomb, float triggerTime)
    {
        if (bomb.gameObject.GetInstanceID() != this.id || currentState == EnemyState.FuseLit) return;
        triggeredTime = triggerTime;
        currentState = EnemyState.FuseLit;
    }
    //--------------------------------------------------------------------------------
    void OnBombExploded(Bomb bomb)
    {
        if (bomb.gameObject.GetInstanceID() != this.id) return;
        Vector3 playerPos = FindObjectOfType<CharacterController>().transform.position;
        float distance = Vector3.Distance(playerPos, bomb.transform.position);
        if (distance < explosionRadius) 
            GameManager.Get().OnPlayerHpChange(-bomb.Damage);
        Destroy(this.gameObject);
    }
    //--------------------------------------------------------------------------------
    void OnBombDestroyed(Bomb bomb)
    {
        if (bomb.gameObject.GetInstanceID() != this.id) return;
        GameManager.Get().OnPlayerScoreChange(bomb.Points);
        Destroy(this.gameObject);
    }
    //--------------------------------------------------------------------------------
}