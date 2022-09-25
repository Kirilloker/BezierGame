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
        UpgradeEffect effect = UpgradeEffect.speed;

        switch (effect)
        {
            case UpgradeEffect.speed:
                Debug.Log("�������� ��������� 1");
                goto case UpgradeEffect.size;
            case UpgradeEffect.size:
                Debug.Log("�������� ��������� 2");
                goto case UpgradeEffect.shield;
            case UpgradeEffect.shield:
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
