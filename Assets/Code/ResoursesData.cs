using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoursesData : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        LoadSpritesEffect();
    }

    private void LoadSpritesEffect()
    {
        for (int i = 0; i < Enum.GetNames(typeof(UpgradeEffect)).Length; i++)
        {
            spritesEffect.Add(Resources.Load<Sprite>(nameSpriteEffects[i]));
        }
    }


    #region Спрайты эффектов

    // Игровые спрайты 
    static public List<Sprite> spritesEffect = new List<Sprite>();

    // Имена спрайтов
    List<string> nameSpriteEffects = new List<string>()
        {
            "s_Magnite",
            "s_Shield",
            "s_Moution",
            "s_XScore",
            "s_SpeedUp",
            "s_Health",
            "s_Size",
            "s_Player"
        };
    #endregion

    #region Цены эффектов

    static public List<List<int>> pricesEffect = new List<List<int>>()
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

    static public List<List<string>> infoEffectsRU = new List<List<string>>()
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

    static public List<List<string>> infoEffectsEN = new List<List<string>>()
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
