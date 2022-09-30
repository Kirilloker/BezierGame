using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private uint score = 0;
    private uint ScoreWayChangeValue = 2;
    private GameDataManager dataManager;
    
    //Ивент смены пути
    public delegate void ScoreReachWayChangeValueHandler();
    public event ScoreReachWayChangeValueHandler ScoreReachWayChangeValue;

    // Для UI
    public delegate void ScoreChangeValueHandler(uint score);
    public event ScoreChangeValueHandler ScoreChangeValue;

    public Score(GameDataManager dataManager)
    {
        this.dataManager = dataManager;
    }

    private void IncreaseScore(uint points)
    {
        score += points;

        ScoreChangeValue.Invoke(score);

        dataManager.Record = (int)score;

        //Запускаем ивент для смены пути при достижении счета, кратного некоторому числу
        if ((score % ScoreWayChangeValue) == 0 && ScoreReachWayChangeValue != null)
            ScoreReachWayChangeValue.Invoke();
    }


    //Обработка события - игрок коснулся активной платформы
    public void OnPlayerHitPlatform(Player player, Platform platform)
    {
        uint points = player.ScoreMultiplier;
        IncreaseScore(points);
    }
}
