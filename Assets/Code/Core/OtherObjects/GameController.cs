using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SceneManager sceneManager;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Platform platform1;
    [SerializeField]
    private Platform platform2;
    [SerializeField]
    private �annonBattery battery;
    [SerializeField]
    private Way way;

    [SerializeField]
    private GameDataManager dataManager;
    private Score score;

    //����� �������� ������� ������
    public delegate void GameDataLoadedHandler(Dictionary<string, float> upgrades);
    public event GameDataLoadedHandler gameDataLoaded;


    private void Awake()
    {
        dataManager = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataManager>();
        score = new Score(dataManager);

        //������������ ����������� ��������� �������
        platform1.playerHitPlatform += score.OnPlayerHitPlatform;
        platform2.playerHitPlatform += score.OnPlayerHitPlatform;
        platform1.playerHitPlatform += this.OnPlayerHitPlatform;
        platform2.playerHitPlatform += this.OnPlayerHitPlatform;

        way.wayCreated += player.OnWayCreated;
        way.wayChanged += player.OnWayChanged;

        score.ScoreReachWayChangeValue += way.OnScoreReachWayChangeValue;
        score.ScoreReachWayChangeValue += battery.OnScoreReachWayChangeValue;

        player.playerDie += this.OnPlayerDie;
        player.playerFacedProjectile += way.OnPlayerFacedProjectile;
        player.playerPickUpCoin += dataManager.OnPlayerPickUpCoin;

        gameDataLoaded += player.OnGameDataLoaded;
        gameDataLoaded += battery.OnGameDataLoaded;
    }

    //������ ����
    private void Start()
    {
        gameDataLoaded.Invoke(
            GameDataUpgradeEncoder.EncodeUpgradeEffects(
                dataManager.GetAllInfoEffects()));

        Vector2 upPoint = new Vector2(platform1.transform.position.x,
            platform1.transform.position.y - platform1.transform.localScale.y / 2);
        Vector2 downPoint = new Vector2(platform2.transform.position.x,
            platform2.transform.position.y + (platform2.transform.localScale.y / 2));

        platform1.Active = true;;

        way.CreateWay(upPoint, downPoint);
    }

    //����������� �������_________________________________________________
    //��������� ������� - ����� �������� �������� ���������
    private void OnPlayerHitPlatform(Player player, Platform platform)
    {
        platform.Active = false;
        if(platform == platform1)
            platform2.Active = true;
        else 
            platform1.Active = true;
    }

    //��������� ������� - ����� ����
    private void OnPlayerDie()
    {
        Debug.Log("����� �����");
        //��� ��������� ����
    }
    //________________________________________________________________

}
