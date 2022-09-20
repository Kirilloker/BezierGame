using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private uint score = 0;
    private uint ScoreWayChangeValue = 1;
    private GameDataManager dataManager;
    
    //����� ����� ����
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

        //��������� ����� ��� ����� ���� ��� ���������� �����, �������� ���������� �����
        if ((score % ScoreWayChangeValue) == 0 && ScoreReachWayChangeValue != null)
            ScoreReachWayChangeValue.Invoke();
    }


    //��������� ������� - ����� �������� �������� ���������
    public void OnPlayerHitPlatform(Player player, Platform platform)
    {
        uint points = player.ScoreMultiplier;
        IncreaseScore(points);
    }
}
