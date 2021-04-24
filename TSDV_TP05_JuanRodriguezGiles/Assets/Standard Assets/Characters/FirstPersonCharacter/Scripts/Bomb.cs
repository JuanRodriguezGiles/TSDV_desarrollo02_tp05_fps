using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    [SerializeField] private int bombDamage = 50;
    public void Explode()
    {
        GameManager.Get().playerHpHit(bombDamage);
        Destroy(gameObject);
    }
}
