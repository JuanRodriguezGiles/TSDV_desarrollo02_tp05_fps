using UnityEngine;
public class Bomb : MonoBehaviour, GameManager.IHittable
{
    public delegate void BombDestroyed(Bomb bomb);
    public delegate void BombExploded(Bomb bomb);
    public BombExploded OnBombExploded;
    public BombDestroyed OnBombDestroyed;
    [SerializeField] private int damage = 50;
    [SerializeField] private int points = 100;
    public int Damage => this.damage;
    public int Points => this.points;
    void Awake()
    {
        OnBombExploded += Explosion;
        OnBombDestroyed += DiedAction;
    }
    private void DiedAction(Bomb bomb)
    {
        GameManager.Get().PlayerScore += bomb.points;
        UIGameplay.Get().UpdateScoreText();
        Destroy(gameObject);
    }
    private void Explosion(Bomb bomb)
    {
        Destroy(gameObject);
        GameManager.Get().PlayerHP -= bomb.damage;
        GameManager.Get().CheckGameOver();
        UIGameplay.Get().UpdateHPText();
    }
    public void OnHit()
    {
        OnBombDestroyed?.Invoke(this);
    }
    public void OnDealDamage()
    {
        OnBombExploded?.Invoke(this);
    }
}