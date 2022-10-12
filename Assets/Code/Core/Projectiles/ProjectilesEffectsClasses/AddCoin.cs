using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AddCoin : MonoBehaviour, IProjectile
{
    private int coins = 1;
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public ProjectileEffect GetEffect()
    {
        return ProjectileEffect.AddCoin;
    }

    public float GetEffectValue()
    {
        return coins;
    }

    public void SetEffectValue(float value)
    {
        coins = (int)value;
    }
}
