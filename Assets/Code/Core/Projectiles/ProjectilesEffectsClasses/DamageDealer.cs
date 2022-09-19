using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour, IProjectile
{
    private float damage = 1;

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.DealDamage;
    }

    public float GetEffectValue()
    {
        return damage;
    }

    public void SetEffectValue(float value)
    {
        damage = value;
    }
}
