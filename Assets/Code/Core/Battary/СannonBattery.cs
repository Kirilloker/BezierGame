using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class СannonBattery : MonoBehaviour
{
    [SerializeField]
    private ProjectilesPrefabs projectilesPrefabs;
    [SerializeField]
    private Player player;

    private RandomEffectGenerator effectGenerator = new RandomEffectGenerator();
    private List<Cannon> cannons;

    #region параметры различных систем
    private int score = 0;
    private bool DamageSystemEnabled = false;
    private bool PositiveEffSystemEnabled = false;
    private bool NegativeEffSystemEnabled = false;
    private bool CoinSystemEnabled = false;

    private float DamageProjectilesTimer = 3f;
    private float PositiveProjectileTimer = 10f;
    private float NegativeProjectileTimer = 11f;
    private int coinsPerWay = 8;

    //Блок параметров для системы дамажных снарядов
    private int numOfDiffEvents = 1;
    private int numOfConcretEvents = 2;

    private const int maxNumOfDiffEvents = 5;
    private const int maxNumOfConcreteEvents = 5;
    #endregion

    #region Параметры различных снарядов

    private float damageProjectileSpeed = 3;
    private float neganiveEffProjectilesSpeed = 3;
    private float positiveEffProjectilesSpeed = 2;
    private float coinSpeed = 3;

    private bool magnetIsOpen = false;
    private float magnetTimer = 5;
    private float magnetDropChance = 0.1f;

    private bool shieldIsOpen = false;
    private float shieldTimer = 5;
    private float shieldDropChance = 0.1f;

    private bool slowmoutionIsOpen = false;
    private float slowmoutionTimer = 5;
    private float slowmoutionDropChance = 0.1f;

    private bool xscoreIsOpen = false;
    private float xscoreTimer = 5;
    private float xscoreDropChance = 0.1f;

    private float speedUpDropChance = 0.1f;
    private float addingSpeed = 0.2f;

    private float sizeDecDropChance = 0.1f;
    private float sizeDecreasing = -0.1f;

    private float healthUpDropChance = 0.1f;
    private float healthUpValue = 1;
    #endregion

    //Создаем пушки
    private void Awake()
    {
        cannons = new List<Cannon>();

        int startPos = 4;
        int endPos = -4;
        float interval = 0.5f;

        //Заполняем список пушками сверху вниз (четные справа, нечетные слева)
        for (int i = 0; 4 - ((float)(i / 2) * interval) >= endPos; i += 2)
        {
            cannons.Add(new Cannon());
            cannons[i].SetCannonPos(new Vector2(3.5f, startPos - ((float)(i / 2) * interval)));

            cannons.Add(new Cannon());
            cannons[i+1].SetCannonPos(new Vector2(-3.5f, startPos - ((float)(i / 2) * interval)));
        }
    }

    private void FixedUpdate()
    {
        if(!DamageSystemEnabled)
            StartCoroutine(DamageSystem());

        if(!PositiveEffSystemEnabled)
            StartCoroutine(RandomPositiveEffect());

        if(!NegativeEffSystemEnabled)
            StartCoroutine(RandomNegativeEffect());

        if(!CoinSystemEnabled)
            StartCoroutine(RandomCoinProjectiles());
    }

    #region Сиситемы запуска различных проджектайлов
    IEnumerator DamageSystem()
    {
        DamageSystemEnabled = true;

        ChangeComplexity();


        //StartCoroutine(RandomProjectiles(5));
        //StartCoroutine(LaserProjectiles(3));
        //StartCoroutine(BundleProjectiles(2));
        //StartCoroutine(RandomSnakeProjectiles(3));
        //StartCoroutine(RandomWallProjectiles(3));
        //StartCoroutine(RandomWindowProjectiles(1));


        yield return new WaitForSeconds(DamageProjectilesTimer);
        DamageSystemEnabled = false;
    }

    IEnumerator RandomPositiveEffect()
    {
        PositiveEffSystemEnabled = true;
        yield return new WaitForSeconds(PositiveProjectileTimer);

        int randCannon = UnityEngine.Random.Range(0, cannons.Count);
        switch (effectGenerator.GetRandomPositiveEffect())
        {
            case ProjectileEffect.HealthChange:
                LoadHealthUp(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)positiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.SpeedChange:;
                LoadSpeedUp(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)positiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.SizeChange:
                LoadSizeDec(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)positiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.Shield:
                LoadShield(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)positiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.ScoreMultiplyer:
                LoadScoreMult(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)positiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.Slowmoution:
                LoadSlowmoution(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)positiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.CoinMagnet:
                LoadMagnet(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)positiveEffProjectilesSpeed);
                break;
        }
        PositiveEffSystemEnabled = false;
    }

    IEnumerator RandomNegativeEffect()
    {
        NegativeEffSystemEnabled = true;
        yield return new WaitForSeconds(NegativeProjectileTimer);

        int randCannon = UnityEngine.Random.Range(0, cannons.Count);
        switch (effectGenerator.GetRandomNegativeEffect())
        {
            case ProjectileEffect.HidePath:
                LoadHidePath(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)neganiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.SpeedChange:
                LoadSpeedDown(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)neganiveEffProjectilesSpeed);
                break;

            case ProjectileEffect.SizeChange:
                LoadSizeInc(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)neganiveEffProjectilesSpeed);
                break;
        }
        NegativeEffSystemEnabled = false;
    }

    IEnumerator RandomCoinProjectiles()
    {
        CoinSystemEnabled = true;
        while (coinsPerWay > 0)
        {
            int randCannon = UnityEngine.Random.Range(0, cannons.Count);
            LoadCoin(cannons[randCannon]);

            cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
               (int)coinSpeed);

            coinsPerWay--;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.2f, 2f));
        }
        CoinSystemEnabled = false;
    }
    #endregion

    #region Различные ивенты для дамажных снарядов
    IEnumerator RandomProjectiles(int numOfProjectiles)
    {
        for (int i = 0; i < numOfProjectiles; i++)
        {
            yield return new WaitForSeconds(0.3f);
            int randCannon = UnityEngine.Random.Range(0, cannons.Count);

            LoadDamageDealer(cannons[randCannon]);

            cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
               (int)damageProjectileSpeed);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator LaserProjectiles(int numOfLasers)
    {
        for (int i = 0; i < numOfLasers; i++)
        {
            int randCannon = UnityEngine.Random.Range(0, cannons.Count);

            for (int j = 0; j < 6; j++)
            {
                yield return new WaitForSeconds(0.16f);
                LoadDamageDealer(cannons[randCannon]);
                cannons[randCannon].Fire(new Vector2(0,cannons[randCannon].GetCannonPos().y),
                   (int)damageProjectileSpeed);
            }
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator BundleProjectiles(int numOfBundeles)
    {
        for (int i = 0; i < numOfBundeles; i++)
        {
            int randCannon = UnityEngine.Random.Range(0, cannons.Count);

            for (int j = 0; j < 6; j++)
            {
                yield return new WaitForSeconds(0.1f);
                LoadDamageDealer(cannons[randCannon]);
                cannons[randCannon].Fire(GetRandomTagretForCannon(cannons[randCannon]),
                   (int)damageProjectileSpeed);
            }
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator RandomSnakeProjectiles(int numOfSnakes)
    {
        for(int j = 0; j < numOfSnakes; j++)
        {
            int randCannon = UnityEngine.Random.Range(10, cannons.Count - 10);
            int direction = UnityEngine.Random.Range(-1, 1);

            if (direction < 0) direction = -1;
            else direction = 1;

            for (int i = 0; i < 5; i++)
            {
                int cannonNum = randCannon + ((i * direction) * 2);
                LoadDamageDealer(cannons[cannonNum]);
                cannons[cannonNum].Fire(
                   new Vector2(0, cannons[randCannon].GetCannonPos().y),
                   (int)damageProjectileSpeed);
                yield return new WaitForSeconds(0.15f);
            }
        }    
        yield return new WaitForSeconds(1f);
    }

    IEnumerator RandomWallProjectiles(int numOfWalls)
    {
        for (int j = 0; j < numOfWalls; j++)
        {
            int randCannon = UnityEngine.Random.Range(7, cannons.Count - 7);
            int direction = UnityEngine.Random.Range(-1, 1);

            if (direction < 0) direction = -1;
            else direction = 1;

            for (int i = 0; i < 3; i++)
            {
                int cannonNum = randCannon + ((i * direction) * 2);
                LoadDamageDealer(cannons[cannonNum]);
                cannons[cannonNum].Fire(
                   new Vector2(0, cannons[cannonNum].GetCannonPos().y),
                   (int)damageProjectileSpeed);
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator RandomWindowProjectiles(int numOfWindows)
    {
        for (int j = 0; j < numOfWindows; j++)
        {
            int randCannon = UnityEngine.Random.Range(16, cannons.Count - 16);
            int direction = UnityEngine.Random.Range(-1, 1);

            if (direction < 0) direction = -1;
            else direction = 1;

            for (int i = 0; i < 8; i++)
            {
                if (i > 1 && i < 6) continue;

                int cannonNum = randCannon + ((i * direction) * 2);
                LoadDamageDealer(cannons[cannonNum]);
                cannons[cannonNum].Fire(
                   new Vector2(0, cannons[cannonNum].GetCannonPos().y),
                   (int)damageProjectileSpeed);
            }
        }
        yield return new WaitForSeconds(1f);
    }
    #endregion region

    public Vector2 GetRandomTagretForCannon(Cannon cannon)
    {
        Vector2 target = new Vector2(0, cannon.GetCannonPos().y + UnityEngine.Random.Range(1f, -1f));
        return target;
    }

    public void ChangeComplexity()
    {
        if(score > 5 )

        if (score > 3)
        {
            numOfDiffEvents = 2;
            numOfConcretEvents = 2;
        }
    }

    #region Обработчики ивентов
    public void OnGameDataLoaded(Dictionary<string, float> upgrades)
    {
        foreach (var upgrade in upgrades)
        {
            switch (upgrade.Key)
            {
                case ("Increase adding speed"):
                    addingSpeed += upgrade.Value;
                    break;
                case ("Increase speedup drop chance"):
                    speedUpDropChance += upgrade.Value;
                    break;
                case ("Increase size decrease"):
                    sizeDecreasing -= upgrade.Value;
                    break;
                case ("Increase sizedec drop chance"):
                    sizeDecDropChance += upgrade.Value;
                    break;
                case ("Increase shield timer"):
                    shieldTimer += upgrade.Value;
                    break;
                case ("Increase shield drop chance"):
                    shieldDropChance += upgrade.Value;
                    break;
                case ("Increase adding health"):
                    healthUpValue += upgrade.Value;
                    break;
                case ("Increase heathup drop chance"):
                    healthUpDropChance += upgrade.Value;
                    break;
                case ("Increase slowmoution timer"):
                    slowmoutionTimer += upgrade.Value;
                    break;
                case ("Increase slowmoution drop chance"):
                    slowmoutionDropChance += upgrade.Value;
                    break;
                case ("Increase magnet drop chance"):
                    magnetDropChance += upgrade.Value;
                    break;
                case ("Increase magnet timer"):
                    magnetTimer += upgrade.Value;
                    break;
                case ("Increase xscore timer"):
                    xscoreTimer += upgrade.Value;
                    break;
                case ("Increase xscore drop chance"):
                    xscoreDropChance += upgrade.Value;
                    break;
                case ("Open xscore upgrade"):
                    xscoreIsOpen = true;
                    effectGenerator.OpenXScore();
                    break;
                case ("Open magnet upgrade"):
                    magnetIsOpen = true;
                    effectGenerator.OpenMagnet();
                    break;
                case ("Open slowmoution upgrade"):
                    slowmoutionIsOpen = true;
                    effectGenerator.OpenSlowmounion();
                    break;
                case ("Open shield upgrade"):
                    shieldIsOpen = true;
                    effectGenerator.OpenShield();
                    break;
            }

        }
    }

    public void OnScoreChangeValue()
    {
        coinsPerWay = 4;
        score++;
    }

    #endregion

    #region Методы для зарядки различных снарядов
    private void LoadHealthUp(Cannon cannon)
    {
        cannon.Load(
            projectilesPrefabs.GetProjectilePrefab(ProjectileForm.HealthUp), ProjectileEffect.HealthChange, healthUpValue);
    }

    private void LoadDamageDealer(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Cube), ProjectileEffect.HealthChange, -1);
    }

    private void LoadCoin(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Coin), ProjectileEffect.AddCoin, 1);
    }

    private void LoadSizeInc(Cannon cannon)
    {
        cannon.Load(
         projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SizeInc), ProjectileEffect.SizeChange, 0.25f);
    }

    private void LoadSizeDec(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SizeDec), ProjectileEffect.SizeChange, sizeDecreasing);
    }

    private void LoadSpeedUp(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SpeedUp), ProjectileEffect.SpeedChange, addingSpeed);
    }

    private void LoadSpeedDown(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.SpeedDown), ProjectileEffect.SpeedChange, -2);
    }
    private void LoadShield(Cannon cannon)
    {
        cannon.Load(
         projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Shield), ProjectileEffect.Shield, shieldTimer);
    }

    private void LoadScoreMult(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.ScoreMultiplyer), ProjectileEffect.ScoreMultiplyer, xscoreTimer);
    }

    private void LoadSlowmoution(Cannon cannon)
    {
        cannon.Load(
         projectilesPrefabs.GetProjectilePrefab(ProjectileForm.Slowmoution), ProjectileEffect.Slowmoution, slowmoutionTimer);
    }

    private void LoadHidePath(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.HidePath), ProjectileEffect.HidePath, 5);
    }

    private void LoadMagnet(Cannon cannon)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(ProjectileForm.CoinMagnet), ProjectileEffect.CoinMagnet, magnetTimer);
    }
    #endregion

    #region Функции усложнения игры

    public float FuncPositiveTimer(float x)
    {
        float normalizeX = x / 15f;

        if (normalizeX < 4)
        {
            return 20;
        }
        else if (normalizeX > 25)
        {
            return 4;
        }

        return (9.6f) / (0.13f * normalizeX) + 0.8f;
    }

    public float FuncNegativeTimer(float x)
    {
        float normalizeX = x / 15f;

        if (normalizeX < 4)
        {
            return 25;
        }
        else if (normalizeX > 25)
        {
            return 1;
        }

        return (4f) / (0.047f * normalizeX) - 2.2f;
    }

    #endregion
}
