using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // ���� True - � ������� ����� ���������� ���� �� ������ ����������
    private bool PrintDebug = true;

    private Hashtable gameData;

    private void ChangeHashTable(string key, string value) 
    {
        if (gameData == null) Debug.Log("SaveData �� ����������");

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
        }
        catch 
        {
            Debug.Log("�� ������� ��������� ����: " + key + "  �� ���������: " + value);
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
            if (PrintDebug) Debug.Log("���� ���������� ����� ������ (����� ��������� � ������)");
        }
    }

    private void OnApplicationQuit()
    {
        // ��� ���� ������� ���������� ��� ����
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    // ����� ���� ����� ����������� ==============================================================================
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
