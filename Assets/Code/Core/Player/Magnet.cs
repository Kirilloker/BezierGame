using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private float magnetSpeed = 5;

    List<ProjectileEffect> whoMagnet = new List<ProjectileEffect>();

    Vector2 targetPos;

    public void SetMagnetableProjectiles(List<ProjectileEffect> whoMagnet)
    {
        this.whoMagnet = whoMagnet;
    }

    public void FixedUpdate()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        List<GameObject> magnetableProjectiles = new List<GameObject>();


        //Выбираем все объекты, которые будут притягиваться
        foreach (GameObject projectile in projectiles)
        {
            ProjectileEffect effect = projectile.GetComponent<IProjectile>().GetEffect();
            float effectValue = projectile.GetComponent<IProjectile>().GetEffectValue();

            //Если эффект находится в списке тех, кто будет притягиваться
            if (whoMagnet.Contains(effect))
            {
                //Если наносит урон - пропускаем
                if (effect == ProjectileEffect.HealthChange && effectValue < 0)
                    continue;
                //Если увеличивает размер - пропускаем
                if (effect == ProjectileEffect.SizeChange && effectValue > 0)
                    continue;
                //Если замедляет игрока - пропускаем
                if (effect == ProjectileEffect.SpeedChange && effectValue < 0)
                    continue;

                magnetableProjectiles.Add(projectile);
            }
        }

        //Получаем актуальную позиция игрока
        targetPos = this.gameObject.transform.position;

        //Изменяем скорость всех снарядов - направляем их в игрока
        foreach(GameObject projectile in magnetableProjectiles)
        {
            Vector2 direction = (targetPos - (Vector2)projectile.transform.position).normalized;

            projectile.GetComponent<Rigidbody2D>().velocity = direction * magnetSpeed;
        }

    }
}
