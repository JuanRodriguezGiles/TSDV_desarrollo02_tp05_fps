using System.Collections;
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

        if (Input.GetMouseButtonDown(0)) ShootGun(ray);
        if (Input.GetKeyDown(KeyCode.R)) ReloadGun();
    }
    void ShootGun(Ray ray)
    {
        RaycastHit hit;
        GameManager.Get().SetBullets(GameManager.Get().GetBullets() - 1);
        if (!Physics.Raycast(ray, out hit, gunRange) || GameManager.Get().GetBullets() == 0) return;
        if (hit.rigidbody == null) return;
        if (hit.rigidbody.gameObject.tag != "Bomb") return;
        hit.rigidbody.GetComponent<Bomb>().ExplodeGun();
    }
    void ReloadGun()
    {
        GameManager.Get().SetBullets(GameManager.Get().GetClipSize());
    }
}