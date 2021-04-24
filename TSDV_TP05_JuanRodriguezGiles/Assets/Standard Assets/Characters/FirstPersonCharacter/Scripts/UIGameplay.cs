using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameplay : MonoBehaviour
{
    public TMP_Text playerHpText;
    public TMP_Text playerScore;
    void Update()
    {
        playerHpText.text = "HP: " + GameManager.Get().GetPlayerHP().ToString();
        playerScore.text = "Score: " + GameManager.Get().GetPlayerScore().ToString();
    }
}