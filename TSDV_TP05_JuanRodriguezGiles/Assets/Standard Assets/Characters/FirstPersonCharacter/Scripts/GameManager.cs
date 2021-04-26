using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        fs = File.OpenRead("playerHighScore.dat");
        playerHighScore = (int)bf.Deserialize(fs);
        fs.Close();
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion
    #region PLAYER
    private int playerHP = 100;
    private int playerScore = 0;
    private int playerHighScore = 0;
    private int bullets = 7;
    private int clipSize = 7;
    public int GetPlayerHP()
    {
        return playerHP;
    }
    public void PlayerHpHit(int damage)
    {
        playerHP -= damage;
    }
    public int GetPlayerScore()
    {
        return playerScore;
    }
    public void PlayerScoreAdd(int score)
    {
        playerScore += score;
    }
    public int GetBullets()
    {
        return bullets;
    }
    public void SetBullets(int num)
    {
        bullets = num;
        if (bullets < 0) bullets = 0;
    }
    public int GetClipSize()
    {
        return clipSize;
    }
    public void CheckGameOver()
    {
        if (playerHP <= 0)
            SceneManager.LoadScene("GameOver");
    }
    #endregion
    #region SavedData
    private FileStream fs;
    private BinaryFormatter bf = new BinaryFormatter();
    void OnApplicationQuit()
    {
        fs = File.OpenWrite("playerHighScore.dat");
        bf.Serialize(fs, playerHighScore);
        fs.Close();
    }
    public void UpdateHighScore()
    {
        if (playerScore >= playerHighScore)
            playerHighScore = playerScore;
    }
    public int GetPlayerHighScore()
    {
        return playerHighScore;
    }
    #endregion
}