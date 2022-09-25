using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Language language = Language.ru;
    private bool PrintDebug = true;
    GameDataManager data;

    [SerializeField]
    GameObject prefabItemShop;

    [SerializeField]
    Transform transformItem;

    [SerializeField]
    Text textCoins;

    [SerializeField]
    Text textButton;

    private List<ItemShop> itemsShop = new List<ItemShop>();

    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();

        UpdateTextCoins();
        SetTextButtonBack();

        CreateItems();
    }

    public void CreateItems()
    {
        for (int i = 0; i < Enum.GetNames(typeof(UpgradeEffect)).Length; i++)
        {
            UpgradeEffect nowEffect = (UpgradeEffect)i;

            GameObject itemShop = Instantiate(prefabItemShop, transformItem);
            itemShop.GetComponent<ItemShop>().CreateItem(
                GetInfoEffect(nowEffect, language),
                data.GetInfoEffect(nowEffect),
                GetPriceEffect(nowEffect),
                GetNameIcon(nowEffect),
                language,
                nowEffect,
                this,
                data
                );

            // Задаем размер Item
            itemShop.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 1200);

            itemsShop.Add(itemShop.GetComponent<ItemShop>());
        }
    }

    public bool BuyEffect(UpgradeEffect effect, int price)
    {
        if (data.Coins >= price)
        {
            Debug.Log("Был куплен эффект под номером:" + effect + " По цене:" + price);

            data.Coins -= price;
            data.IncEffect(effect);

            // Говорим всем остальным ячейкам, что им надо обновиться
            for (int i = 0; i < itemsShop.Count; i++)
            {
                itemsShop[i].UpdateElementsUI();
            }

            UpdateTextCoins();

            return true;
        }

        return false;
    }

    private void UpdateTextCoins()
    {
        textCoins.text = data.Coins.ToString();
    }

    private void SetTextButtonBack()
    {
        string t_Button;

        switch (language)
        {
            case Language.en:
                t_Button = "Back Menu";
                break;
            case Language.ru:
                t_Button = "Назад в меню";
                break;
            default:
                t_Button = "Back Menu";
                break;
        }

        textButton.text = t_Button;
    }

    private List<int> GetPriceEffect(UpgradeEffect effect)
    {
        switch (effect)
        {
            case UpgradeEffect.Magnite:
                return priceMagnite;
            case UpgradeEffect.Shield:
                return priceShield;
            case UpgradeEffect.Moution:
                return priceMoution;
            case UpgradeEffect.XScore:
                return priceXScore;
            case UpgradeEffect.Speed:
                return priceSpeed;
            case UpgradeEffect.Health:
                return priceHealth;
            case UpgradeEffect.Size:
                return priceSize;
            default:
                return priceSpeed;
        }

    }

    private string GetNameIcon(UpgradeEffect effect)
    {
        switch (effect)
        {
            case UpgradeEffect.Magnite:
                return iconMagnite;
            case UpgradeEffect.Shield:
                return iconShield;
            case UpgradeEffect.Moution:
                return iconMoution;
            case UpgradeEffect.XScore:
                return iconXScore;
            case UpgradeEffect.Speed:
                return iconSpeed;
            case UpgradeEffect.Health:
                return iconHealth;
            case UpgradeEffect.Size:
                return iconSize;
            default:
                return iconSpeed;
        }
    }

    private List<string> GetInfoEffect(UpgradeEffect effect, Language language)
    {
        switch (effect)
        {
            case UpgradeEffect.Magnite:
                switch (language)
                {
                    case Language.en:
                        return infoMagnitedEN;
                    case Language.ru:
                        return infoMagnitedRU;
                    default:
                        Cout("Такого языка нет");
                        return infoMagnitedEN;
                }

            case UpgradeEffect.Shield:
                switch (language)
                {
                    case Language.en:
                        return infoShieldEN;
                    case Language.ru:
                        return infoShieldRU;
                    default:
                        Cout("Такого языка нет");
                        return infoShieldEN;
                }

            case UpgradeEffect.Moution:
                switch (language)
                {
                    case Language.en:
                        return infoMoutionEN;
                    case Language.ru:
                        return infoMoutionRU;
                    default:
                        Cout("Такого языка нет");
                        return infoMoutionEN;
                }

            case UpgradeEffect.XScore:
                switch (language)
                {
                    case Language.en:
                        return infoXScoreEN;
                    case Language.ru:
                        return infoXScoreRU;
                    default:
                        Cout("Такого языка нет");
                        return infoXScoreEN;
                }

            case UpgradeEffect.Speed:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("Такого языка нет");
                        return infoSpeedEN;
                }
            case UpgradeEffect.Health:
                switch (language)
                {
                    case Language.en:
                        return infoHealtEN;
                    case Language.ru:
                        return infoHealtRU;
                    default:
                        Cout("Такого языка нет");
                        return infoHealtEN;
                }

            case UpgradeEffect.Size:
                switch (language)
                {
                    case Language.en:
                        return infoSizeEN;
                    case Language.ru:
                        return infoSizeRU;
                    default:
                        Cout("Такого языка нет");
                        return infoSizeEN;
                }

            default:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("Такого языка нет");
                        return infoSpeedEN;
                }

        }

    }

    private void Cout(string mess)
    {
        if (PrintDebug) Debug.Log(mess);
    }

    #region Имя иконки эффекта

    string iconMagnite= "s_Magnite";
    string iconShield = "s_Shield";
    string iconMoution = "s_Moution";
    string iconXScore = "s_XScore";
    string iconSpeed = "s_Speed";
    string iconHealth = "s_Health";
    string iconSize = "s_Size";
    //string iconPlayer = "s_Spped";

    #endregion

    #region Цены эффектов

    List<int> priceMagnite = new List<int>()
    {
        10,  20,  30,  40,  50,  60 
    };

    List<int> priceShield = new List<int>()
    {
        10,  20,  30,  40,  50,  60
    };

    List<int> priceMoution = new List<int>()
    {
        10,  20,  30,  40,  50,  60
    };

    List<int> priceXScore = new List<int>()
    {
        10,  20,  30,  40,  50,  60
    };

    List<int> priceSpeed = new List<int>()
    {
        10,  20,  30,  40,  50,  60
    };

    List<int> priceHealth = new List<int>()
    {
        10,  20,  30,  40,  50,  60
    };

    List<int> priceSize = new List<int>()
    {
        10,  20,  30,  40,  50,  60
    };


    #endregion

    #region Описание покупки эффектов

    #region Русский

    List<string> infoMagnitedRU = new List<string>()
    {
        "Получить эффект - он притягивает множитель очков", //1
        "Увеличивает длительность", //2
        "Притягивает уменьшение размера", //3
        "Притягивает увеличение скорости", //4
        "Увеличивает шанс выпадения", //5
        "Притягивает сердечки", //6
    };

    List<string> infoShieldRU = new List<string>()
    {
        "Получить эффект - он добавляет уязвимость к снарядам", //1
        "Увеличивает длительность", //2
        "Увеличивает шанс выпадения", //3
        "Увеличивает длительность", //4
        "Увеличивает шанс выпадения", //5
        "Увеличивает длительность", //6
    };

    List<string> infoMoutionRU = new List<string>()
    {
        "Получить эффект - он замедляет время", //1
        "Увеличивает длительность", //2
        "Усиление слоумо", //3
        "Увеличивает шанс выпадения", //4
        "Увеличивает длительность", //5
        "Усиление слоумо", //6
    };

    List<string> infoXScoreRU = new List<string>()
    {
        "Получить эффект - больше очков получаешь", //1
        "Увеличивает длительность", //2
        "Увеличивает шанс выпадения", //3
        "х3", //4
        "Увеличивает длительность", //5
        "х4", //6
    };

    List<string> infoSpeedRU = new List<string>()
    {
        "Получить эффект - быстрее движешься", //1
        "Увеличивает шанс выпадения", //2
        "Увеличивает прибавляемую скорость", //3
        "Увеличивает шанс выпадения", //4
        "Увеличивает прибавляемую скорость", //5
        "Увеличивает максимальную скорость", //6
    };

    List<string> infoHealtRU = new List<string>()
    {
        "Получить эффект - дается жизнь", //1
        "Увеличивает шанс выпадения", //2
        "Увеличивает прибавляемое хп", //3
        "Увеличивает шанс выпадения", //4
        "Увеличивает прибавляемое хп", //5
        "Увеличивает максимальное хп", //6
    };

    List<string> infoSizeRU = new List<string>()
    {
        "Получить эффект - уменьшается размер", //1
        "Увеличивает шанс выпадения", //2
        "Увеличивает уменьшение размера", //3
        "Увеличивает шанс выпадения", //4
        "Увеличивает уменьшение размера", //5
        "Уменьшает минимальный размера", //6
    };

    #endregion

    #region Английский

    List<string> infoMagnitedEN = new List<string>()
    {
        "Get the effect - it attracts a score multiplier", //1
        "Increases duration", //2
        "Attracts size reduction", //3
        "Attracts an increase in speed", //4
        "Increases drop chance", //5
        "Attracts hearts", //6
    };

    List<string> infoShieldEN = new List<string>()
    {
        "Get effect - it adds vulnerability to projectiles", //1
        "Increases duration", //2
        "Increases drop chance", //3
        "Increases duration", //4
        "Increases drop chance", //5
        "Increases duration", //6
    };

    List<string> infoMoutionEN = new List<string>()
    {
        "Get the effect - it slows down time", //1
        "Increases duration", //2
        "Slowmo gain", //3
        "Increases drop chance", //4
        "Increases duration", //5
        "Slowmo gain", //6
    };

    List<string> infoXScoreEN = new List<string>()
    {
        "Get the effect - you get more points", //1
        "Increases duration", //2
        "Increases drop chance", //3
        "х3", //4
        "Increases duration", //5
        "х4", //6
    };

    List<string> infoSpeedEN = new List<string>()
    {
        "Get the effect - move faster", //1
        "Increases drop chance", //2
        "Increases added speed", //3
        "Increases drop chance", //4
        "Increases added speed", //5
        "Increases top speed", //6
    };

    List<string> infoHealtEN = new List<string>()
    {
        "Get the effect - life is given", //1
        "Increases drop chance", //2
        "Increases added HP", //3
        "Increases drop chance", //4
        "Increases added HP", //5
        "Increases top HP", //6
    };

    List<string> infoSizeEN = new List<string>()
    {
        "Get the effect - size decreases", //1
        "Increases drop chance", //2
        "Increases size reduction", //3
        "Increases drop chance", //4
        "Increases size reduction", //5
        "Decreases the minimum size", //6
    };

    #endregion

    #endregion

}
public enum Language
{
    en,
    ru
}