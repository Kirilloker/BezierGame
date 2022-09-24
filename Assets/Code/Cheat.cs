using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    GameDataManager data;
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();
    }

    public void GiveMoney()
    {
        data.Coins += 100;
        Effect effect = Effect.speed;

        switch (effect)
        {
            case Effect.speed:
                Debug.Log("�������� ��������� 1");
                goto case Effect.size;
            case Effect.size:
                Debug.Log("�������� ��������� 2");
                goto case Effect.shield;
            case Effect.shield:
                Debug.Log("�������� ��������� 3");
                break;
            default:
                break;
        }

    }

    public void ResetData()
    {
        Debug.Log("��������� ������");
        data.ResetData();
    }

}
