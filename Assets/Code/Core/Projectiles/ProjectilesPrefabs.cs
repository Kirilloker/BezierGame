using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPrefabs : MonoBehaviour
{
    //������ �� ����������� �������
    GameObject projectile;

    //��� ��������� ��������� ������� ��������
    [SerializeField]
    private GameObject damageCubeProjectile;
    [SerializeField]
    private GameObject coinProjectile;
    [SerializeField]
    private GameObject scoreMultiplyerProjectile;
    [SerializeField]
    private GameObject speedUpProjectile;
    [SerializeField]
    private GameObject speedDownProjectile;
    [SerializeField]
    private GameObject sizeIncreaseProjectile;
    [SerializeField]
    private GameObject sizeDecreaseProjectile;


    public GameObject GetProjectilePrefab(ProjectileForm prefab)
    {
        //�������� ������
        switch (prefab)
        {
            case ProjectileForm.Cube:
                projectile = damageCubeProjectile;
                break;
            case ProjectileForm.Coin:
                projectile = coinProjectile;
                break;
        }

        return projectile;
    } 
}
