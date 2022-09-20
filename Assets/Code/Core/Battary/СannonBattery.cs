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
    }
}
