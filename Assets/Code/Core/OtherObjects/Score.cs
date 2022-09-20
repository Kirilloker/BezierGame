using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private uint score = 0;
    private uint ScoreWayChangeValue = 1;
    private GameDataManager dataManager;
    
    //»вент смены пути
    public delegate void ScoreReachWayChangeValueHandler();
    public event ScoreReachWayChangeValueHandler ScoreReachWayChangeValue;

    public Score(GameDataManager dataManager)
    {
        this.dataManager = dataManager;
    }

    private void IncreaseScore(uint points)
    {
        score += points;

        dataManager.Record = (int)score;

        //«апускаем ивент дл€ смены пути при достижении счета, кратного некоторому числу
        if ((score % ScoreWayChangeValue) == 0 && ScoreReachWayChangeValue != null)
            ScoreReachWayChangeValue.Invoke();
    }


    //ќбработка событи€ - игрок коснулс€ активной платформы
    public void OnPlayerHitPlatform(Player player, Platform platform)
    {
        uint points = player.ScoreMultiplier;
        IncreaseScore(points);
    }
}
