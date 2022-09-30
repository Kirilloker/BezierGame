using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    Animation Animation;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool active = false;

    private UnityEngine.Color activeColor = new Color(0.5490196f, 0.9019608f, 0.3176471f, 1f);
    private UnityEngine.Color disactiveColor = new Color(0.303048f, 0.4077995f, 0.488f, 1f);

    //����� ������� ������� ���������
    public delegate void PlayerHitPlatformHandler(Player player, Platform platform);
    public event PlayerHitPlatformHandler playerHitPlatform;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���� ����� ���������� � ����������
        if (collision.gameObject.tag == "Player")
        {
            //� ��������� �������
            if (active)
            {
                //��������� ����� - ����� �������� �������� ���������
                if (playerHitPlatform != null)
                    playerHitPlatform.Invoke(collision.gameObject.GetComponent<Player>(), this);
            }

            //��� ����� ��������� �����-�� ������ ��� ������ ������� ����� ���������
        }
        if (collision.gameObject.tag == "Projectile")
        {
            IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();

            //if((projectile.GetEffect() == ProjectileEffect.HealthChange) && (projectile.GetEffectValue() < 0))
            //    projectile.Destroy();

            //if((projectile.GetEffect() == ProjectileEffect.SizeChange) && (projectile.GetEffectValue() > 0))
            //    projectile.Destroy();

            //if ((projectile.GetEffect() == ProjectileEffect.SpeedChange) && (projectile.GetEffectValue() < 0))
            //    projectile.Destroy();

            //if (projectile.GetEffect() == ProjectileEffect.HidePath)
            //    projectile.Destroy();

            projectile.Destroy();
        }
    }

    public bool Active
    {
        get
        { // ��� ����� ������ ���� ��� ��������� ������� ���������
            return active;
        }
        set
        {
            active = value;
            if (value)
                Animation.Play("ActivePlatform");
            //spriteRenderer.color = activeColor;
            else
                Animation.Play("NoActivePlatform");
            //spriteRenderer.color = disactiveColor;
        }
    }
}