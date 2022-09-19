using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private uint score = 0;
    private uint ScoreWayChangeValue = 1;
    
    //Ивент смены пути
    public delegate void ScoreReachWayChangeValueHandler();
    public event ScoreReachWayChangeValueHandler ScoreReachWayChangeValue;

    private void IncreaseScore(uint points)
    {
        score += points;

        // Новая строчка - выглядит она просто супер, согласись
        GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>().Record = (int)score;

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
