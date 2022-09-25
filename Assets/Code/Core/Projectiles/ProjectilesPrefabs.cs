using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPrefabs : MonoBehaviour
{
    //Ссылка на создаваемые префабы
    GameObject projectile;

    //Тут добавляем различные префабы снарядов
    [SerializeField]
    private GameObject damageCubeProjectile;
    [SerializeField]
    private GameObject coinProjectile;
    [SerializeField]
    private GameObject speedUpProjectile;
    [SerializeField]
    private GameObject speedDownProjectile;
    [SerializeField]
    private GameObject sizeIncProjectile;
    [SerializeField]
    private GameObject sizeDecProjectile;
    [SerializeField]
    private GameObject scoreMultiplyerProjectile;
    [SerializeField]
    private GameObject SlowmoutionProjectile;
    [SerializeField]
    private GameObject ShieldProjectile;
    [SerializeField]
    private GameObject HidePathProjectile;
    [SerializeField]
    private GameObject CoinMagnetProjectile;
    [SerializeField]
    private GameObject HealthUpProjectile;


    public GameObject GetProjectilePrefab(ProjectileForm prefab)
    {
        //Выбираем префаб
        switch (prefab)
        {
            case ProjectileForm.Cube:
                projectile = damageCubeProjectile;
                break;
            case ProjectileForm.Coin:
                projectile = coinProjectile;
                break;
            case ProjectileForm.SpeedUp:
                projectile = speedUpProjectile;
                break;
            case ProjectileForm.SpeedDown:
                projectile = speedDownProjectile;
                break;
            case ProjectileForm.SizeDec:
                projectile = sizeDecProjectile;
                break;
            case ProjectileForm.SizeInc:
                projectile = sizeIncProjectile;
                break;
            case ProjectileForm.ScoreMultiplyer:
                projectile = scoreMultiplyerProjectile;
                break;
            case ProjectileForm.Shield:
                projectile = ShieldProjectile;
                break;
            case ProjectileForm.Slowmoution:
                projectile = SlowmoutionProjectile;
                break;
            case ProjectileForm.CoinMagnet:
                projectile = CoinMagnetProjectile;
                break;
            case ProjectileForm.HidePath:
                projectile = HidePathProjectile;
                break;
            case ProjectileForm.HealthUp:
                projectile = HealthUpProjectile;
                break;
        }

        return projectile;
    } 
}
