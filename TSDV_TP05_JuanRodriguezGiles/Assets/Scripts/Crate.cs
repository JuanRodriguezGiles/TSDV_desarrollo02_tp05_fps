﻿using UnityEngine;
public class Crate : MonoBehaviour,GameManager.IPickUp
{
    public int id;
    int points = 50;
    void OnEnable()
    {
        id = this.gameObject.GetInstanceID();
        GameManager.onCratePickUp += OnCratePickUp;
    }
    void OnDisable()
    {
        GameManager.onCratePickUp -= OnCratePickUp;
    }
    void OnCratePickUp(Crate crate)
    {
        if (crate.gameObject.GetInstanceID() != this.id) return;
        GameManager.Get().OnPlayerScoreChange(points);
        Destroy(crate.gameObject);
    }
    public void OnPickUp()
    {
        GameManager.Get().OnCratePickUp(this);
    }
}