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

    private float projectileSpeed = 3;
    private int wayChangedCount = 0;

    private float DamageProjectilesTimer = 2f;
    private float PositiveProjectileTimer = 5f;
    private float NegativeProjectileTimer = 5f;

    #region Параметры различных снарядов
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
    private float subtractionSpeed = -2f;

    private float sizeDecDropChance = 0.1f;
    private float sizeDecreasing = -0.1f;

    private float healthUpDropChance = 0.1f;
    private float healthUpValue = 1;
    private float healthDamageValue = -1;

    private float hidePathTimer = 5f;

    private float countAddCoin = 1f;


    #endregion

    Dictionary<ProjectileEffect, ProjectileForm> possitiveEffectTest = new Dictionary<ProjectileEffect, ProjectileForm>()
    {
        { ProjectileEffect.HealthChange, ProjectileForm.HealthUp },
        { ProjectileEffect.SpeedChange, ProjectileForm.SpeedUp },
        { ProjectileEffect.SizeChange, ProjectileForm.SizeDec },
        { ProjectileEffect.Shield, ProjectileForm.Shield },
        { ProjectileEffect.ScoreMultiplyer, ProjectileForm.ScoreMultiplyer },
        { ProjectileEffect.Slowmoution, ProjectileForm.Slowmoution },
        { ProjectileEffect.CoinMagnet, ProjectileForm.CoinMagnet },
    };

    Dictionary<ProjectileEffect, ProjectileForm> NegativeEffectTest = new Dictionary<ProjectileEffect, ProjectileForm>()
    {
        { ProjectileEffect.HidePath, ProjectileForm.HidePath },
        { ProjectileEffect.SpeedChange, ProjectileForm.SpeedDown },
        { ProjectileEffect.SizeChange, ProjectileForm.SizeInc },
    };

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

        LoadDictionaryProjectileForm();
    }

    private void Start()
    {
        StartCoroutine(RandomDamage(DamageProjectilesTimer));
        StartCoroutine(RandomPositiveEffect(PositiveProjectileTimer));
        StartCoroutine(RandomNegativeEffect(NegativeProjectileTimer));
    }

    #region Сиситемы запуска различных проджектайлов
    IEnumerator RandomDamage(float timer)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            int randCannon = UnityEngine.Random.Range(0, cannons.Count);

            Fire(cannons[randCannon], ProjectileForm.Cube);
        }
        
        yield return new WaitForSeconds(DamageProjectilesTimer);
        StartCoroutine(RandomDamage(DamageProjectilesTimer));
    }


    IEnumerator RandomPositiveEffect(float timer)
    {
        int randCannon = UnityEngine.Random.Range(0, cannons.Count);

        ProjectileEffect randomPosEffect = effectGenerator.GetRandomPositiveEffect();

        Fire(cannons[randCannon], possitiveEffectTest[randomPosEffect]);

        yield return new WaitForSeconds(PositiveProjectileTimer);
        StartCoroutine(RandomPositiveEffect(PositiveProjectileTimer));
    }

    IEnumerator RandomNegativeEffect(float timer)
    {
        int randCannon = UnityEngine.Random.Range(0, cannons.Count);

        ProjectileEffect randomNegEffect = effectGenerator.GetRandomNegativeEffect();

        Fire(cannons[randCannon], NegativeEffectTest[randomNegEffect]);

        yield return new WaitForSeconds(NegativeProjectileTimer);
        StartCoroutine(RandomNegativeEffect(NegativeProjectileTimer));
    }

    private void Fire(Cannon cannon, ProjectileForm projectileForm)
    {
        LoadCannon(cannon, projectileForm);
        cannon.Fire(new Vector2(UnityEngine.Random.Range(-2, 2f), UnityEngine.Random.Range(-4f, 4f)),
           (int)projectileSpeed);
    }

    #endregion

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
                    effectGenerator.OpenEffect(ProjectileEffect.ScoreMultiplyer);
                    break;
                case ("Open magnet upgrade"):
                    magnetIsOpen = true;
                    effectGenerator.OpenEffect(ProjectileEffect.CoinMagnet);
                    break;
                case ("Open slowmoution upgrade"):
                    slowmoutionIsOpen = true;
                    effectGenerator.OpenEffect(ProjectileEffect.Slowmoution);
                    break;
                case ("Open shield upgrade"):
                    shieldIsOpen = true;
                    effectGenerator.OpenEffect(ProjectileEffect.Shield);
                    break;
            }

        }

        // Надеюсь там ссылки хранятся, но если нет лучше перезагружу
        LoadDictionaryProjectileForm();
    }
    public void OnScoreReachWayChangeValue()
    {
        wayChangedCount++;
    }
    #endregion


    private void LoadCannon(Cannon cannon, ProjectileForm form)
    {
        cannon.Load(
          projectilesPrefabs.GetProjectilePrefab(form), projectileForm[form].effect, projectileForm[form].effectValue);
    }

    Dictionary<ProjectileForm, TwoVal> projectileForm;

    private void LoadDictionaryProjectileForm()
    {
        projectileForm = new Dictionary<ProjectileForm, TwoVal>()
        {
            {ProjectileForm.HealthUp,       new TwoVal(ProjectileEffect.HealthChange,   healthUpValue) },
            {ProjectileForm.Cube,           new TwoVal(ProjectileEffect.HealthChange,   healthDamageValue) },
            {ProjectileForm.Coin,           new TwoVal(ProjectileEffect.AddCoin,        countAddCoin) },
            {ProjectileForm.SizeInc,        new TwoVal(ProjectileEffect.SizeChange,     0.25f) },
            {ProjectileForm.SizeDec,        new TwoVal(ProjectileEffect.SizeChange,     sizeDecreasing) },
            {ProjectileForm.SpeedUp,        new TwoVal(ProjectileEffect.SpeedChange,    addingSpeed) },
            {ProjectileForm.SpeedDown,      new TwoVal(ProjectileEffect.SpeedChange,    subtractionSpeed) },
            {ProjectileForm.Shield,         new TwoVal(ProjectileEffect.Shield,         shieldTimer) },
            {ProjectileForm.ScoreMultiplyer,new TwoVal(ProjectileEffect.ScoreMultiplyer,xscoreTimer) },
            {ProjectileForm.Slowmoution,    new TwoVal(ProjectileEffect.Slowmoution,    slowmoutionTimer) },
            {ProjectileForm.HidePath,       new TwoVal(ProjectileEffect.HidePath,       hidePathTimer) },
            {ProjectileForm.CoinMagnet,     new TwoVal(ProjectileEffect.CoinMagnet,     magnetTimer) },
        };
    }
}

public struct TwoVal
{
    public ProjectileEffect effect;
    public float effectValue;
    public TwoVal(ProjectileEffect effect, float effectValue)
    {
        this.effect = effect;
        this.effectValue = effectValue;
    }
}