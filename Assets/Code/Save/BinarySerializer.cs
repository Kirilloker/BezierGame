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
    private const string path = "/GameData.dat";

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
            SetDefaultData();
        }

        //Для теста
        foreach (DictionaryEntry de in data)
        {
            Debug.Log("Key: " + de.Key + " Value: " + de.Value);
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
    }
}
