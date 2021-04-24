using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private int cratePoints = 50;
    public void PickUp()
    {
        GameManager.Get().PlayerScoreAdd(cratePoints);
        Destroy(gameObject);
    }
}
