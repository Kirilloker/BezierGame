using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Player player;
    //Все точки пути
    private Dictionary<int, Vector2> wayPoints;
    //параметр, отвечающий за перемещение по кривой, использовать только через свойство!
    private int sigma = 1;
    //Длинна пути
    private int wayLenght = 0;

    //Параметры перемещения
    private float maxSpeedAbs;
    private float currentSpeed;
    private float acceleration;
    private float slowdown;

    private void Awake()
    {
        player = GetComponent<Player>();
        SetSpeed(player.PlayerSpeed);
    }

    //Управление игроком
    private void FixedUpdate()
    {
        Sigma += (int)currentSpeed;
        player.transform.position = wayPoints[Sigma];


        if (Input.GetKey(KeyCode.W))
        {
            AcceleratePlayer(1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            AcceleratePlayer(-1);
        }

        SlowdownPlayer();

        //    if (Input.GetMouseButton(0))
        //{
        //    //CreatePointBezier(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //    Vector3 vectorTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    if (vectorTouch.x < 0)
        //        
        //    else
        //        
        //}
    }

    private void SlowdownPlayer()
    {
        //Если игрок движется в положительном направлении
        if (currentSpeed > 0f)
        {
            //Замедляем его
            currentSpeed -= slowdown;

            //Но не изменяем направление
            if (currentSpeed < 0f)
                currentSpeed = 0f;
        }

        //Если игрок движется в отрицательном направлении
        if (currentSpeed < 0f)
        {
            //Замедляем его
            currentSpeed += slowdown;

            //Но не изменяем направление
            if (currentSpeed > 0f)
                currentSpeed = 0f;
        }
    }
    private void AcceleratePlayer(int direction)
    {
        direction = direction / Math.Abs(direction);

        //Если скорость не достигла максимальной
        if (Math.Abs(currentSpeed) < maxSpeedAbs)
        {
            //Увеличиваем скорость
            currentSpeed += acceleration * direction;

            //Скорость не может быть выше максимальной
            if (Math.Abs(currentSpeed) > maxSpeedAbs)
                currentSpeed = maxSpeedAbs * direction;
        }
    }
    public void SetSpeed(float value)
    {
        maxSpeedAbs = value;
        acceleration = maxSpeedAbs * 0.2f;
        slowdown = maxSpeedAbs * 0.1f;
    }
    public void SetWayPoints(Dictionary<int, Vector2> wayPoints)
    {
        this.wayPoints = wayPoints;
        wayLenght = wayPoints.Count;
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