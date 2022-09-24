using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePath : MonoBehaviour, IProjectile
{

    //Таймер в секундах
    private float hideTimer = 8;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.HidePath;
    }

    public float GetEffectValue()
    {
        return hideTimer;
    }

    public void SetEffectValue(float value)
    {
        hideTimer = value;
    }
}
