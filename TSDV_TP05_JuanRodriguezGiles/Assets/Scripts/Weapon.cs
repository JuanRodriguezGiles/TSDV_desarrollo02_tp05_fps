using System;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    public enum Weapons
    {
        Pistol,
        BallLauncher
    } 
    public int currentWeapon;
    //--------------------------------------------------------------------------------
    void WeaponSwitch(int selectedWeapon)
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == selectedWeapon);
            i++;
        }
        currentWeapon = selectedWeapon;
        GameManager.Get().OnWeaponInfoChange(currentWeapon);
    }
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        GameManager.onWeaponSwitch += WeaponSwitch;
    }
    void OnDisable()
    {
        GameManager.onWeaponSwitch -= WeaponSwitch;
    }
    //--------------------------------------------------------------------------------
    void Start()
    {
        WeaponSwitch(0);
    }
    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 5, Color.red);

        if (Input.GetKeyDown(KeyCode.Alpha1)) GameManager.Get().OnWeaponSwitch(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) GameManager.Get().OnWeaponSwitch(1);
        if (Input.GetMouseButtonDown(0)) GameManager.Get().OnWeaponShoot(currentWeapon);
        if (Input.GetKeyDown(KeyCode.R)) GameManager.Get().OnWeaponReload(currentWeapon);
    }
    //--------------------------------------------------------------------------------
}