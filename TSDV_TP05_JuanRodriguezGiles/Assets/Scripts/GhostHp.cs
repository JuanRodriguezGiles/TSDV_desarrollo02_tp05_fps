using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GhostHp : MonoBehaviour
{
    public TMP_Text hpText;
    private int hp;
    void OnEnable()
    {
        GameManager.onGhostDamaged += UpdateHpText;
        UpdateHpText(GetComponentInParent<Ghost>(), 0);
    }
    void OnDisable()
    {
        GameManager.onGhostDamaged -= UpdateHpText;
    }
    void UpdateHpText(Ghost ghost, int hp)
    {
        if (ghost.gameObject.GetInstanceID() != GetComponentInParent<Ghost>().id) return;
        hpText.text = ghost.Hp.ToString();
    }
}