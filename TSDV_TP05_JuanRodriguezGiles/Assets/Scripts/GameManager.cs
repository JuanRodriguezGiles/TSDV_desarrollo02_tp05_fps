﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region INSTANCE
    private static GameManager instance;
    public static GameManager Get()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion
    #region SCENES
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion
}