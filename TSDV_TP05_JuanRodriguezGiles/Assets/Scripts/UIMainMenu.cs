using TMPro;
using UnityEngine;
public class UIMainMenu : MonoBehaviour
{
    public TMP_Text highScoreText;
    void Start()
    {
        highScoreText.text = "High Score: " + GameManager.Get().PlayerHighScore.ToString();
    }
    public void LoadGameplayScene()
    {
       GameManager.Get().LoadGameplayScene();
    }
}