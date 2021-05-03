using UnityEngine;
using TMPro;
public class UIGameplay : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    public TMP_Text playerHpText;
    public TMP_Text playerScoreText;
    public TMP_Text gunText;
    public TMP_Text playerHighScoreText;
    //--------------------------------------------------------------------------------
    void OnEnable()
    {
        GameManager.onWeaponSwitch += UpdateGunInfoText;
        GameManager.onWeaponSwitch += OnWeaponSwitch;
        GameManager.onWeaponShoot += UpdateGunInfoText;
        GameManager.onWeaponReload += UpdateGunInfoText;
        GameManager.onPlayerHpChange += UpdateHPText;
    }
    void OnDisable()
    {
        GameManager.onWeaponSwitch -= UpdateGunInfoText;
        GameManager.onWeaponSwitch -= OnWeaponSwitch;
        GameManager.onWeaponShoot -= UpdateGunInfoText;
        GameManager.onWeaponReload -= UpdateGunInfoText;
    }
    void OnWeaponSwitch(int currentWeapon)
    {
        GameManager.onWeaponReload -= UpdateGunInfoText;
        GameManager.onWeaponReload += UpdateGunInfoText;
    }
    //--------------------------------------------------------------------------------
    void Start()
    {
        UpdateGunInfoText(0);
        UpdateHPText(0);
    }
    //--------------------------------------------------------------------------------
    void UpdateHPText(int hpChange)
    {
        playerHpText.text = "HP: " + GameManager.Get().PlayerHP.ToString();
    }
    void UpdateScoreText()
    {

    }
    void UpdateGunInfoText(int currentWeapon)
    {
        Weapon.Weapons weapon = (Weapon.Weapons)currentWeapon;
        switch (weapon)
        {
            case Weapon.Weapons.Pistol:
                gunText.text = GameObject.FindObjectOfType<Pistol>().Bullets.ToString() + " / " +
                               GameObject.FindObjectOfType<Pistol>().ClipSize.ToString();
                break;
            case Weapon.Weapons.BallLauncher:
                gunText.text = GameObject.FindObjectOfType<BallLauncher>().Bullets.ToString() + " / " +
                               GameObject.FindObjectOfType<BallLauncher>().ClipSize.ToString();
                break;
        }
    }
    //--------------------------------------------------------------------------------
}