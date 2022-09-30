using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Если True - в консоль будут выводиться логи по поводу сохранения
    private bool PrintDebug = true;

    private Hashtable gameData;

    private BinarySerializer serializer = new BinarySerializer();

    private void ChangeHashTable(string key, string value) 
    {
        if (gameData == null) Debug.Log("gameData не существует");

        try
        {
            if (gameData.ContainsKey(key) == true)
            {
                if (PrintDebug) Debug.Log("ОБНОВЛЕН ключ: " + key + "  Со значением: " + value);
                gameData[key] = value;
            }
            else
            {
                if (PrintDebug) Debug.Log("ДОБАВЛЕН ключ: " + key + "  Со значением: " + value);
                gameData.Add(key, value);
            }

            serializer.SaveData();
        }
        catch 
        {
            Debug.Log("НЕ УДАЛОСЬ сохранить ключ: " + key + "  Со значением: " + value);
        }
    }

    private string TryGetValueInHashTable(string key)
    {
        try
        {
            if (GameData.ContainsKey(key) == true)
            {
                return (string)gameData[key];
            }
            else
            {
                if (PrintDebug) Debug.Log("Не удалось достать значение по ключу:" + key);
            }
        }
        catch
        {
            if (PrintDebug) Debug.Log("ОШИБКА не удалось достать значение по ключу:" + key);
            
        }

        return "Error";
    }

    public Hashtable GameData
    {
        get 
        {
            return gameData; 
        }
        set 
        {
            gameData = value;
            if (PrintDebug) Debug.Log("Были установлны новые данные (нужно сохранить в память)");
        }
    }

    private void OnApplicationQuit()
    {
        serializer.SaveData();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        serializer.LoadData();
        GameData = serializer.GetData();
    }

    public void ResetData()
    {
        serializer.ResetData();
        GameData = serializer.GetData();
    }

    //Обработчики ивентов__________________________________
    public void OnPlayerPickUpCoin(int numOfCoins)
    {
        Coins += numOfCoins;
    }

    // Какие поля будут сохраняться ==============================================================================


    public int Record
    {
        get { return int.Parse(TryGetValueInHashTable("Record")); }
        set 
        { 
            if (value < 0)
            {
                if (PrintDebug) Debug.Log("Record);

                ChangeHashTable("Record", "0");
            }
            else
            {
                if (Record >= value)
                {
                    if (PrintDebug) Debug.Log("Перехвачена попытка уменьшить Record");
                }
                else
                {
                    ChangeHashTable("Record", value.ToString());
                }
                
            }
        }
    }

    public int Coins
    {
        get { return int.Parse(TryGetValueInHashTable("Coins")); }
        set
        {
            if (value < 0)
            {
                if (PrintDebug) Debug.Log("Coins не может быть отрицательным");
                ChangeHashTable("Coins", "0");
            }
            else
            {
                ChangeHashTable("Coins", value.ToString());
            }

        }
    }

    public Language Language
    {
        get { return (Language)int.Parse(TryGetValueInHashTable("Language")); }

        set { ChangeHashTable("Language", ((int)(value)).ToString()); }
    }

    // ===========================================================================================================

    private List<bool> GetAllInfoItems()
    {
        // Строчку типа: "1, 0, 0, 1, 1," превращает в 
        // List <bool> = {true, false, false, true, true}
        List<bool> allItem = new List<bool>();

        string s = TryGetValueInHashTable("Items");
        char[] separators = new char[] { ' ', ',' };

        string[] subs = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        foreach (var sub in subs)
        {
            allItem.Add(sub == "1");
        }

        return allItem;
    }

    public static string GetStringAllItems(List<bool> allItem)
    {
        string newString = "";

        for (int i = 0; i < allItem.Count; i++)
        {
            if (allItem[i] == true) newString += "1";
            else newString += "0";

            if (i != allItem.Count - 1) newString += ", ";
        }

        return newString;
    }

    public bool GetInfoItem(Item item)
    {
        List<bool> allItem = GetAllInfoItems();

        if (allItem.Count < (int)item)
        {
            if (PrintDebug) Debug.Log("В списке нет предмета под номером:" + (int)item);
            return false;
        }

        return allItem[(int)item];
    }

    public void SetInfoItem(Item item, bool stateItem)
    {
        List<bool> allItem = GetAllInfoItems();

        if (allItem.Count < (int)item)
        {
            if (PrintDebug) Debug.Log("В списке нет предмета под номером:" + (int)item);
            return;
        }

        allItem[(int)item] = stateItem;

        string newString = GetStringAllItems(allItem);

        ChangeHashTable("Items", newString);
    }


    

    public List<int> GetAllInfoEffects()
    {
        List<int> allEffect = new List<int>();

        string s = TryGetValueInHashTable("Effects");
        char[] separators = new char[] { ' ', ',' };

        string[] subs = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        foreach (var sub in subs)
        {
            allEffect.Add(int.Parse(sub));
        }

        return allEffect;
    }

    public static string GetStringAllEffects(List<int> allItem)
    {
        string newString = "";

        for (int i = 0; i < allItem.Count; i++)
        {
            newString += allItem[i].ToString();

            if (i != allItem.Count - 1) newString += ", ";
        }

        return newString;
    }

    public int GetInfoEffect(UpgradeEffect effect)
    {
        List<int> allItem = GetAllInfoEffects();

        if (allItem.Count < (int)effect)
        {
            if (PrintDebug) Debug.Log("В списке нет эффекта под номером:" + (int)effect);
            return 0;
        }

        return allItem[(int)effect];
    }

    private void SetInfoEffect(UpgradeEffect effect, int stateItem)
    {
        List<int> allItem = GetAllInfoEffects();

        if (allItem.Count < (int)effect)
        {
            if (PrintDebug) Debug.Log("В списке нет эффекта под номером:" + (int)effect);
            return;
        }

        allItem[(int)effect] = stateItem;

        string newString = GetStringAllEffects(allItem);

        ChangeHashTable("Effects", newString);
    }

    public void IncEffect(UpgradeEffect effect)
    {
        if (GetInfoEffect(effect) < 6)
        {
            SetInfoEffect(effect, GetInfoEffect(effect) + 1);
        }
        else
        {
            if (PrintDebug) Debug.Log("Нельзя устанавливать эффект больше 6!");
        }
    }
}

public enum UpgradeEffect
{
    Magnite,
    Shield,
    Moution,
    XScore,
    Speed,
    Health,
    Size, 
    Player
}

public enum Item
{
    s_Hallowen,
    s_Cat,
    s_Pluto,
}
public enum Language
{
    en,
    ru
}

