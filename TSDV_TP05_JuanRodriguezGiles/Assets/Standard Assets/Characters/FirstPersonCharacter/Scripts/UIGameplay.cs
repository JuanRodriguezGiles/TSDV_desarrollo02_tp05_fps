using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameplay : MonoBehaviour
{
    public TMP_Text playerHpText;
    public TMP_Text playerScoreText;
    public TMP_Text gunText;
    public TMP_Text playerHighScoreText;
    void Update()
    {
        playerHpText.text = "HP: " + GameManager.Get().GetPlayerHP().ToString();
        playerScoreText.text = "Score: " + GameManager.Get().GetPlayerScore().ToString();
        gunText.text = GameManager.Get().GetBullets().ToString() + " / " + GameManager.Get().GetClipSize().ToString();
        playerHighScoreText.text = "High Score: " + GameManager.Get().GetPlayerHighScore().ToString();
    }
}