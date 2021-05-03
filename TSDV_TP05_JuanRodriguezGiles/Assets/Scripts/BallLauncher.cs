using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BallLauncher : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    public GameObject bulletGameObject;
    float bulletSpeed = 40;
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
        GameManager.onWeaponShoot += Shoot;
        GameManager.onWeaponReload += Reload;
    }
    void OnDisable()
    {
        GameManager.onWeaponShoot -= Shoot;
        GameManager.onWeaponReload -= Reload;
    }
    //--------------------------------------------------------------------------------
    void Shoot(int currentWeapon)
    {
        if (bullets == 0) return;
        bullets--;
        GameManager.Get().OnWeaponInfoChange(currentWeapon);
        GameObject go = Instantiate(bulletGameObject, transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().velocity += transform.parent.forward * bulletSpeed;
    }
    void Reload(int currentWeapon)
    {
        bullets = ClipSize;
        GameManager.Get().OnWeaponInfoChange(currentWeapon);
    }
    //--------------------------------------------------------------------------------
}