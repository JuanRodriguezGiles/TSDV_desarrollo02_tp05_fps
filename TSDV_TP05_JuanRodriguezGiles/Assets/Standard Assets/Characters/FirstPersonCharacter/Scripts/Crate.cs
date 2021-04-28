using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Crate : MonoBehaviour,GameManager.IPickUp
{
    [SerializeField] private int cratePoints = 50;
    public void PickUp()
    {
        GameManager.Get().PlayerScoreAdd(cratePoints);
        GameManager.Get().UpdateHighScore();
        Destroy(gameObject);
    }
}