using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // ���� True - � ������� ����� ���������� ���� �� ������ ����������
    private bool PrintDebug = true;

    private Hashtable gameData;

    private BinarySerializer serializer = new BinarySerializer();

    private void ChangeHashTable(string key, string value) 
    {
        if (gameData == null) Debug.Log("gameData �� ����������");

        try
        {
            if (gameData.ContainsKey(key) == true)
            {
                if (PrintDebug) Debug.Log("�������� ����: " + key + "  �� ���������: " + value);
                gameData[key] = value;
            }
            else
            {
                if (PrintDebug) Debug.Log("�������� ����: " + key + "  �� ���������: " + value);
                gameData.Add(key, value);
            }

            serializer.SaveData();
        }
        catch 
        {
            Debug.Log("�� ������� ��������� ����: " + key + "  �� ���������: " + value);
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
                if (PrintDebug) Debug.Log("�� ������� ������� �������� �� �����:" + key);
            }
        }
        catch
        {
            if (PrintDebug) Debug.Log("������ �� ������� ������� �������� �� �����:" + key);
            
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
            if (PrintDebug) Debug.Log("���� ���������� ����� ������ (����� ��������� � ������)");
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


    // ����� ���� ����� ����������� ==============================================================================
    

    public int Record
    {
        get { return int.Parse(TryGetValueInHashTable("Record")); }
        set 
        { 
            if (value < 0)
            {
                if (PrintDebug) Debug.Log("Record �� ����� ���� �������������");

                ChangeHashTable("Record", "0");
            }
            else
            {
                if (Record >= value)
                {
                    if (PrintDebug) Debug.Log("����������� ������� ��������� Record");
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
                if (PrintDebug) Debug.Log("Coins �� ����� ���� �������������");
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
