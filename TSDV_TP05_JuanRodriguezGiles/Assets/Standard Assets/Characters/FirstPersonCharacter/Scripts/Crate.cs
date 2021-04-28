using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crate : MonoBehaviour,GameManager.IHittable
{
    [SerializeField] private int cratePoints = 50;
    public void PickUp()
    {
        GameManager.Get().PlayerScoreAdd(cratePoints);
        GameManager.Get().UpdateHighScore();
        Destroy(gameObject);
    }
    public void DealDamage()
    {
        throw new System.NotImplementedException();
    }
    public void Die()
    {
        throw new System.NotImplementedException();
    }
}