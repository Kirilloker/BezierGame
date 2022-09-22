using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChanger : MonoBehaviour, IProjectile
{
    private int healhtChange = 1;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.HealthChange;
    }

    public float GetEffectValue()
    {
        return healhtChange;
    }

    public void SetEffectValue(float value)
    {
        healhtChange = (int)value;
    }
}
