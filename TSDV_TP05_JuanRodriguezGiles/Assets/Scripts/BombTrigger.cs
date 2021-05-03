using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombTrigger : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    void OnDisable()
    {
        Destroy(transform.parent.gameObject);
    }
    //--------------------------------------------------------------------------------
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
            GameManager.Get().OnBombTriggered(GetComponentInChildren<Bomb>(), Time.time);
    }
    //--------------------------------------------------------------------------------
}