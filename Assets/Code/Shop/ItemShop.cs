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
    private List<Image> ActiveImages;

    [SerializeField]
    private List<Image> SelectImages;

    private UpgradeEffect effect;
    private List<string> infoEffect;
    private int levelEffect;
    private List<int> priceEffect;
    private string nameIcon;
    private Language language;


    private string colorHaveItem = "00FB00";
    private string colorNoItem = "13FBBB";

    public void CreateItem(List<string> infoEffect, int levelEffect, List<int> priceEffect, string nameIcon, Language language, UpgradeEffect effect)
    {
        this.infoEffect = infoEffect;
        this.levelEffect = levelEffect;
        this.priceEffect = priceEffect;
        this.nameIcon = nameIcon;
        this.language = language;
        this.effect = effect;

        SetIcon();
        UpdateElementsUI();
        SelectItem = levelEffect + 1;
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
        var sprite = Resources.Load<Sprite>("coin");
        iconEffect.sprite = sprite;
    }

    private void SetTextButton()
    {
        Debug.Log("Level"  + levelEffect);
        Debug.Log("infoEffect" + infoEffect.Count);
        // ���� ��� ������ ��� �������, �� ������� ������ ������
        if (levelEffect == infoEffect.Count - 1)
        {
            if (buttonBuy.gameObject == null) return;

            Destroy(buttonBuy.gameObject);
            Debug.Log("Test"); 
            return;
        }

        string priceText = "";
        int price = priceEffect[levelEffect + 1];

        // ���� �������� ���������� ����� ������
        int pricePlayer = 40;

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

        buttonBuy.GetComponentInChildren<Text>().text = priceText;
    }

    public void SendRequest()
    {
        Debug.Log("�� ������ ������ ������");

        levelEffect++;
        UpdateElementsUI();
    }

    private int SelectItem
    {
        set
        {
            if (value < 0) value = 0;
            if (value > 6) value = 6;

            for (int i = 0; i < SelectImages.Count; i++)
            {
                SelectImages[i].gameObject.SetActive(false);
            }

            SelectImages[value].gameObject.SetActive(true);

            infoText.text = infoEffect[value];
        }
    }
}
