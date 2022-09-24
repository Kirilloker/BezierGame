using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Language language = Language.ru;
    private bool PrintDebug = true;
    GameDataManager data;

    [SerializeField]
    GameObject prefabItemShop;

    [SerializeField]
    Transform transformItem;

    [SerializeField]
    Text textCoins;

    [SerializeField]
    Text textButton;

    private List<ItemShop> itemsShop = new List<ItemShop>();

    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();

        UpdateTextCoins();
        SetTextButtonBack();

        CreateItems();
    }

    public void CreateItems()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Effect)).Length; i++)
        {
            Effect nowEffect = (Effect)i;

            GameObject itemShop = Instantiate(prefabItemShop, transformItem);
            itemShop.GetComponent<ItemShop>().CreateItem(
                GetInfoEffect(nowEffect, language),
                data.GetInfoEffect(nowEffect),
                GetPriceEffect(nowEffect),
                GetNameIcon(nowEffect),
                language,
                nowEffect,
                this,
                data
                );

            // ������ ������ Item
            itemShop.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 1200);

            itemsShop.Add(itemShop.GetComponent<ItemShop>());
        }
    }

    public bool BuyEffect(Effect effect, int price)
    {
        if (data.Coins >= price)
        {
            Debug.Log("��� ������ ������ ��� �������:" + effect + " �� ����:" + price);

            data.Coins -= price;
            data.IncEffect(effect);

            // ������� ���� ��������� �������, ��� �� ���� ����������
            for (int i = 0; i < itemsShop.Count; i++)
            {
                itemsShop[i].UpdateElementsUI();
            }

            UpdateTextCoins();

            return true;
        }

        return false;
    }

    private void UpdateTextCoins()
    {
        textCoins.text = data.Coins.ToString();
    }

    private void SetTextButtonBack()
    {
        string t_Button;

        switch (language)
        {
            case Language.en:
                t_Button = "Back Menu";
                break;
            case Language.ru:
                t_Button = "����� � ����";
                break;
            default:
                t_Button = "Back Menu";
                break;
        }

        textButton.text = t_Button;
    }

    private List<int> GetPriceEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.speed:
                return priceSpeed;
            case Effect.size:
                return priceSpeed;
            case Effect.shield:
                return priceSpeed;
        }

        return new List<int>() { 1000000, 1000000, 1000000, 1000000, 1000000, 1000000 };
    }

    private string GetNameIcon(Effect effect)
    {
        switch (effect)
        {
            case Effect.speed:
                return iconSpeed;
            case Effect.size:
                return iconSpeed;
            case Effect.shield:
                return iconSpeed;
        }

        return "Error";
    }

    private List<string> GetInfoEffect(Effect effect, Language language)
    {
        switch (effect)
        {
            case Effect.speed:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("������ ����� ���");
                        break;
                }
                break;
            case Effect.size:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("������ ����� ���");
                        break;
                }
                break;
            case Effect.shield:
                switch (language)
                {
                    case Language.en:
                        return infoSpeedEN;
                    case Language.ru:
                        return infoSpeedRU;
                    default:
                        Cout("������ ����� ���");
                        break;
                }
                break;
            default:
                Cout("������ ������� ���!");
                break;
        }

        return new List<string>() { "Error" };
    }

    private void Cout(string mess)
    {
        if (PrintDebug) Debug.Log(mess);
    }

    #region ��� ������ �������

    string iconSpeed = "coin";

    #endregion


    #region ���� ��������

    List<int> priceSpeed = new List<int>()
    {
        10, //1
        20, //2
        30, //3
        40, //4
        50, //5
        60  //6
    };

    #endregion

    #region �������� ������� ��������

    #region �������

    List<string> infoSpeedRU = new List<string>()
    {
        "�������� ������", //1
        "����������� ���� ���������", //2
        "����������� ������������ ��������", //3
        "����������� ���� ���������", //4
        "����������� ������������ ��������", //5
        "����������� ������������ ��������", //6
    };

    #endregion

    #region ����������

    List<string> infoSpeedEN = new List<string>()
    {
        "Get effect", //1
        "Increases spawn chance", //2
        "Increases added speed", //3
        "Increases spawn chance", //4
        "Increases added speed", //5
        "Increases the maximum speed", //6
    };

    #endregion

    #endregion

}
public enum Language
{
    en,
    ru
}