using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BinarySerializer
{
    private const string path = "/GameData5.dat";

    private Hashtable data;

    public void SaveData()
    {
        if (data == null)
        {
            Debug.Log("Data is null, load data before saving");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + path);

        try
        {
            bf.Serialize(file, data);
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + path, FileMode.Open);
            try
            {
                data = (Hashtable)bf.Deserialize(file);
            }
            catch (SerializationException e)
            {
                Debug.Log("Failed to deserialize. Reason: " + e.Message);
            }
            finally
            {
                file.Close();
            }
        }
        else
        {
            Debug.Log("Not found file. Create New.");
            SetDefaultData();
        }

        //Для теста
        foreach (DictionaryEntry de in data)
        {
            Debug.Log("Key: " + de.Key + " Value: " + de.Value);
        }
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + path))
        {
            try
            {
                File.Delete(Application.persistentDataPath + path);
            }
            catch 
            {
                Debug.Log("C удалением файла что то пошло не так");
            }
            SetDefaultData();
<<<<<<< HEAD
            SaveData();
=======
>>>>>>> parent of c29e3f8 (Добавлена кнопка назад в магазине и количество монет)
        }
    }


    public ref Hashtable GetData()
    {
        return ref data;
    }

    private void SetDefaultData()
    {
        data = new Hashtable() 
        {
            {"Coins", "0"},
            {"Record", "0"}
        };

        // Добавление списка предметов
        List<bool> allItem = new List<bool>();

        for (int i = 0; i < Enum.GetNames(typeof(Item)).Length; i++)
        {
            allItem.Add(false);
        }

        data["Items"] = GameDataManager.GetStringAllItems(allItem);


        // Добавление списка эффектов
        List<int> allEffect = new List<int>();

        for (int i = 0; i < Enum.GetNames(typeof(UpgradeEffect)).Length; i++)
        {
            allEffect.Add(0);
        }

        data["Effects"] = GameDataManager.GetStringAllEffects(allEffect);
    }
}
