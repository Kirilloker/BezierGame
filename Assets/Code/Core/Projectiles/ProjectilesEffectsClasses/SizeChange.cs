using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour, IProjectile
{
    private float sizeChange = 0.25f;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.SizeChange;
    }

    public float GetEffectValue()
    {
        return sizeChange;
    }


    public void SetEffectValue(float value)
    {
        sizeChange = value;
    }
}
