using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // Если True - в консоль будут выводиться логи по поводу сохранения
    private bool PrintDebug = true;

    private Hashtable gameData;

    private void ChangeHashTable(string key, string value) 
    {
        if (gameData == null) Debug.Log("SaveData не существует");

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
        }
        catch 
        {
            Debug.Log("НЕ УДАЛОСЬ сохранить ключ: " + key + "  Со значением: " + value);
        }
    }

    public Hashtable SaveData
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
        // Тут надо вызвать сохранение пук пука
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    // Какие поля будут сохраняться ==============================================================================
    public int Record
    {
        get { return (int)gameData["Record"]; }
        set { ChangeHashTable("Record", value.ToString()); }
    }

    public int Coins
    {
        get { return (int)gameData["Coins"]; }
        set { ChangeHashTable("Coins", value.ToString()); }
    }

    // ===========================================================================================================
}
