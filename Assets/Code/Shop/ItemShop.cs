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
    }

    public void UpdateElementsUI()
    {
        SetText();
        SetTextButton();
    }

    private void SetText()
    {
        string text = "";

        for (int i = 0; i < infoEffect.Count; i++)
        {
            // Подкрашиваем те, которые у нас есть
            if (i <= levelEffect)
                text += "<color=#" + colorHaveItem + "> ";
            else
                text += "<color=#" + colorNoItem + "> ";

            text += infoEffect[i];

            text += "</color>";

            text += "\n";
        }

        infoText.text = text;
    }

    private void SetIcon()
    {
        var sprite = Resources.Load<Sprite>(nameIcon);
        iconEffect.sprite = sprite;
    }

    private void SetTextButton()
    {
        // Если уже купили все эффекты, то удаляем кнопку Купить
        if (levelEffect == infoEffect.Count - 1)
        {
            if (buttonBuy.gameObject == null) return;

            Destroy(buttonBuy.gameObject);
            return;
        }

        string priceText = "";
        int price = priceEffect[levelEffect + 1];

        // Надо получить количество монет игрока
        int pricePlayer = data.Coins;

        // Если у Игрока нет денег то выключаем кнопку
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
                priceText += "Купить: ";
                break;
            default:
                Debug.Log("Не известный язык");
                break;
        }

        priceText += price;

        buttonBuy.GetComponentInChildren<Text>().text = priceText;
    }

    public void SendRequest()
    {
        if (shop.BuyEffect(effect, priceEffect[levelEffect + 1]))
        {
            levelEffect++;
            UpdateElementsUI();
        }
    }
}
