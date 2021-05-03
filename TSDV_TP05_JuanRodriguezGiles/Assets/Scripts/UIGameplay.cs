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
        GameManager.onWeaponInfoChange += UpdateGunInfoText;

        GameManager.onPlayerHpChange += UpdateHPText;
        GameManager.onPlayerScoreChange += UpdateScoreText;
        GameManager.onPlayerHighScoreChange += UpdateHighScoreText;
    }
    void OnDisable()
    {
        GameManager.onWeaponInfoChange -= UpdateGunInfoText;

        GameManager.onPlayerHpChange -= UpdateHPText;
        GameManager.onPlayerScoreChange -= UpdateScoreText;
        GameManager.onPlayerHighScoreChange -= UpdateHighScoreText;
    }
    //--------------------------------------------------------------------------------
    void Start()
    {
        UpdateGunInfoText(0);
        UpdateHPText(0);
        UpdateScoreText(0);
        UpdateHighScoreText(0);
    }
    //--------------------------------------------------------------------------------
    void UpdateHPText(int hpChange)
    {
        playerHpText.text = "HP: " + GameManager.Get().PlayerHP.ToString();
    }
    void UpdateScoreText(int scoreChange)
    {
        playerScoreText.text = "Score: " + GameManager.Get().PlayerScore.ToString();
    }
    void UpdateHighScoreText(int scoreChange)
    {
        playerHighScoreText.text = "High Score: " + GameManager.Get().PlayerHighScore.ToString();
    }
    //--------------------------------------------------------------------------------
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