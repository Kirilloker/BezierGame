using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IProjectile
{

    //Таймер в секундах
    private float shieldTimer = 8;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.Shield;
    }

    public float GetEffectValue()
    {
        return shieldTimer;
    }


    public void SetEffectValue(float value)
    {
        shieldTimer = value;
    }
}
