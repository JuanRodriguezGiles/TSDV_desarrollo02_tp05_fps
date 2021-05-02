using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pistol : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    public int Range { get; } = 5;
    public int ClipSize { get; } = 7;

    int bullets = 7;
    public int Bullets
    {
        get => this.bullets;
        set => this.bullets = this.bullets == 0 ? 0 : value;
    }
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        Debug.Log("Pistol enabled");
        GameManager.onWeaponShoot += Shoot;
        GameManager.onWeaponReload += Reload;
    }
    void OnDisable()
    {
        Debug.Log("Pistol disabled");
        GameManager.onWeaponShoot -= Shoot;
        GameManager.onWeaponReload -= Reload;
    }
    //--------------------------------------------------------------------------------
    void Shoot(int currentWeapon)
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        Bullets--;

        if (!Physics.Raycast(ray, out hit, Range) || Bullets == 0) return;
        if (hit.rigidbody.GetComponent<GameManager.IEnemy>() == null) return;
        hit.rigidbody.GetComponent<GameManager.IEnemy>().OnDie();
    }
    void Reload(int currentWeapon)
    {
        bullets = ClipSize;
    }
    //--------------------------------------------------------------------------------
}