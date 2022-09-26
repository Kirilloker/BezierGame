using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // Если true - в косноль будут выводиться логи
    bool PrintDebug = true;

    // Данные игры
    GameDataManager data;

    // Префаб ячейки-улучшения в магазине
    [SerializeField]
    GameObject prefabItemShop;

    // Куда будут создаваться ячейки-улучшения
    [SerializeField]
    Transform transformItem;
    
    // Число монеток
    [SerializeField]
    Text textCoins;

    // Надпись на кнопке, которая вернет в главное меню
    [SerializeField]
    Text textButton;

    // Ячейки-улучшения 
    List<ItemShop> itemsShop = new List<ItemShop>();
 
    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();

        UpdateTextCoins();
        SetTextButtonBack(data.Language);
        CreateItems();
    }


    // Приобретаем эффект
    public bool BuyEffect(UpgradeEffect effect, int price)
    {
        // Если нам хватает монет для покупки улучшения
        if (data.Coins >= price)
        {
            Cout("Был куплен эффект под номером:" + effect + " По цене:" + price);

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

    // Создает все ячейки-улучшения
    private void CreateItems()
    {
        for (int i = 0; i < Enum.GetNames(typeof(UpgradeEffect)).Length; i++)
        {
            UpgradeEffect nowEffect = (UpgradeEffect)i;

            GameObject itemShop = Instantiate(prefabItemShop, transformItem);

            itemShop.GetComponent<ItemShop>().CreateItem(
                GetInfoEffect(nowEffect, data.Language),
                data.GetInfoEffect(nowEffect),
                GetPriceEffect(nowEffect),
                GetNameIcon(nowEffect),
                data.Language,
                nowEffect,
                this,
                data
                );

            // Задаем размер Item
            itemShop.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 1200);

            itemsShop.Add(itemShop.GetComponent<ItemShop>());
        }
    }

    private void UpdateTextCoins()
    {
        textCoins.text = data.Coins.ToString();
    }

    private void SetTextButtonBack(Language language)
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
                Cout("Не найден язык");
                t_Button = "Back Menu";
                break;
        }

        textButton.text = t_Button;
    }

    private List<int> GetPriceEffect(UpgradeEffect effect)
    {
        if (priceEffects.Count <= (int)effect)
        {
            Cout("Номер эффекта вышел за границы");
            return new List<int>() { 100000, 100000, 100000, 100000, 100000, 100000};
        }

        return priceEffects[(int)effect];
    }

    private string GetNameIcon(UpgradeEffect effect)
    {
        if (priceEffects.Count <= (int)effect)
        {
            Cout("Номер эффекта вышел за границы");
            return "Error";
        }

        return iconEffects[(int)effect];
    }

    private List<string> GetInfoEffect(UpgradeEffect effect, Language language)
    {
        switch (language)
        {
            case Language.en:
                return infoEffectsEN[(int)effect];
            case Language.ru:
                return infoEffectsRU[(int)effect];
            default:
                Cout("Не найден язык");
                return infoEffectsEN[(int)effect];
        }
    }

    private void Cout(string mess)
    {
        if (PrintDebug) Debug.Log(mess);
    }

    #region Имя иконки эффекта

    List<string> iconEffects = new List<string>()
    {
        "s_Magnite",
        "s_Shield",
        "s_Moution",
        "s_XScore",
        "s_Speed",
        "s_Health",
        "s_Size",
        "s_Player"
    };

    #endregion

    #region Цены эффектов

    List<List<int>> priceEffects = new List<List<int>>()
    {
        // Magnite
        new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },

        // Shield
        new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },

        // Moution
        new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },

        // XScore 
        new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },

        // Speed
        new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },

        // Health
        new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },

        // Size
         new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },

        // Player
        new List<int>()
        {
            10, 20, 30, 40, 50, 60
        },
    };

    #endregion

    #region Описание покупки эффектов

    #region Русский

    List<List<string>> infoEffectsRU = new List<List<string>>()
    {
        // Magnite
        new List<string>()
        {
            "Получить эффект - он притягивает множитель очков", //1
            "Увеличивает длительность", //2
            "Притягивает уменьшение размера", //3
            "Притягивает увеличение скорости", //4
            "Увеличивает шанс выпадения", //5
            "Притягивает сердечки", //6
        },

        // Shield
        new List<string>()
        {
            "Получить эффект - он добавляет уязвимость к снарядам", //1
            "Увеличивает длительность", //2
            "Увеличивает шанс выпадения", //3
            "Увеличивает длительность", //4
            "Увеличивает шанс выпадения", //5
            "Увеличивает длительность", //6
        },

        // Moution
        new List<string>()
        {
            "Получить эффект - он замедляет время", //1
            "Увеличивает длительность", //2
            "Усиление слоумо", //3
            "Увеличивает шанс выпадения", //4
            "Увеличивает длительность", //5
            "Усиление слоумо", //6
        },

        // XScore
        new List<string>()
        {
            "Получить эффект - больше очков получаешь", //1
            "Увеличивает длительность", //2
            "Увеличивает шанс выпадения", //3
            "х3", //4
            "Увеличивает длительность", //5
            "х4", //6
        },

        // Speed
        new List<string>()
        {
            "Получить эффект - быстрее движешься", //1
            "Увеличивает шанс выпадения", //2
            "Увеличивает прибавляемую скорость", //3
            "Увеличивает шанс выпадения", //4
            "Увеличивает прибавляемую скорость", //5
            "Увеличивает максимальную скорость", //6
        },

        // Health
        new List<string>()
        {
            "Получить эффект - дается жизнь", //1
            "Увеличивает шанс выпадения", //2
            "Увеличивает прибавляемое хп", //3
            "Увеличивает шанс выпадения", //4
            "Увеличивает прибавляемое хп", //5
            "Увеличивает максимальное хп", //6
        },

        // Size
        new List<string>()
        {
            "Получить эффект - уменьшается размер", //1
            "Увеличивает шанс выпадения", //2
            "Увеличивает уменьшение размера", //3
            "Увеличивает шанс выпадения", //4
            "Увеличивает уменьшение размера", //5
            "Уменьшает минимальный размера", //6
        },
                
        // Player
        new List<string>()
        {
            "Увеличивает базовую скорость", //1
            "Уменьшает базовый размер", //2
            "Увеличивает базовое хп", //3
            "Увеличивает базовую скорость", //4
            "Уменьшает базовый размер", //5
            "Увеличивает базовое хп", //6
        },
    };


    #endregion

    #region Английский

    List<List<string>> infoEffectsEN = new List<List<string>>()
    {
        // Magnite
        new List<string>()
        {
            "Get the effect - it attracts a score multiplier", //1
            "Increases duration", //2
            "Attracts size reduction", //3
            "Attracts an increase in speed", //4
            "Increases drop chance", //5
            "Attracts hearts", //6
        },

        // Shield
        new List<string>()
        {
            "Get effect - it adds vulnerability to projectiles", //1
            "Increases duration", //2
            "Increases drop chance", //3
            "Increases duration", //4
            "Increases drop chance", //5
            "Increases duration", //6
        },

        // Moution
        new List<string>()
        {
            "Get the effect - it slows down time", //1
            "Increases duration", //2
            "Slowmo gain", //3
            "Increases drop chance", //4
            "Increases duration", //5
            "Slowmo gain", //6
        },

        // XScore
        new List<string>()
        {
            "Get the effect - you get more points", //1
            "Increases duration", //2
            "Increases drop chance", //3
            "х3", //4
            "Increases duration", //5
            "х4", //6
        },

        // Speed
        new List<string>()
        {
            "Get the effect - move faster", //1
            "Increases drop chance", //2
            "Increases added speed", //3
            "Increases drop chance", //4
            "Increases added speed", //5
            "Increases top speed", //6
        },

        // Health
        new List<string>()
        {
            "Get the effect - life is given", //1
            "Increases drop chance", //2
            "Increases added HP", //3
            "Increases drop chance", //4
            "Increases added HP", //5
            "Increases top HP", //6
        },

        // Size
        new List<string>()
        {
            "Get the effect - size decreases", //1
            "Increases drop chance", //2
            "Increases size reduction", //3
            "Increases drop chance", //4
            "Increases size reduction", //5
            "Decreases the minimum size", //6
        },
                
        // Player
        new List<string>()
        {
            "Increases base speed", //1
            "Decreases base size", //2
            "Increases base HP", //3
            "Increases base speed", //4
            "Decreases base size", //5
            "Increases base HP", //6
        },
    };

    #endregion

    #endregion

}


