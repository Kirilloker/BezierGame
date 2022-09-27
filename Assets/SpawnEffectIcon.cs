using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectIcon : MonoBehaviour
{
    // Префаб одной визуальной ячейки
    [SerializeField]
    GameObject prefabItemEffect;

    List<Sprite> sprites = new List<Sprite>();

    private void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(UpgradeEffect)).Length; i++)
        {
            sprites.Add(Resources.Load<Sprite>(iconEffects[i]));
        }
    }

    public List<IconEffectInGame> IconEffectInGameScript = new List<IconEffectInGame>();
    public List<UpgradeEffect> upgradeEffects = new List<UpgradeEffect>();

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

        StartCoroutine(effectScript.CreateEffect(time, sprites[(int)effect], effect, this));
    }

    public void DeleteEffect(UpgradeEffect effect)
    {
        IconEffectInGameScript.RemoveAt(upgradeEffects.IndexOf(effect));
        upgradeEffects.Remove(effect);
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

    List<string> iconEffects = new List<string>()
    {
        "s_Magnite",
        "s_Shield",
        "s_Moution",
        "s_XScore",
        "s_Speed",
        "s_Health",
        "s_Size",
        "s_Player"
    };
}
