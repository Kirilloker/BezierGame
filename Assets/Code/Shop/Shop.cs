using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // ���� true - � ������� ����� ���������� ����
    bool PrintDebug = true;

    // ������ ����
    GameDataManager data;

    // ������ ������-��������� � ��������
    [SerializeField]
    GameObject prefabItemShop;

    // ���� ����� ����������� ������-���������
    [SerializeField]
    Transform transformItem;
    
    // ����� �������
    [SerializeField]
    Text textCoins;

    // ������� �� ������, ������� ������ � ������� ����
    [SerializeField]
    Text textButton;

    // ������-��������� 
    List<ItemShop> itemsShop = new List<ItemShop>();
 
    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();

        UpdateTextCoins();
        SetTextButtonBack(data.Language);
        CreateItems();

        for (int i = 0; i < 300; i++)
        {
            Debug.Log("SECOND: " + i + " PositiveTimer:" + FuncPositiveTimer(i));
        }


        for (int i = 0; i < 300; i++)
        {
            Debug.Log("SECOND: " + i + " NegativeTimer:" + FuncNegativeTimer(i));
        }
    }
    public float FuncPositiveTimer(float x)
    {
        float normalizeX = x / 15f;

        if (normalizeX < 4)
        {
            return 20;
        }
        else if (normalizeX > 25)
        {
            return 4;
        }

        return (9.6f) / (0.13f * normalizeX) + 0.8f;
    }

    public float FuncNegativeTimer(float x)
    {
        float normalizeX = x / 15f;

        if (normalizeX < 4)
        {
            return 25;
        }
        else if (normalizeX > 25)
        {
            return 1;
        }

        return (4f) / (0.047f * normalizeX) -2.2f;
    }


    // ����������� ������
    public bool BuyEffect(UpgradeEffect effect, int price)
    {
        // ���� ��� ������� ����� ��� ������� ���������
        if (data.Coins >= price)
        {
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

    // ������� ��� ������-���������
    private void CreateItems()
    {
        for (int i = 0; i < Enum.GetNames(typeof(UpgradeEffect)).Length; i++)
        {
            UpgradeEffect nowEffect = (UpgradeEffect)i;

            GameObject itemShop = Instantiate(prefabItemShop, transformItem);

            itemShop.GetComponent<ItemShop>().CreateItem(
                GetInfoEffect(nowEffect, data.Language),
                data.GetInfoEffect(nowEffect),
                GetPriceEffect(nowEffect),
                GetSpriteEffect(nowEffect),
                data.Language,
                nowEffect,
                this,
                data
                );

            // ������ ������ Item
            itemShop.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 1200);

            itemsShop.Add(itemShop.GetComponent<ItemShop>());
        }
    }

    private void UpdateTextCoins()
    {
        textCoins.text = data.Coins.ToString();
    }

    private void SetTextButtonBack(Language language)
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
                Cout("�� ������ ����");
                t_Button = "Back Menu";
                break;
        }

        textButton.text = t_Button;
    }

    private List<int> GetPriceEffect(UpgradeEffect effect)
    {
        if (ResoursesData.pricesEffect.Count <= (int)effect)
        {
            Cout("����� ������� ����� �� �������");
            return new List<int>() { 100000, 100000, 100000, 100000, 100000, 100000};
        }

        return ResoursesData.pricesEffect[(int)effect];
    }

    private Sprite GetSpriteEffect(UpgradeEffect effect)
    {
        if (ResoursesData.pricesEffect.Count <= (int)effect)
        {
            Cout("����� ������� ����� �� �������");
            return ResoursesData.spritesEffect[0];
        }

        return ResoursesData.spritesEffect[(int)effect];
    }

    private List<string> GetInfoEffect(UpgradeEffect effect, Language language)
    {
        switch (language)
        {
            case Language.en:
                return ResoursesData.infoEffectsEN[(int)effect];
            case Language.ru:
                return ResoursesData.infoEffectsRU[(int)effect];
            default:
                Cout("�� ������ ����");
                return ResoursesData.infoEffectsEN[(int)effect];
        }
    }

    private void Cout(string mess)
    {
        if (PrintDebug) Debug.Log(mess);
    }

}


