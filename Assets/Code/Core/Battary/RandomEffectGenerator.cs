using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RandomEffectGenerator
{
    #region Позитивные эффекты
    private float SpeedUpChance = 0.20f;
    private float HealthUpChance = 0.15f;
    private float SizeDecChance = 0.20f;
    private float MagnetChance = 0.15f;
    private float SlowmoutionChance = 0.15f;
    private float ShieldChance = 0.075f;
    private float xScoreChance = 0.075f;

    private bool magnetOpen = false;
    private bool xScoreOpen = false;
    private bool slowmoutionOpen = false;
    private bool shieldOpen = false;
    #endregion

    #region Негативные эффекты
    private float hidePathChance = 0.05f;
    private float sizeIncChance = 0.45f;
    private float speedDownChance = 0.50f;
    #endregion

    public ProjectileEffect GetRandomPositiveEffect()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);

        float sum = 0f;

        if (roll <= SpeedUpChance)
            return ProjectileEffect.SpeedChange;

        sum += SpeedUpChance;

        if ((roll > sum) && (roll <= (sum + HealthUpChance)))
            return ProjectileEffect.HealthChange;

        sum += HealthUpChance;

        if ((roll > sum) && (roll <= (sum + SizeDecChance)))
            return ProjectileEffect.SizeChange;

        sum += SizeDecChance;

        if ((roll > sum) && (roll <= (sum + MagnetChance)) && magnetOpen)
            return ProjectileEffect.CoinMagnet;

        sum += MagnetChance;
        
        if ((roll > sum) && (roll <= (sum + SlowmoutionChance)) && slowmoutionOpen)
            return ProjectileEffect.Slowmoution;

        sum += SlowmoutionChance;

        if ((roll > sum) && (roll <= (sum + ShieldChance)) && shieldOpen)
            return ProjectileEffect.Shield;

        sum += ShieldChance;

        if ((roll > sum) && (roll <= (sum + xScoreChance)) && xScoreOpen)
            return ProjectileEffect.ScoreMultiplyer;

        return RollWithoutUpgrades();
    }
    public ProjectileEffect GetRandomNegativeEffect()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);

        float sum = 0f;

        if (roll <= hidePathChance)
            return ProjectileEffect.HidePath;

        sum += hidePathChance;

        if ((roll > sum) && (roll <= (sum + sizeIncChance)))
            return ProjectileEffect.SizeChange;

        sum += sizeIncChance;

        if ((roll > sum) && (roll <= (sum + speedDownChance)))
            return ProjectileEffect.SpeedChange;

        return ProjectileEffect.SpeedChange;
    }
    public void OpenMagnet()
    {
        magnetOpen = true;
    }
    public void OpenShield()
    {
        shieldOpen = true;
    }
    public void OpenSlowmounion()
    {
        slowmoutionOpen = true;
    }
    public void OpenXScore()
    {
        xScoreOpen = true;
    }
    private ProjectileEffect RollWithoutUpgrades()
    {
        float roll = UnityEngine.Random.Range(0f, 1f);

        if (roll <= 0.33f)
            return ProjectileEffect.SpeedChange;

        if ((roll > 0.33f) && (roll <= 0.66f))
            return ProjectileEffect.HealthChange;

        if ((roll > 0.66f) && (roll <= 1f))
            return ProjectileEffect.SizeChange;


        return ProjectileEffect.SpeedChange;
    }
}

