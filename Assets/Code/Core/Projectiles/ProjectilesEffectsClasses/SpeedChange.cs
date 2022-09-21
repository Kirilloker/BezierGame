using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour, IProjectile
{
    private float speedChange = 0.25f;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.SpeedChange;
    }

    public float GetEffectValue()
    {
        return speedChange;
    }

    public void SetEffectValue(float value)
    {
        speedChange = value;
    }
}
