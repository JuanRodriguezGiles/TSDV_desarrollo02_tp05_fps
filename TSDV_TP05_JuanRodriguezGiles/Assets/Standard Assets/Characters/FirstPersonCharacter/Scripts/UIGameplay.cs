using UnityEngine;
using TMPro;
public class UIGameplay : MonoBehaviour
{
    #region INSTANCE
    private static UIGameplay instance;
    public static UIGameplay Get()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public TMP_Text playerHpText;
    public TMP_Text playerScoreText;
    public TMP_Text gunText;
    public TMP_Text playerHighScoreText;
    void OnEnable()
    {
        Gun.OnGunShotAsStatic += UpdateGunInfoText;
        Gun.OnGunReloadAsStatic += UpdateGunInfoText;
    }
    void OnDisable()
    {
        Gun.OnGunShotAsStatic -= UpdateGunInfoText;
        Gun.OnGunReloadAsStatic -= UpdateGunInfoText;
    }
    void Start()
    {
        UpdateScoreText();
        UpdateHPText();
    }
    public void UpdateHPText()
    {
        playerHpText.text = "HP: " + GameManager.Get().PlayerHP.ToString();
    }
    public void UpdateScoreText()
    {
        playerScoreText.text = "Score: " + GameManager.Get().PlayerScore.ToString();

        if (GameManager.Get().PlayerScore >= GameManager.Get().PlayerHighScore)
            GameManager.Get().PlayerHighScore = GameManager.Get().PlayerScore;
        playerHighScoreText.text = "High Score: " + GameManager.Get().PlayerHighScore.ToString();
    }
    public void UpdateGunInfoText(Gun gun)
    {
        gunText.text = gun.Bullets.ToString() + " / " + gun.ClipSize.ToString();
    }
}