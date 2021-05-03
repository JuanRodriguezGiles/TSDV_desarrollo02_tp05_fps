using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallLauncher : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    public GameObject bulletGameObject;
    public int ClipSize { get; } = 5;
    int bullets = 5;
    public int Bullets
    {
        get => this.bullets;
        set => this.bullets = this.bullets == 0 ? 0 : value;
    }
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        Debug.Log("Ball enabled");
        GameManager.onWeaponShoot += Shoot;
        GameManager.onWeaponReload += Reload;
    }
    void OnDisable()
    {
        Debug.Log("Ball disabled");
        GameManager.onWeaponShoot -= Shoot;
        GameManager.onWeaponReload -= Reload;
    }
    //--------------------------------------------------------------------------------
    void Shoot(int currentWeapon)
    {
        Bullets--;

        if (Bullets == 0) return;
        GameObject go = Instantiate(bulletGameObject, transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().velocity += transform.parent.forward * 20;
    }
    void Reload(int currentWeapon)
    {
        bullets = ClipSize;
    }
    //--------------------------------------------------------------------------------
}