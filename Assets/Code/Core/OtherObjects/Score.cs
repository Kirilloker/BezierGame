using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    private uint score = 0;
    private uint ScoreWayChangeValue = 1;
    
    //����� ����� ����
    public delegate void ScoreReachWayChangeValueHandler();
    public event ScoreReachWayChangeValueHandler ScoreReachWayChangeValue;

    private void IncreaseScore(uint points)
    {
        score += points;

        // ����� ������� - �������� ��� ������ �����, ���������
        GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>().Record = (int)score;

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
