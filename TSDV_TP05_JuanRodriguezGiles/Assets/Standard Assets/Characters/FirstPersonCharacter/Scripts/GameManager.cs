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

        if (File.Exists("playerHighScore.dat"))
        {
            fs = File.OpenRead("playerHighScore.dat");
            playerHighScore = (int)bf.Deserialize(fs);
            fs.Close();
        }
    }
    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion

    #region PLAYER
    private int playerHP = 100;
    private int playerScore = 0;
    private int playerHighScore = 0;
    public int PlayerHP
    {
        get => this.playerHP;
        set => this.playerHP = value;
    }
    public int PlayerScore
    {
        get => this.playerScore;
        set => this.playerScore = value;
    }
    public int PlayerHighScore
    {
        get => this.playerHighScore;
        set => this.playerHighScore = value;
    }
    public void CheckGameOver()
    {
        if (playerHP <= 0)
            LoadGameOverScene();
    }
    #endregion

    #region SAVED_DATA
    private FileStream fs;
    private BinaryFormatter bf = new BinaryFormatter();
    void OnApplicationQuit()
    {
        fs = File.OpenWrite("playerHighScore.dat");

        bf.Serialize(fs, playerHighScore);

        fs.Close();
    }
    #endregion

    #region INTERFACES
    public interface IHittable
    {
        void OnDealDamage();
        void OnHit();
    }
    public interface IPickUp
    {
        void OnPickUp();
    }
    #endregion
}