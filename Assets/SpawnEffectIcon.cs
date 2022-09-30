using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectIcon : MonoBehaviour
{
    // Префаб одной визуальной ячейки
    [SerializeField]
    GameObject prefabItemEffect;

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

        StartCoroutine(effectScript.CreateEffect(time, ResoursesData.spritesEffect[(int)effect], effect, this));
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
}
