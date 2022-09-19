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

    public GameObject GetProjectilePrefab(ProjectileForm prefab)
    {
        //�������� ������
        switch (prefab)
        {
            case ProjectileForm.Cube:
                //��� ���� ���������
                projectile = cubeProjectile;
                break;
        }

        return projectile;
    } 
}
