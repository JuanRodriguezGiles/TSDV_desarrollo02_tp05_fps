using UnityEngine;
public class Gun : MonoBehaviour
{
    public delegate void GunShot(Gun gun);
    public delegate void GunReload(Gun gun);
    public static GunShot OnGunShotAsStatic;
    public static GunReload OnGunReloadAsStatic;

    private Camera mainCamera;
    private Ray ray;
    private int range = 5;
    private int bullets = 7;
    private int clipSize = 7;

    public int Bullets
    {
        get => this.bullets;
        set => this.bullets = this.bullets == 0 ? 0 : value;
    }
    public int ClipSize => this.clipSize;

    void OnEnable()
    {
        OnGunShotAsStatic += ShootGun;
        OnGunReloadAsStatic += ReloadGun;
    }
    void OnDisable()
    {
        OnGunShotAsStatic -= ShootGun;
        OnGunReloadAsStatic -= ReloadGun;
    }
    void Start()
    {
        mainCamera = Camera.main;
        UIGameplay.Get().UpdateGunInfoText(this);
    }
    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        Vector3 mousePos = Input.mousePosition;

        ray = mainCamera.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (Input.GetMouseButtonDown(0)) OnGunShotAsStatic?.Invoke(this);
        if (Input.GetKeyDown(KeyCode.R)) OnGunReloadAsStatic?.Invoke(this);
    }
    void ShootGun(Gun gun)
    {
        RaycastHit hit;
        gun.Bullets--;
        if (!Physics.Raycast(ray, out hit, gun.range) || gun.bullets == 0) return;
        if (hit.rigidbody.GetComponent<GameManager.IHittable>() == null) return;
        hit.rigidbody.GetComponent<GameManager.IHittable>().OnHit();
    }
    void ReloadGun(Gun gun)
    {
        gun.bullets = gun.ClipSize;
    }
}