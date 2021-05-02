using UnityEngine;
using TMPro;
public class UIGameOver : MonoBehaviour
{
    public TMP_Text playerScoreText;
    public TMP_Text playerHighScoreText;
    void Start()
    {
        playerScoreText.text = "Final score: " + GameManager.Get().PlayerScore.ToString();
        playerHighScoreText.text = "High Score: " + GameManager.Get().PlayerHighScore.ToString();
    }
}
