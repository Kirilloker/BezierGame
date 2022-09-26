using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    GameDataManager data;
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();
    }

    public void GiveMoney()
    {
        data.Coins += 100;
    }

    public void ResetData()
    {
        Debug.Log("”дал€ютс€ данные");
        data.ResetData();
    }

    public void ChanhgeLanguage()
    {
        int lang = (int)data.Language;

        lang++;

        int sizeLang = Enum.GetNames(typeof(Language)).Length;

        if (lang >= sizeLang) lang = 0;

        data.Language = (Language) lang;
    }

}
