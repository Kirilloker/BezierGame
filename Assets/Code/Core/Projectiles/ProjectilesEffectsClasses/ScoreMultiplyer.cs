using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplyer : MonoBehaviour, IProjectile
{

    //Таймер в секундах
    private float multiplyerTimer = 8;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.ScoreMultiplyer;
    }

    public float GetEffectValue()
    {
        return multiplyerTimer;
    }

    public void SetEffectValue(float value)
    {
        multiplyerTimer = value;
    }
}
