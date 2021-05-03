using System;
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
    void OnEnable()
    {
        onPlayerHpChange += UpdatePlayerHp;
        onPlayerScoreChange += UpdatePlayerScore;
        onPlayerHighScoreChange += UpdatePlayerHighScore;
    }
    void OnDisable()
    {
        onPlayerHpChange -= UpdatePlayerHp;
        onPlayerScoreChange -= UpdatePlayerScore;
        onPlayerHighScoreChange -= UpdatePlayerHighScore;
    }
    #endregion

    #region SCENES
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");

        if (File.Exists("playerHighScore.dat"))
        {
            fs = File.OpenRead("playerHighScore.dat");
            PlayerHighScore = (int)bf.Deserialize(fs);
            fs.Close();
        }
    }
    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    #endregion

    #region PLAYER
    public int PlayerHP { get; set; } = 1000;

    public int PlayerScore { get; set; } = 0;

    public int PlayerHighScore { get; set; }

    void UpdatePlayerHp(int hpChange)
    {
        PlayerHP += hpChange;
        CheckGameOver();
    }
    void CheckGameOver()
    {
        if (PlayerHP <= 0)
            LoadGameOverScene();
    }
    void UpdatePlayerScore(int scoreChange)
    {
        PlayerScore += scoreChange;
        if (PlayerScore >= PlayerHighScore)
        {
            OnPlayerHighScoreChange(PlayerScore);
        }
    }
    void UpdatePlayerHighScore(int scoreChange)
    {
        PlayerHighScore = scoreChange;
    }
    #endregion

    #region SAVED_DATA
    FileStream fs;
    BinaryFormatter bf = new BinaryFormatter();
    void OnApplicationQuit()
    {
        fs = File.OpenWrite("playerHighScore.dat");

        bf.Serialize(fs, PlayerHighScore);

        fs.Close();
    }
    #endregion

    #region INTERFACES
    public interface IEnemy
    {
        void OnAttack();
        void OnDie();
    }
    public interface IPickUp
    {
        void OnPickUp();
    }
    #endregion

    #region EVENTS
    //--------------------------------------------------------------------------------
    public static event Action<int> onWeaponSwitch;
    public void OnWeaponSwitch(int selectedWeapon)
    {
        onWeaponSwitch?.Invoke(selectedWeapon);
    }
    public static event Action<int> onWeaponShoot;
    public void OnWeaponShoot(int currentWeapon)
    {
        onWeaponShoot?.Invoke(currentWeapon);
    }
    public static event Action<int> onWeaponReload;
    public void OnWeaponReload(int currentWeapon)
    {
        onWeaponReload?.Invoke(currentWeapon);
    }
    //--------------------------------------------------------------------------------
    public static event Action<Bomb,float> onBombTriggered;
    public void OnBombTriggered(Bomb bomb,float triggerTime)
    {
        onBombTriggered?.Invoke(bomb,triggerTime);
    }
    public static event Action<Bomb> onBombExploded;
    public void OnBombExploded(Bomb bomb)
    {
        onBombExploded?.Invoke(bomb);
    }
    public static event Action<Bomb> onBombDestroyed;
    public void OnBombDestroyed(Bomb bomb)
    {
        onBombDestroyed?.Invoke(bomb);
    }
    //--------------------------------------------------------------------------------
    public static event Action<Crate> onCratePickUp;
    public void OnCratePickUp(Crate crate)
    {
        onCratePickUp?.Invoke(crate);
    }
    //--------------------------------------------------------------------------------
    public static event Action<int> onPlayerHpChange;
    public void OnPlayerHpChange(int hpChange)
    {
        onPlayerHpChange?.Invoke(hpChange);
    }
    public static event Action<int> onPlayerScoreChange;
    public void OnPlayerScoreChange(int scorechange)
    {
        onPlayerScoreChange?.Invoke(scorechange);
    }
    public static event Action<int> onPlayerHighScoreChange;
    public void OnPlayerHighScoreChange(int scorechange)
    {
        onPlayerHighScoreChange?.Invoke(scorechange);
    }
    //--------------------------------------------------------------------------------
    #endregion
}