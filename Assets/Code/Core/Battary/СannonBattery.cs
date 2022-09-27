using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class СannonBattery : MonoBehaviour
{
    [SerializeField]
    private ProjectilesPrefabs projectilesPrefabs;
    [SerializeField]
    private Player player;

    //Список пушек
    private List<Cannon> cannons;
    private float projectileSpeed = 4;
    private int wayChangedCount = 0;

    #region Параметры различных снарядов
    private bool magnetIsOpen = false;
    private float magnetTimer = 5;
    private float magnetDropChance = 0.1f;

    private bool shieldIsOpen = false;
    private float shieldTimer = 5;
    private float shieldDropChance = 0.1f;

    private bool slowmoutionIsOpen = false;
    private float slowmoutionTimer = 5;
    private float slowmoutionDropChance = 0.1f;

    private bool xscoreIsOpen = false;
    private float xscoreTimer = 5;
    private float xscoreDropChance = 0.1f;

    private float speedUpDropChance = 0.1f;
    private float addingSpeed = 0.2f;

    private float sizeDecDropChance = 0.1f;
    private float sizeDecreasing = -0.1f;

    private float healthUpDropChance = 0.1f;
    private float healthUpValue = 1;
    #endregion

    //Создаем пушки
    private void Awake()
    {
        cannons = new List<Cannon>();

        int startPos = 4;
        int endPos = -4;
        float interval = 0.5f;

        //Заполняем список пушками сверху вниз (четные справа, нечетные слева)
        for (int i = 0; 4 - ((float)(i / 2) * interval) >= endPos; i += 2)
        {
            cannons.Add(new Cannon());
            cannons[i].SetCannonPos(new Vector2(3, startPos - ((float)(i / 2) * interval)));

            cannons.Add(new Cannon());
            cannons[i+1].SetCannonPos(new Vector2(-3, startPos - ((float)(i / 2) * interval)));
        }

        foreach(Cannon cannon in cannons)
        {
            LoadDamageDealer(cannon);
        }
    }

    //Test 
    private void FixedUpdate()
    {

        if (UnityEngine.Random.RandomRange(0f, 1f) > 0.98f)
        {
            cannons[UnityEngine.Random.RandomRange(0, cannons.Count)].Fire(
               new Vector2(UnityEngine.Random.Range(-2, 2f), UnityEngine.Random.Range(-4f, 4f)),
               (int)projectileSpeed);
        }
    }

    #region Обработчики ивентов
    public void OnGameDataLoaded(Dictionary<string, float> upgrades)
    {
        foreach (var upgrade in upgrades)
        {
            switch (upgrade.Key)
            {
                case ("Increase adding speed"):
                    addingSpeed += upgrade.Value;
                    break;
                case ("Increase speedup drop chance"):
                    speedUpDropChance += upgrade.Value;
                    break;
                case ("Increase size decrease"):
                    sizeDecreasing -= upgrade.Value;
                    break;
                case ("Increase sizedec drop chance"):
                    sizeDecDropChance += upgrade.Value;
                    break;
                case ("Increase shield timer"):
                    shieldTimer += upgrade.Value;
                    break;
                case ("Increase shield drop chance"):
                    shieldDropChance += upgrade.Value;
                    break;
                case ("Increase adding health"):
                    healthUpValue += upgrade.Value;
                    break;
                case ("Increase heathup drop chance"):
                    healthUpDropChance += upgrade.Value;
                    break;
                case ("Increase slowmoution timer"):
                    slowmoutionTimer += upgrade.Value;
                    break;
                case ("Increase slowmoution drop chance"):
                    slowmoutionDropChance += upgrade.Value;
                    break;
                case ("Increase magnet drop chance"):
                    magnetDropChance += upgrade.Value;
                    break;
                case ("Increase magnet timer"):
                    magnetTimer += upgrade.Value;
                    break;
                case ("Increase xscore timer"):
                    xscoreTimer += upgrade.Value;
                    break;
                case ("Increase xscore drop chance"):
                    xscoreDropChance += upgrade.Value;
                    break;
                case ("Open xscore upgrade"):
                    xscoreIsOpen = true;
                    break;
                case ("Open magnet upgrade"):
                    magnetIsOpen = true;
                    break;
                case ("Open slowmoution upgrade"):
                    slowmoutionIsOpen = true;
                    break;
                case ("Open shield upgrade"):
                    shieldIsOpen = true;
                    break;
            }

        }
    }
    public void OnScoreReachWayChangeValue()
    {
        wayChangedCount++;
    }
    #endregion

    #region Методы для зарядки различных снарядов
    private void LoadHealthUp(Cannon cannon)
    {
        cannon.Load(
            projectilesPrefabs.GetProjectilePrefab(ProjectileForm.HealthUp), ProjectileEffect.HealthChange, healthUpValue);
    }

    private void LoadDamageDealer(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Cube), ProjectileEffect.HealthChange, -1);
    }

    private void LoadCoin(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Coin), ProjectileEffect.AddCoin, 1);
    }

    private void LoadSizeInc(Cannon cannon)
    {
        cannon.Load(
         projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SizeInc), ProjectileEffect.SizeChange, 0.25f);
    }

    private void LoadSizeDec(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SizeDec), ProjectileEffect.SizeChange, sizeDecreasing);
    }

    private void LoadSpeedUp(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SpeedUp), ProjectileEffect.SpeedChange, addingSpeed);
    }

    private void LoadSpeedDown(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SpeedDown), ProjectileEffect.SpeedChange, -2);
    }
    private void LoadShield(Cannon cannon)
    {
        cannon.Load(
         projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Shield), ProjectileEffect.Shield, shieldTimer);
    }

    private void LoadScoreMult(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.ScoreMultiplyer), ProjectileEffect.ScoreMultiplyer, xscoreTimer);
    }

    private void LoadSlowmoution(Cannon cannon)
    {
        cannon.Load(
         projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Slowmoution), ProjectileEffect.Slowmoution, slowmoutionTimer);
    }

    private void LoadHidePath(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.HidePath), ProjectileEffect.HidePath, 5);
    }

    private void LoadMagnet(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.CoinMagnet), ProjectileEffect.CoinMagnet, magnetTimer);
    }
    #endregion
}
