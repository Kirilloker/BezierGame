using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Префаб одной визуальной ячейки
    [SerializeField]
    GameObject prefabItemEffect;

    [SerializeField]
    Text HealthText;

    [SerializeField]
    Text ScoreText;

    [SerializeField]
    Text CoinsText;

    GameDataManager dataManager;

    public List<IconEffectInGame> IconEffectInGameScript = new List<IconEffectInGame>();
    public List<UpgradeEffect> upgradeEffects = new List<UpgradeEffect>();

    private void Start()
    {
        dataManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();
        //UIGame = GameObject.FindGameObjectWithTag("GameUI").GetComponent<SpawnEffectIcon>();

        CoinsUI = dataManager.Coins;
        ScoreUI = 0;
        HealthUI = 1;
    }

    public void CreateEffect(UpgradeEffect effect, float time)
    {
        if (upgradeEffects.Contains(effect))
        {
            IconEffectInGameScript[upgradeEffects.IndexOf(effect)].Delete();
        }

        GameObject go = Instantiate(prefabItemEffect, this.transform);
        IconEffectInGame effectScript = go.GetComponent<IconEffectInGame>();

        IconEffectInGameScript.Add(effectScript);
        upgradeEffects.Add(effect);

        StartCoroutine(effectScript.CreateEffect(time, ResoursesData.spritesEffect[(int)effect], effect, this));
    }

    public void DeleteEffect(UpgradeEffect effect)
    {
        IconEffectInGameScript.RemoveAt(upgradeEffects.IndexOf(effect));
        upgradeEffects.Remove(effect);
    }



    int healthUI;
    public int HealthUI
    {
        get 
        {
            return healthUI;
        }
        set
        {
            healthUI = value; 
            HealthText.text = healthUI.ToString();
        }
    }

    int scoreUI;
    public int ScoreUI
    {
        get
        {
            return scoreUI;
        }
        set
        {
            scoreUI = value;

            string newText = "";

            switch (dataManager.Language)
            {
                case Language.en:
                    newText = "Score: ";
                    break;
                case Language.ru:
                    newText = "Очки: ";
                    break;
                default:
                    newText = "Score: ";
                    break;
            }

            newText += scoreUI.ToString();

            ScoreText.text = newText;
        }
    }

    int coinsUI;
    public int CoinsUI
    {
        get
        {
            return coinsUI;
        }
        set
        {
            coinsUI = value;
            CoinsText.text = coinsUI.ToString();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("XScore creat");
            CreateEffect(UpgradeEffect.XScore, 2f);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Shield creat");
            CreateEffect(UpgradeEffect.Shield, 5f);
        }
    }
}
