using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Bomb : MonoBehaviour, GameManager.IHittable
{
    [SerializeField] private int bombDamage = 50;
    [SerializeField] private int bombPoints = 100;
    public void ExplodePlayer()
    {
        Destroy(gameObject);
        GameManager.Get().PlayerHpHit(bombDamage);
        GameManager.Get().CheckGameOver();
    }
    public void ExplodeGun()
    {
        GameManager.Get().PlayerScoreAdd(bombPoints);
        GameManager.Get().UpdateHighScore();
        Destroy(gameObject);
    }

    public void DealDamage()
    {
        Destroy(gameObject);
        GameManager.Get().PlayerHpHit(bombDamage);
        GameManager.Get().CheckGameOver();
    }
    public void Die()
    {
        GameManager.Get().PlayerScoreAdd(bombPoints);
        GameManager.Get().UpdateHighScore();
        Destroy(gameObject);
    }
}