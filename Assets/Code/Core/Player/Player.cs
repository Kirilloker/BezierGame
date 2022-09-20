using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Объект отвечающий за перемещение игрока
    private PlayerMover mover;
    private CircleCollider2D circleCollider;

    //Характеристики игрока_________________________________
    private float playerSpeed = 1;
    private float playerInertia = 0.04f;
    private int playerHealth = 1;
    private int scoreMultiplier = 1;
    private float playerSize = 0.5f;
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
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = true;
    }

    //Обработка столкновений с снарядами
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            IProjectile projectile = collision.gameObject.GetComponent<IProjectile>();

            ProjectileEffect effect = projectile.GetEffect();
            float effectValue = projectile.GetEffectValue();

            Debug.Log(effectValue);

            switch (effect)
            {
                case ProjectileEffect.DealDamage:
                    int newHealth = PlayerHealth;
                    newHealth -= (int)effectValue;
                    PlayerHealth = newHealth;
                    break;
                case ProjectileEffect.AddCoin:
                    playerPickUpCoin.Invoke((int)effectValue);
                    break;
                default:
                    break;
            }

            projectile.Destroy();
        }
    }

    //Управление персонажем
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.S))
        {
            mover.MovePlayer(-1 * playerSpeed, playerInertia);
        }

        if (Input.GetKey(KeyCode.W))
        {

            mover.MovePlayer(playerSpeed, playerInertia);
        }

        if (Input.GetMouseButton(0))
        {
            //CreatePointBezier(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3 vectorTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (vectorTouch.x < 0)
                mover.MovePlayer(playerSpeed, playerInertia);
            else
                mover.MovePlayer(-1 * playerSpeed, playerInertia);

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
            this.transform.localScale = new Vector3(playerSize, playerSize, 0);
        }
    }
    //_______________________________________________________________
}