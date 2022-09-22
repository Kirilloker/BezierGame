using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //������ ���������� �� ����������� ������
    private PlayerMover mover;
    private CircleCollider2D circleCollider;

    //�������������� ������_________________________________
    private float playerSpeed = 10;
    private int playerHealth = 1;
    private int scoreMultiplier = 1;
    private float playerSize = 1;
    private float maxSpeed = 50f;
    private float minSpeed = 5f;
    private bool shield = false;
    //______________________________________________________


    //������ ����������� �������_________________________________________________
    //����� ������
    public delegate void PlayerDeadHandler();
    public event PlayerDeadHandler playerDie;

    //����� ������������ ������ � ��������� - ��� ��������, ����������� �� ���������
    public delegate void PlayerFacedProjectile(ProjectileEffect effect, float projectileValue);
    public event PlayerFacedProjectile playerFacedProjectile;

    //����� �������� �������
    public delegate void PlayerPickUpCoin(int coins);
    public event PlayerPickUpCoin playerPickUpCoin;
    //___________________________________________________________________________

    private void Awake()
    {
        mover = GetComponent<PlayerMover>();
        mover.SetSpeed(playerSpeed);

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = true;
    }

    //��������� ������������ � ���������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();

            ProjectileEffect effect = projectile.GetEffect();
            float effectValue = projectile.GetEffectValue();

            switch (effect)
            {
                //__________________________________________________________________
                case ProjectileEffect.HealthChange:
                    if (shield && (effectValue < 0)) break;
                    int newHealth = PlayerHealth;
                    newHealth += (int)effectValue;
                    PlayerHealth = newHealth;
                    break;
                //__________________________________________________________________
                case ProjectileEffect.AddCoin:
                    playerPickUpCoin.Invoke((int)effectValue);
                    break;
                //__________________________________________________________________
                case ProjectileEffect.SizeChange:
                    float newSize = PlayerSize;
                    newSize += effectValue;
                    PlayerSize = newSize;
                    break;
                //__________________________________________________________________
                case ProjectileEffect.SpeedChange:
                    float newSpeed = PlayerSpeed;
                    newSpeed += effectValue;
                    PlayerSpeed = newSpeed;
                    break;
                //__________________________________________________________________
                case ProjectileEffect.Slowmoution:
                    StartCoroutine(EnableTempPlayerEffect(effect, effectValue));
                    break;
                //__________________________________________________________________
                case ProjectileEffect.Shield:
                    StartCoroutine(EnableTempPlayerEffect(effect, effectValue));
                    break;
                //__________________________________________________________________
                case ProjectileEffect.ScoreMultiplyer:
                    StartCoroutine(EnableTempPlayerEffect(effect, effectValue));
                    break;
                //__________________________________________________________________
                case ProjectileEffect.HidePath:
                    playerFacedProjectile.Invoke(effect, effectValue);
                    break;
                //__________________________________________________________________
                case ProjectileEffect.CoinMagnet:
                    playerFacedProjectile.Invoke(effect, effectValue);
                    break;
                //__________________________________________________________________

            }
            projectile.Destroy();
        }
    }


    //�������� ��� ��������� ��������________________________________
    IEnumerator EnableTempPlayerEffect(ProjectileEffect effect, float time)
    {
        switch (effect)
        {
            case ProjectileEffect.Shield:
                shield = true;
                yield return new WaitForSeconds(time);
                shield = false;
                break;

            case ProjectileEffect.ScoreMultiplyer:
                ScoreMultiplier *= 2;
                yield return new WaitForSeconds(time);
                scoreMultiplier /= 2;
                break;

            case ProjectileEffect.Slowmoution:
                Time.timeScale = 0.4f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                yield return new WaitForSeconds(time);
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                break;
        }
    }
    //_______________________________________________________________


    //����������� �������____________________________________________
    public void OnWayCreated(Dictionary<int, Vector2> wayPoints)
    {
        mover.SetWayPoints(wayPoints);
    }

    public void OnWayChanged(Dictionary<int, Vector2> wayPoints)
    {
        mover.SetWayPoints(wayPoints);
    }
    //_______________________________________________________________


    //��������_______________________________________________________
    public int PlayerHealth
    {
        //���� �������� ������ 0 ��� ������ - ��������� ����� ������
        set
        {
            playerHealth = value;
            if (playerHealth <= 0)
                playerDie.Invoke();
        }
        get
        {
            return playerHealth;
        }
    }
    public uint ScoreMultiplier
    {
        get { return (uint)scoreMultiplier; }
        set { scoreMultiplier = (int)value; }
    }
    public float PlayerSize
    {
        get { return playerSize; }
        set
        {
            playerSize = (float)value;

            if (playerSize < 0.35f)
                playerSize = 0.35f;
            if (playerSize > 1.6f)
                playerSize = 1.6f;

            this.transform.localScale = new Vector3(playerSize, playerSize, 0);
        }
    }
    public float PlayerSpeed
    {
        get { return playerSpeed; }
        set
        {
            playerSpeed = value;

            if (playerSpeed < minSpeed)
                playerSpeed = minSpeed;
            if (playerSpeed > maxSpeed)
                playerSpeed = maxSpeed;

            mover.SetSpeed(value);
        }
    }
    //_______________________________________________________________
}