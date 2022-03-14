using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    public static Action<Achivement> OnAchievementUnlocked;
    public static Action<Achivement> OnProgressUpdated;

    [SerializeField] private AchievementCard achievementCardPrefab;
    [SerializeField] private Transform achievementPanelContainer;
    [SerializeField] private Achivement[] achievements;

    private void Start()
    {
        LoadAchievements();
    }
    private void LoadAchievements()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            AchievementCard card = Instantiate(achievementCardPrefab, achievementPanelContainer);
            card.SetupAchievement(achievements[i]);
        }
    }
    public void AddProgress(string achivementID, int amount)
    {
        Achivement achievementWanted = AchievementExists(achivementID);
        if (achievementWanted != null)
        {
            achievementWanted.AddProgress(amount);
        }
    }
    private Achivement AchievementExists(string achievementID)
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (achievements[i].ID == achievementID)
            {
                return achievements[i];
            }
        }
        return null;
    }
}
