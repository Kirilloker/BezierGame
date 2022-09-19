using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision != null) && (collision.gameObject.tag == "Projectile"))
        {
            collision.gameObject.GetComponent<IProjectile>().Destroy();
        }
    }
}
