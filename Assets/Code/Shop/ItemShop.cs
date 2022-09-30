using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    // Справочная информация о эффекте
    [SerializeField]
    private Text infoText;

    // Иконка эффекта
    [SerializeField]
    private Image iconEffect;

    // Кнопка покупки
    [SerializeField]
    private Button buttonBuy;

    // Выбранный элемент
    [SerializeField]
    private List<Image> SelectImages;

    // Куплен эффект
    [SerializeField]
    private List<Image> HaveEffectImages;

    private UpgradeEffect effect;
    private List<string> infoEffect;
    private int levelEffect;
    private List<int> priceEffect;
    private Sprite spriteEffect;
    private Language language;

    private Shop shop;
    private GameDataManager data;

    public void CreateItem(List<string> infoEffect, int levelEffect, List<int> priceEffect, Sprite spriteEffect, Language language, UpgradeEffect effect, Shop shop, GameDataManager data)
    {
        this.infoEffect = infoEffect;
        this.levelEffect = levelEffect;
        this.priceEffect = priceEffect;
        this.spriteEffect = spriteEffect;
        this.language = language;
        this.effect = effect;
        this.shop = shop;
        this.data = data;

        SetIcon();
        UpdateElementsUI();

        SelectItem = levelEffect;
        BuyItem = levelEffect;
    }

    public void UpdateElementsUI()
    {
        SetTextButton();
    }
    private void SetIcon()
    {
        iconEffect.sprite = spriteEffect;
    }

    private void SetTextButton()
    {
        buttonBuy.enabled = true;
        buttonBuy.GetComponentInChildren<Text>().color = Color.black;

        string priceText = "";

        if (SelectItem <= levelEffect - 1)
        {
            // Смотрим уже купленные
            switch (language)
            {
                case Language.en:
                    priceText += "You have!";
                    break;
                case Language.ru:
                    priceText += "Куплено";
                    break;
                default:
                    priceText += "Error";
                    break;
            }

            buttonBuy.enabled = false;
            buttonBuy.GetComponentInChildren<Text>().color = Color.gray;
        }
        else if (SelectItem > levelEffect)
        {
            // Смотрим те которые еще не можем купить

            switch (language)
            {
                case Language.en:
                    priceText += "Not available";
                    break;
                case Language.ru:
                    priceText += "Не доступно";
                    break;
                default:
                    priceText += "Error";
                    break;
            }

            buttonBuy.enabled = false;
            buttonBuy.GetComponentInChildren<Text>().color = Color.gray;

        }
        else if (SelectItem == levelEffect)
        {
            // Смотрим ту, которую можем купить
            // Надо получить количество монет игрока
            int price = priceEffect[levelEffect];

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
        }

        buttonBuy.GetComponentInChildren<Text>().text = priceText;
    }

    private void SendRequest()
    {
        if (shop.BuyEffect(effect, priceEffect[levelEffect]))
        {
            levelEffect++;
            UpdateElementsUI();
            BuyItem++;
            SelectItem++;
        }
    }

    public void PressItem(int numberItem)
    {
        SelectItem = numberItem;
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
            if (value <= 0) value = 0;
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
            if (value <= 0) value = 0;
            if (value >= 6) value = 6;

            for (int i = 0; i < HaveEffectImages.Count; i++)
            {
                HaveEffectImages[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < value; i++)
            {
                HaveEffectImages[i].gameObject.SetActive(true);
            }

            Debug.Log("BuyItem:" + value);

            butItem = value; 
        }
    }
}
