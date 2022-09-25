using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool PrintDebug = true;
    GameDataManager data;

    [SerializeField]
    GameObject prefabItemShop;

    [SerializeField]
    Transform transformItem;

    private List<GameObject> itemsShop = new List<GameObject>();

    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();
        Stard();
    }

    private void Stard()
    {
        Language language = Language.en;

        for (int i = 0; i < Enum.GetNames(typeof(UpgradeEffect)).Length; i++)
        {
            UpgradeEffect nowEffect = (UpgradeEffect)i;

            GameObject itemShop = Instantiate(prefabItemShop, new Vector2(0f, i * -6f), Quaternion.identity ,transformItem);
            itemShop.GetComponent<ItemShop>().CreateItem(
                GetInfoEffect(nowEffect, language),
                data.GetInfoEffect(nowEffect),
                GetPriceEffect(nowEffect),
                GetNameIcon(nowEffect),
                language,
                nowEffect
                );

            itemsShop.Add(itemShop);
        }


    }

    private List<int> GetPriceEffect(UpgradeEffect effect)
    {
        switch (effect)
        {
            case UpgradeEffect.speed:
                return priceSpeed;
            case UpgradeEffect.size:
                return priceSpeed;
            case UpgradeEffect.shield:
                return priceSpeed;
        }

        return new List<int>() { 1000000, 1000000, 1000000, 1000000, 1000000, 1000000 };
    }

    private string GetNameIcon(UpgradeEffect effect)
    {
        switch (effect)
        {
            case UpgradeEffect.speed:
                return iconSpeed;
            case UpgradeEffect.size:
                return iconSpeed;
            case UpgradeEffect.shield:
                return iconSpeed;
        }

        return "Error";
    }

    private List<string> GetInfoEffect(UpgradeEffect effect, Language language)
    {
        switch (effect)
        {
            case UpgradeEffect.speed:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("Такого языка нет");
                        break;
                }
                break;
            case UpgradeEffect.size:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("Такого языка нет");
                        break;
                }
                break;
            case UpgradeEffect.shield:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("Такого языка нет");
                        break;
                }
                break;
            default:
                Cout("Такого эффекта нет!");
                break;
        }

        return new List<string>() { "Error" };
    }

    private void Cout(string mess)
    {
        if (PrintDebug) Debug.Log(mess);
    }

    #region Имя иконки эффекта

    string iconSpeed = "coin";

    #endregion


    #region Цены эффектов

    List<int> priceSpeed = new List<int>()
    {
        10, //1
        20, //2
        30, //3
        40, //4
        50, //5
        60  //6
    };

    #endregion

    #region Описание покупки эффектов

    #region Русский

    List<string> infoSpeedRU = new List<string>()
    {
        "Получить эффект", //1
        "Увеличивает шанс выпадения", //2
        "Увеличивает прибавляемую скорость", //3
        "Увеличивает шанс выпадения", //4
        "Увеличивает прибавляемую скорость", //5
        "Увеличивает максимальную скорость", //6
    };

    #endregion

    #region Английский

    List<string> infoSpeedEN = new List<string>()
    {
        "Get effect", //1
        "Increases spawn chance", //2
        "Increases added speed", //3
        "Increases spawn chance", //4
        "Increases added speed", //5
        "Increases the maximum speed", //6
    };

    #endregion

    #endregion

}
public enum Language
{
    en,
    ru
}