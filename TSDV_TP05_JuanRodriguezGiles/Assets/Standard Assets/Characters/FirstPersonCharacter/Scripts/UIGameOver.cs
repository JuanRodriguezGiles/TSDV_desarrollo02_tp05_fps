using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameOver : MonoBehaviour
{
    public TMP_Text playerScore;
    void Start()
    {
        playerScore.text = "Final score: " + GameManager.Get().GetPlayerScore().ToString();
    }
}
