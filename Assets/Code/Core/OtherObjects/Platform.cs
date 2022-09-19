using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool active;

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
                spriteRenderer.color = Color.green;
            else
                spriteRenderer.color = Color.grey;
        }
    }
}