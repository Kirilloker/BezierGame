using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowmoution : MonoBehaviour, IProjectile
{

    //Таймер в секундах
    private float slowmoutionTimer = 8;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.Slowmoution;
    }

    public float GetEffectValue()
    {
        return slowmoutionTimer;
    }


    public void SetEffectValue(float value)
    {
        slowmoutionTimer = value;
    }
}
