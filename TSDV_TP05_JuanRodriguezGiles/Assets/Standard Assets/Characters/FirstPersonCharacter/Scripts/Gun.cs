﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gun : MonoBehaviour
{
    private Camera mainCamera;
    private int gunRange = 5;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        Vector3 mousePos = Input.mousePosition;

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * gunRange, Color.red);

        RaycastHit hit;
        if (!Input.GetMouseButtonDown(0)) return;
        if (!Physics.Raycast(ray, out hit, gunRange)) return;
        if (hit.rigidbody.gameObject.tag != "Bomb" || hit.rigidbody == null) return;
        hit.rigidbody.GetComponent<Bomb>().ExplodeGun();
    }
}
