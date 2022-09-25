using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{

    [SerializeField]
    private Text infoText;

    [SerializeField]
    private Image iconEffect;

    [SerializeField]
    private Button buttonBuy;

    [SerializeField]
    private List<Image> SelectImages;

    // ������ ������
    [SerializeField]
    private List<Image> HaveEffectImages;

    private Effect effect;
    private List<string> infoEffect;
    private int levelEffect;
    private List<int> priceEffect;
    private string nameIcon;
    private Language language;

    private Shop shop;
    private GameDataManager data;


    private string colorHaveItem = "00FB00";
    private string colorNoItem = "13FBBB";

    public void CreateItem(List<string> infoEffect, int levelEffect, List<int> priceEffect, string nameIcon, Language language, Effect effect, Shop shop, GameDataManager data)
    {
        this.infoEffect = infoEffect;
        this.levelEffect = levelEffect;
        this.priceEffect = priceEffect;
        this.nameIcon = nameIcon;
        this.language = language;
        this.effect = effect;
        this.shop = shop;
        this.data = data;

        SetIcon();
        UpdateElementsUI();
        SelectItem = levelEffect + 1;
        BuyItem = levelEffect;
    }

    public void UpdateElementsUI()
    {
        //SetText();
        SetTextButton();
    }

    //private void SetText()
    //{
    //    string text = "";

    //    for (int i = 0; i < infoEffect.Count; i++)
    //    {
    //        // ������������ ��, ������� � ��� ����
    //        if (i <= levelEffect)
    //            text += "<color=#" + colorHaveItem + "> ";
    //        else
    //            text += "<color=#" + colorNoItem + "> ";

    //        text += infoEffect[i];

    //        text += "</color>";

    //        text += "\n";
    //    }

    //    infoText.text = text;
    //}

    private void SetIcon()
    {
        var sprite = Resources.Load<Sprite>(nameIcon);
        iconEffect.sprite = sprite;
    }

    private void SetTextButton()
    {
        // ���� ��� ������ ��� �������, �� ������� ������ ������
        //if (levelEffect == infoEffect.Count - 1)
        //{
        //    if (buttonBuy == null) return;

        //    Destroy(buttonBuy.gameObject);
        //    return;
        //}

        buttonBuy.enabled = true;
        buttonBuy.GetComponentInChildren<Text>().color = Color.black;


        string priceText = "";

        Debug.Log("SelectItem: " + selectItem);
        Debug.Log("levelEffect: " + levelEffect);

        if (SelectItem <= levelEffect)
        {
            // ������� ��� ���������
            switch (language)
            {
                case Language.en:
                    priceText += "You have!";
                    break;
                case Language.ru:
                    priceText += "�������";
                    break;
                default:
                    priceText += "Error";
                    break;
            }

            buttonBuy.enabled = false;
            buttonBuy.GetComponentInChildren<Text>().color = Color.gray;
        }
        else if (SelectItem > levelEffect + 1)
        {
            // ������� �� ������� ��� �� ����� ������

            switch (language)
            {
                case Language.en:
                    priceText += "Not available";
                    break;
                case Language.ru:
                    priceText += "�� ��������";
                    break;
                default:
                    priceText += "Error";
                    break;
            }

            buttonBuy.enabled = false;
            buttonBuy.GetComponentInChildren<Text>().color = Color.gray;

        }
        else if (SelectItem == levelEffect + 1)
        {
            // ������� ��, ������� ����� ������
            // ���� �������� ���������� ����� ������
            int price = priceEffect[levelEffect + 1];

            int pricePlayer = data.Coins;

            // ���� � ������ ��� ����� �� ��������� ������
            if (price > pricePlayer)
            {
                buttonBuy.enabled = false;
                buttonBuy.GetComponentInChildren<Text>().color = Color.gray;
            }

            switch (language)
            {
                case Language.en:
                    priceText += "Price: ";
                    break;
                case Language.ru:
                    priceText += "������: ";
                    break;
                default:
                    Debug.Log("�� ��������� ����");
                    break;
            }

            priceText += price;
        }

        buttonBuy.GetComponentInChildren<Text>().text = priceText;
    }

    public void SendRequest()
    {
        if (shop.BuyEffect(effect, priceEffect[levelEffect + 1]))
        {
            levelEffect++;
            UpdateElementsUI();
            BuyItem++;
            SelectItem++;
        }
    }

    public void PressButBack()
    {
        SelectItem--;
    }

    public void PressButNext()
    {
        SelectItem++;
    }

    int selectItem;
    private int SelectItem
    {
        get { return selectItem; }
        set
        {
            if (value < 0) value = 0;
            if (value >= 5) value = 5;

            for (int i = 0; i < SelectImages.Count; i++)
            {
                SelectImages[i].gameObject.SetActive(false);
            }

            SelectImages[value].gameObject.SetActive(true);

            infoText.text = infoEffect[value];

            selectItem = value;

            SetTextButton();
        }
    }

    int butItem;

    private int BuyItem
    {
        get { return butItem; }
        set
        {
            if (value < 0) value = 0;
            if (value >= 5) value = 5;

            for (int i = 0; i < HaveEffectImages.Count; i++)
            {
                HaveEffectImages[i].gameObject.SetActive(false);
            }

            for (int i = 0; i <= value; i++)
            {
                HaveEffectImages[i].gameObject.SetActive(true);
            }

            butItem = value; 
        }
    }
}
