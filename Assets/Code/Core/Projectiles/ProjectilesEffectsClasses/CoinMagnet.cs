using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour, IProjectile
{
    //Таймер в секундах
    private float magnetTimer = 8;

    static private List<ProjectileEffect> whoMagnet;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.CoinMagnet;
    }

    public float GetEffectValue()
    {
        return magnetTimer;
    }

    public void SetEffectValue(float value)
    {
        magnetTimer = value;
    }
}
