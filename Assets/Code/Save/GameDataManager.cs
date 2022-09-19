using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // ���� True - � ������� ����� ���������� ���� �� ������ ����������
    private bool PrintDebug = true;

    private Hashtable saveData;

    private void ChangeHashTable(string key, string value) 
    {
        if (saveData == null && PrintDebug) Debug.Log("SaveData �� ����������");

        try
        {
            if (saveData.ContainsKey(key) == true)
            {
                if (PrintDebug) Debug.Log("�������� ����: " + key + "  �� ���������: " + value);
                saveData[key] = value;
            }
            else
            {
                if (PrintDebug) Debug.Log("�������� ����: " + key + "  �� ���������: " + value);
                saveData.Add(key, value);
            }
        }
        catch 
        {
            if (PrintDebug) Debug.Log("�� ������� ��������� ����: " + key + "  �� ���������: " + value);
        }
    }

    public Hashtable SaveData
    {
        get 
        {
            return saveData; 
        }
        set 
        { 
            saveData = value;
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
        get { return (int)saveData["Record"]; }
        set { ChangeHashTable("Record", value.ToString()); }
    }

    public int Coins
    {
        get { return (int)saveData["Coins"]; }
        set { ChangeHashTable("Coins", value.ToString()); }
    }

    // ===========================================================================================================
}
