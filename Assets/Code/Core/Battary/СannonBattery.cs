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
        if(Input.GetKeyDown(KeyCode.F))
        {
            cannons[0].Fire(new Vector2(0,0), 4);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
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
    }
}
