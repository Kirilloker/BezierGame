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


    // Какие поля будут сохраняться ==============================================================================
    

    public int Record
    {
        get { return int.Parse(TryGetValueInHashTable("Record")); }
        set 
        { 
            if (value < 0)
            {
                if (PrintDebug) Debug.Log("Record не может быть отрицательным");

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

    // ===========================================================================================================
}
