using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBezierPointSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject platformUp;
    [SerializeField]
    private GameObject platformDown;
    [SerializeField]
    private Camera sceneCamera;

    private float marginXPercent = 0f;
    private float marginYPercent = 0.05f;

    private Vector2 leftUp = new Vector2();
    private Vector2 rightDown = new Vector2();

    private void Awake()
    {
        CalcCornerPoints();
    }
    private Vector2 GetRandomVectorInRange(Vector2 leftUp, Vector2 rightDown)
    {
        return new Vector2(Random.Range(leftUp.x, rightDown.x), Random.Range(leftUp.y, rightDown.y));
    }

    public Vector2 GetRandomVector()
    {
        return GetRandomVectorInRange(leftUp, rightDown);
    }

    public void CalcCornerPoints()
    {
        Vector2 bottomRight = sceneCamera.ScreenToWorldPoint(new Vector2(sceneCamera.pixelWidth, 0));
        Vector2 topLeft = sceneCamera.ScreenToWorldPoint(new Vector2(0, sceneCamera.pixelHeight));

        float marginX = (bottomRight.x - topLeft.x) * marginXPercent;
        float marginY = (topLeft.y - bottomRight.y) * marginYPercent;

        leftUp = new Vector2(topLeft.x + marginX, platformUp.transform.position.y - platformUp.transform.localScale.y / 2 - marginY);
        rightDown = new Vector2(bottomRight.x - marginX, platformDown.transform.position.y + platformDown.transform.localScale.y / 2 + marginY);
    }
}