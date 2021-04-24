using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    [SerializeField] private int bombDamage = 50;
    [SerializeField] private int bombPoints = 100;
    public void ExplodePlayer()
    {
        GameManager.Get().PlayerHpHit(bombDamage);
        Destroy(gameObject);
        GameManager.Get().CheckGameOver();
    }
    public void ExplodeGun()
    {
        GameManager.Get().PlayerScoreAdd(bombPoints);
        Destroy(gameObject);
    }
}
