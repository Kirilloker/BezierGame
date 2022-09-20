using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesPrefabs : MonoBehaviour
{
    //������ �� ����������� �������
    GameObject projectile;

    //��� ��������� ��������� ������� ��������
    [SerializeField]
    private GameObject cubeProjectile;
    [SerializeField]
    private GameObject coinProjectile;
    [SerializeField]
    private GameObject scoreMultiplyerProjectile;
    [SerializeField]
    private GameObject speedUpProjectile;
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
                projectile = cubeProjectile;
                break;
            case ProjectileForm.Coin:
                projectile = coinProjectile;
                break;
        }

        return projectile;
    } 
}
