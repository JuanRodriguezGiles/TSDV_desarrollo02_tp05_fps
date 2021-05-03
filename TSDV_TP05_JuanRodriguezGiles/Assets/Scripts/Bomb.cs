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
        if (bomb.gameObject.GetInstanceID() != this.id||currentState==EnemyState.FuseLit) return;
        triggeredTime = triggerTime;
        currentState = EnemyState.FuseLit;
    }
    //--------------------------------------------------------------------------------
    void OnBombExploded(Bomb bomb)
    {
        if (bomb.gameObject.GetInstanceID() != this.id) return;
        float overlapRadius = GetComponentInParent<SphereCollider>().radius;
        Collider[] hitColliders = Physics.OverlapSphere(bomb.transform.position, overlapRadius);
        foreach (var hitCollider in hitColliders)
            if (hitCollider.gameObject.tag == "Player")
                GameManager.Get().PlayerHP -= bomb.Damage;
        Destroy(this.gameObject);
    }
    //--------------------------------------------------------------------------------
    void OnBombDestroyed(Bomb bomb)
    {
        if (bomb.gameObject.GetInstanceID() != this.id) return;
        GameManager.Get().PlayerScore += bomb.Points;
        Destroy(this.gameObject);
    }
    //--------------------------------------------------------------------------------
}