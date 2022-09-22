using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour 
{
    private GameObject projectilePrefab;
    private GameObject launchedProjectile;

    private Vector2 cannonPos;

    private ProjectileEffect effect;
    private float effectValue;


    public void Load(GameObject prefab, ProjectileEffect effect, float effectValue)
    {
        this.projectilePrefab = prefab;
        this.effect = effect;
        this.effectValue = effectValue;
    }

    //������� ������� ������ ������� � ��������� ���
    public GameObject Fire(Vector2 targetPos, int firePower)
    {
        //������� � ������� ���� ������
        launchedProjectile = Instantiate(projectilePrefab, cannonPos, Quaternion.identity);

        //��������� ��� ������
        switch (effect)
        {
            case ProjectileEffect.HealthChange:
                launchedProjectile.AddComponent<HealthChanger>();
                break;
            case ProjectileEffect.AddCoin:
                launchedProjectile.AddComponent<AddCoin>();
                break;
            case ProjectileEffect.SizeChange:
                launchedProjectile.AddComponent<SizeChange>();
                break;
            case ProjectileEffect.SpeedChange:
                launchedProjectile.AddComponent<SpeedChange>();
                break;
            case ProjectileEffect.Shield:
                launchedProjectile.AddComponent<Shield>();
                break;
            case ProjectileEffect.Slowmoution:
                launchedProjectile.AddComponent<Slowmoution>();
                break;
            case ProjectileEffect.ScoreMultiplyer:
                launchedProjectile.AddComponent<ScoreMultiplyer>();
                break;
        }

        //������ �������� ������������ �������
        launchedProjectile.GetComponent<IProjectile>().SetEffectValue(effectValue);

        //��������� ������
        launchedProjectile.GetComponent<Rigidbody2D>().velocity = 
            (targetPos  - cannonPos).normalized * firePower;

        return launchedProjectile;
    }
    
    public void SetCannonPos(Vector2 newPos)
    {
        cannonPos = newPos;
    }
}
