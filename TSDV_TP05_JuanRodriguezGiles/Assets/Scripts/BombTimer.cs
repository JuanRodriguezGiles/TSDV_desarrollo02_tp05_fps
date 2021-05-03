using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BombTimer : MonoBehaviour
{
    public TMP_Text time;
    private float timer = 5;
    private bool active = false;
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        GameManager.onBombTriggered += OnBombTriggered;
    }
    void OnDisable()
    {
        GameManager.onBombTriggered -= OnBombTriggered;
    }
    //--------------------------------------------------------------------------------
    void UpdateText()
    {
        time.text = timer.ToString();
        timer--;
    }
    //--------------------------------------------------------------------------------
    void OnBombTriggered(Bomb bomb, float triggerTime)
    {
        if (bomb.gameObject.GetInstanceID() != this.GetComponentInParent<Bomb>().id || this.active) return;
        active = true;
        InvokeRepeating("UpdateText", 0, 1);
    }
}