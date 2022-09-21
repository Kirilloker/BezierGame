using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool active;

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
                spriteRenderer.color = activeColor;
            else
                spriteRenderer.color = disactiveColor;
        }
    }
}