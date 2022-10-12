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

        CoinsUI = (uint)dataManager.Coins;
        ScoreUI = 0;
        HealthUI = 1;
    }

    public void CreateEffect(UpgradeEffect effect, float time)
    {
        // Если такой эффект уже есть на игроке
        // то удаляем иконку предыдущего эффекта 
        if (upgradeEffects.Contains(effect))
        {
            IconEffectInGameScript[upgradeEffects.IndexOf(effect)].Delete();
        }

        GameObject go = Instantiate(prefabItemEffect, this.transform);
        IconEffectInGame effectScript = go.GetComponent<IconEffectInGame>();

        IconEffectInGameScript.Add(effectScript);
        upgradeEffects.Add(effect);

        // Запускаем анимацию длительности эффекта
        StartCoroutine(effectScript.CreateEffect(time, ResoursesData.spritesEffect[(int)effect], effect, this));
    }

    public void DeleteEffect(UpgradeEffect effect)
    {
        IconEffectInGameScript.RemoveAt(upgradeEffects.IndexOf(effect));
        upgradeEffects.Remove(effect);
    }

    public void OnScoreChangeValue(uint score)
    {
        ScoreUI = score;
    }


    uint healthUI;
    public uint HealthUI
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

    uint scoreUI;
    public uint ScoreUI
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

    uint coinsUI;
    public uint CoinsUI
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
