using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGameplay : MonoBehaviour
{
    public TMP_Text playerHpText;
    void Update()
    {
        playerHpText.text = "HP: " + GameManager.Get().getPlayerHP().ToString();
    }
}