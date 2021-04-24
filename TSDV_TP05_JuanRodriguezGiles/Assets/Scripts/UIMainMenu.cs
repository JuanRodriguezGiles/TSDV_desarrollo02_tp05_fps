using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIMainMenu : MonoBehaviour
{
    public void LoadGameplayScene()
    {
       GameManager.Get().LoadGameplayScene();
    }
}