using System.Collections;
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
    #region PLAYER
    private int playerHP = 100;
    private int playerScore = 0;
    private int bullets;
    private int clipSize;
    public int getPlayerHP()
    {
        return playerHP;
    }
    public void playerHpHit(int damage)
    {
        playerHP -= damage;
    }
    public void playerScoreAdd(int score)
    {
        playerScore += score;
    }
    #endregion
}