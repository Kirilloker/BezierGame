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
<<<<<<< HEAD
        UpgradeEffect effect = UpgradeEffect.speed;

        switch (effect)
        {
            case UpgradeEffect.speed:
                Debug.Log("Добавить улучшение 1");
                goto case UpgradeEffect.size;
            case UpgradeEffect.size:
                Debug.Log("Добавить улучшение 2");
                goto case UpgradeEffect.shield;
            case UpgradeEffect.shield:
                Debug.Log("Добавить улучшение 3");
                break;
            default:
                break;
        }

    }

    public void ResetData()
    {
        Debug.Log("Удаляются данные");
        data.ResetData();
=======
>>>>>>> parent of c29e3f8 (Р”РѕР±Р°РІР»РµРЅР° РєРЅРѕРїРєР° РЅР°Р·Р°Рґ РІ РјР°РіР°Р·РёРЅРµ Рё РєРѕР»РёС‡РµСЃС‚РІРѕ РјРѕРЅРµС‚)
    }

}
