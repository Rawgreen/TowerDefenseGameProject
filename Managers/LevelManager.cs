using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int lives = 10;

    // creating property
    public int TotalLives { get; set; }
    // to keep number of which wave player is in
    public int CurrentWave { get; set; }
    // need to initialize value of totallife property
    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;
    }
    // need to subscribe to event with the method i want to call
    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            // Game Over
            GameOver();
        }
    }
    private void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel();
        Time.timeScale = 0f;
    }
    private void WaveCompleted()
    {
        CurrentWave++;
        //adding progress to Wave Achievement
        AchievementManager.Instance.AddProgress("Waves10", 1);
        AchievementManager.Instance.AddProgress("Waves20", 1);
        AchievementManager.Instance.AddProgress("Waves50", 1);
        AchievementManager.Instance.AddProgress("Waves100", 1);
    }
    // will work when level manager gets enabled
    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        Spawner.OnWaveCompleted += WaveCompleted;
    }
    // will work when level manager gets disabled
    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }
}
