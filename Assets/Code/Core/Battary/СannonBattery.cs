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


    //Список пушек
    private List<Cannon> cannons;

    //Пригодится для получения рандомных снарядов
    private int projectilesEffectsCount = Enum.GetNames(typeof(ProjectileEffect)).Length;
    private int projectilesFormsCount = Enum.GetNames(typeof(ProjectileForm)).Length;

    private void Awake()
    {
        cannons = new List<Cannon>();

        cannons.Add(new Cannon());

        cannons[0].SetCannonPos(new Vector2(3, 5));
    }

    //Test 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            cannons[0].Fire(new Vector2(0, 0), 4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Cube), ProjectileEffect.HealthChange, -1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Coin), ProjectileEffect.AddCoin, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SizeInc), ProjectileEffect.SizeChange, 0.25f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SizeDec), ProjectileEffect.SizeChange, -0.25f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SpeedUp), ProjectileEffect.SpeedChange, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SpeedDown), ProjectileEffect.SpeedChange, -1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Shield), ProjectileEffect.Shield, 10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.ScoreMultiplyer), ProjectileEffect.ScoreMultiplyer, 10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Slowmoution), ProjectileEffect.Slowmoution, 5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.HidePath), ProjectileEffect.HidePath, 5);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.CoinMagnet), ProjectileEffect.CoinMagnet, 5);

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            cannons[0].Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.HealthUp), ProjectileEffect.HealthChange, 1);

        }
    }

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
}
