using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Player player;

    //¬се точки пути
    private Dictionary<int, Vector2> wayPoints;

    //параметр, отвечающий за перемещение по кривой, использовать только через свойство!
    private int sigma = 1;
    private int wayLenght = 0;
    private float currentSpeed = 0;

    //ѕараметр отвечающий за инерцию движени€, чем он больше, тем меньше инерци€
    private float slowingSpeed = 0.04f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        playerRb.inertia = 1;
    }

    //ѕередвижение игрока
    private void FixedUpdate()
    {

        Sigma += (int)currentSpeed;
        player.transform.position = wayPoints[Sigma];
        if (currentSpeed > 0)
            currentSpeed -= slowingSpeed;
        if (currentSpeed < 0)
            currentSpeed += slowingSpeed;
    }

    public void SetWayPoints(Dictionary<int, Vector2> wayPoints)
    {
        this.wayPoints = wayPoints;
        wayLenght = wayPoints.Count;
    }

    public void MovePlayer(float speed, float inertia)
    {
        slowingSpeed = inertia;
        currentSpeed = speed;
    }

    private int Sigma
    {
        get 
        {
            return sigma;
        }
        set
        {
            if (value >= wayLenght)
            {
                sigma = wayLenght;
            }
            else if (value <= 0)
            {
                sigma = 1;
            }
            else sigma = value;
        }
    }
}
