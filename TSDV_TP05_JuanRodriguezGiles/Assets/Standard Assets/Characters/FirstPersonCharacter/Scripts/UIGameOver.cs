using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameOver : MonoBehaviour
{
    public TMP_Text playerScoreText;
    public TMP_Text playerHighScoreText;
    void Start()
    {
        playerScoreText.text = "Final score: " + GameManager.Get().GetPlayerScore().ToString();
        playerHighScoreText.text = "High Score: " + GameManager.Get().GetPlayerHighScore().ToString();
    }
}
