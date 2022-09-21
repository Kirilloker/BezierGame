using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Объект отвечающий за перемещение игрока
    private PlayerMover mover;
    private CircleCollider2D circleCollider;

    //Характеристики игрока_________________________________
    private float playerSpeed = 10;
    private int playerHealth = 1;
    private int scoreMultiplier = 1;
    private float playerSize = 1;
    private float maxSpeed = 50f;
    private float minSpeed = 5f;
    //______________________________________________________


    //Ивенты поднимаемые игроком_________________________________________________
    //Ивент смерти
    public delegate void PlayerDeadHandler();
    public event PlayerDeadHandler playerDie;

    //Ивентs столкновения игрока с снарядами
    public delegate void PlayerFacedProjectile();
    public event PlayerFacedProjectile playerFacedProjectile;

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

    //Обработка столкновений с снарядами
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
                default:
                    break;
            }

            projectile.Destroy();
        }
    }

    //Обработчики ивентов____________________________________________
    public void OnWayCreated(Dictionary<int, Vector2> wayPoints)
    {
        mover.SetWayPoints(wayPoints);
    }

    public void OnWayChanged(Dictionary<int, Vector2> wayPoints)
    {
        mover.SetWayPoints(wayPoints);
    }
    //_______________________________________________________________


    //Свойства_______________________________________________________
    public int PlayerHealth
    {
        //Если здоровье игрока 0 или меньше - запускаем ивент смерти
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