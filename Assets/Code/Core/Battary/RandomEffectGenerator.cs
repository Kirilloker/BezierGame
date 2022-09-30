using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RandomEffectGenerator
{
    Dictionary<ProjectileEffect, float> chanceNegativeEffects = new Dictionary<ProjectileEffect, float>()
    {
        { ProjectileEffect.HidePath,     0.10f},
        { ProjectileEffect.SizeChange,   0.45f},
        { ProjectileEffect.SpeedChange,  0.45f},
    };


    Dictionary<ProjectileEffect, float> chancePossitiveEffects = new Dictionary<ProjectileEffect, float>()
    {
        { ProjectileEffect.SpeedChange,     0.20f},
        { ProjectileEffect.HealthChange,    0.15f},
        { ProjectileEffect.SizeChange,      0.20f},
        { ProjectileEffect.CoinMagnet,      0.15f},
        { ProjectileEffect.Slowmoution,     0.15f},
        { ProjectileEffect.Shield,          0.075f},
        { ProjectileEffect.ScoreMultiplyer, 0.075f},
    };


    Dictionary<ProjectileEffect, bool> openPossitiveEffects = new Dictionary<ProjectileEffect, bool>()
    {
        { ProjectileEffect.SpeedChange,     true},
        { ProjectileEffect.HealthChange,    true},
        { ProjectileEffect.SizeChange,      true},
        { ProjectileEffect.CoinMagnet,      false},
        { ProjectileEffect.Slowmoution,     false},
        { ProjectileEffect.Shield,          false},
        { ProjectileEffect.ScoreMultiplyer, false},
    };

    public ProjectileEffect GetRandomPositiveEffect()
    {
        float roll = Random.Range(0f, 1f);
        float sum = 0f;

        foreach (var chanceEffects in chancePossitiveEffects)
        {
            if ((roll > sum) && (roll <= (sum + chanceEffects.Value)) && openPossitiveEffects[chanceEffects.Key])
                return chanceEffects.Key;

            sum += chanceEffects.Value;
        }

        return RollWithoutUpgrades();
    }
    public ProjectileEffect GetRandomNegativeEffect()
    {
        float roll = Random.Range(0f, 1f);
        float sum = 0f;

        foreach (var chanceEffects in chanceNegativeEffects)
        {
            if ((roll > sum) && (roll <= (sum + chanceEffects.Value)))
                return chanceEffects.Key;

            sum += chanceEffects.Value;
        }

        return ProjectileEffect.SpeedChange;
    }

    public void OpenEffect(ProjectileEffect projectileEffect)
    {
        openPossitiveEffects[projectileEffect] = true;
    }

    private ProjectileEffect RollWithoutUpgrades()
    {
        ProjectileEffect[] projectileEffect = new ProjectileEffect[]
        {
            ProjectileEffect.SpeedChange,
            ProjectileEffect.HealthChange,
            ProjectileEffect.SizeChange
        };

        return GetRandomProjectileEffectInArray(projectileEffect);
    }

    private ProjectileEffect GetRandomProjectileEffectInArray(ProjectileEffect[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}