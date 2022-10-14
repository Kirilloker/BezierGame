using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Объект отвечающий за перемещение игрока
    private PlayerMover mover;
    private CircleCollider2D circleCollider;

    //Характеристики игрока_________________________________
    private float playerSpeed;
    private float playerBaseSpeed = 8;
    private float maxSpeed = 35f;
    private float minSpeed = 5f;
   
    private float playerSize;
    private float playerBaseSize = 1.2f;
    private float minSize = 0.5f;
    private float maxSize = 1.8f;

    private int playerHealth;
    private int playerBaseHealth = 1;
    private int MaxHeath = 3;

    private int currentScoreMultiplyer = 1;
    private int scoreMultiplier = 2;
    private bool shield = false;
    private float slowmoutionPower = 0.75f;
    private List<ProjectileEffect> whoMagneting = new List<ProjectileEffect> { ProjectileEffect.AddCoin};
    //______________________________________________________


    //Ивенты поднимаемые игроком_________________________________________________
    //Ивент смерти
    public delegate void PlayerDeadHandler();
    public event PlayerDeadHandler playerDie;

    //Ивент столкновения игрока с снарядами - для эффектов, действующих на окружение
    public delegate void PlayerFacedProjectile(ProjectileEffect effect, float effectValue);
    public event PlayerFacedProjectile playerFacedProjectile;

    //Ивент поднятия монетки
    public delegate void PlayerPickUpCoin(int coins);
    public event PlayerPickUpCoin playerPickUpCoin;
    //___________________________________________________________________________

    public GameUI gameUI;

    private void Awake()
    {
        mover = GetComponent<PlayerMover>();
        mover.SetSpeed(playerSpeed);

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = true;

        gameUI = GameObject.FindGameObjectWithTag("UIGame").GetComponent<GameUI>();
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

                    gameUI.CreateEffect(UpgradeEffect.Moution, effectValue);
                    break;
                //__________________________________________________________________
                case ProjectileEffect.Shield:
                    StartCoroutine(EnableTempPlayerEffect(effect, effectValue));

                    gameUI.CreateEffect(UpgradeEffect.Shield, effectValue);
                    break;
                //__________________________________________________________________
                case ProjectileEffect.ScoreMultiplyer:
                    StartCoroutine(EnableTempPlayerEffect(effect, effectValue));

                    gameUI.CreateEffect(UpgradeEffect.XScore, effectValue);
                    break;
                //__________________________________________________________________
                case ProjectileEffect.HidePath:
                    playerFacedProjectile.Invoke(effect, effectValue);

                    break;
                //__________________________________________________________________
                case ProjectileEffect.CoinMagnet:
                    StartCoroutine(EnableTempPlayerEffect(effect, effectValue));

                    gameUI.CreateEffect(UpgradeEffect.Magnite, effectValue);
                    break;
                //__________________________________________________________________

            }
            projectile.Destroy();
        }
    }

    //Куротина для временных эффектов________________________________
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
                currentScoreMultiplyer = scoreMultiplier;
                yield return new WaitForSeconds(time);
                currentScoreMultiplyer = 1;
                break;

            case ProjectileEffect.Slowmoution:
                Time.timeScale = slowmoutionPower;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                yield return new WaitForSeconds(time * slowmoutionPower);
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                break;

            case ProjectileEffect.CoinMagnet:
                Magnet magnet = this.gameObject.AddComponent<Magnet>();
                magnet.SetMagnetableProjectiles(whoMagneting);
                yield return new WaitForSeconds(time);
                Destroy(magnet);
                break;
        }
    }
    //_______________________________________________________________


    //Обработчики ивентов____________________________________________
    public void OnWayCreated(Dictionary<int, Vector2> wayPoints)
    {
        mover.SetWayPoints(wayPoints);
    }

    public void OnWayChanged(Dictionary<int, Vector2> wayPoints)
    {
        mover.SetWayPoints(wayPoints);
    }

    public void OnGameDataLoaded(Dictionary<string, float> upgrades)
    {
        foreach(var upgrade in upgrades)
        {
            switch (upgrade.Key)
            {
                case ("Increase max speed"):
                    maxSpeed += upgrade.Value;
                    break;
                case ("Decrease min size"):
                    minSize -= upgrade.Value;
                    break;
                case ("Increase base health"):
                    playerBaseHealth += (int)upgrade.Value;
                    break;
                case ("Increase base speed"):
                    playerBaseSpeed += upgrade.Value;
                    break;
                case ("Decrease base size"):
                    playerBaseSize -= upgrade.Value;
                    break;
                case ("Increase max health"):
                    MaxHeath += (int)upgrade.Value;
                    break;
                case ("Increase slowmoution effect"):
                    slowmoutionPower -= upgrade.Value;
                    break;
                case ("Magniting healthup"):
                    whoMagneting.Add(ProjectileEffect.HealthChange);
                    break;
                case ("Magniting speedup"):
                    whoMagneting.Add(ProjectileEffect.SpeedChange);
                    break;              
                case ("Magniting sise decrease"):
                    whoMagneting.Add(ProjectileEffect.SizeChange);
                    break;
                case ("Increase xscore multiplyer"):
                    scoreMultiplier += (int)upgrade.Value;
                    break;
            }
        }

        PlayerHealth = playerBaseHealth;
        PlayerSpeed = playerBaseSpeed;
        PlayerSize = playerBaseSize;
    }
    //_______________________________________________________________

    //Свойства_______________________________________________________
    public int PlayerHealth
    {
        //Если здоровье игрока 0 или меньше - запускаем ивент смерти
        set
        {
            playerHealth = value;
            if (playerHealth > MaxHeath)
                playerHealth = MaxHeath;

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
        get { return (uint)currentScoreMultiplyer; }
        set { currentScoreMultiplyer = (int)value; }
    }
    public float PlayerSize
    {
        get { return playerSize; }
        set
        {
            playerSize = (float)value;

            if (playerSize < minSize)
                playerSize = minSize;
            if (playerSize > maxSize)
                playerSize = maxSize;

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