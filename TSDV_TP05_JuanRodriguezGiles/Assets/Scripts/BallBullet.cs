using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBullet : MonoBehaviour
{
    int damage = 10;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ghost") return;
        GameManager.Get().OnGhostDamaged(collision.gameObject.GetComponent<Ghost>(), damage);
        Destroy(this.gameObject);
    }
}