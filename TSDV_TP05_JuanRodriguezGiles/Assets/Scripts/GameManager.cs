﻿using System;
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
    public int PlayerHP { get; set; } = 100;

    public int PlayerScore { get; set; } = 0;

    public int PlayerHighScore { get; set; } = 0;

    void CheckGameOver()
    {
        if (PlayerHP <= 0)
            LoadGameOverScene();
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
    //--------------------------------------------------------------------------------
    public static event Action<int> onWeaponShoot;
    public void OnWeaponShoot(int currentWeapon)
    {
        onWeaponShoot?.Invoke(currentWeapon);
    }
    //--------------------------------------------------------------------------------
    public static event Action<int> onWeaponReload;
    public void OnWeaponReload(int currentWeapon)
    {
        onWeaponReload?.Invoke(currentWeapon);
    }
    //--------------------------------------------------------------------------------
    #endregion
    void Update()
    {

    }
}