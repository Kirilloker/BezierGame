using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    RandomBezierPointSpawner pointSpawner;
    
    private Bezier curve;
    private Vector2 upPoint;
    private Vector2 downPoint;

    //Сколько раз уже поменялся путь
    private int difficult = 0;
    //Максимальное количесто добавленных точек
    private int maxBezierPoints = 12;
    //Если сложность кратна этому числу - добавляем новую точку
    private int addNewPointDiffValue = 3;

    //Ивент создания пути
    public delegate void WayCreated(Dictionary<int, Vector2> wayPoints);
    public event WayCreated wayCreated;

    //Ивент изменения пути
    public delegate void WayChanged(Dictionary<int, Vector2> wayPoints);
    public event WayCreated wayChanged;

    public void CreateWay(Vector2 upPoint, Vector2 downPoint)
    {
        curve = new Bezier(upPoint, downPoint, lineRenderer);
        this.upPoint = upPoint;
        this.downPoint = downPoint;

        curve.AddPointBezier(pointSpawner.GetRandomVector());

        //Создаем путь и поднимаем ивент создания пути
        wayCreated.Invoke(curve.UpdateCurve());
    }

    public void OnScoreReachWayChangeValue()
    {
        difficult += 1;

        //Добавление новых базовых точек безье
        if((difficult % addNewPointDiffValue == 0) && ((difficult / addNewPointDiffValue) <= maxBezierPoints))
        {
            curve.AddPointBezier(pointSpawner.GetRandomVector());
        }

        //Изменение координат точек - меняем форму кривой
        for (int i = 0; i < 100 /*(3 + (difficult/addNewPointDiffValue))*/; i++)
        {
            curve.ReplaceRandomBezierPoint(pointSpawner.GetRandomVector());
        }

        //Сортировка точек по оси у - избавляемся от петель
        curve.SortBezierPoints();

        for (int i = 0; i < (1 + (difficult / addNewPointDiffValue) / 2); i++)
        {
            if (i > maxBezierPoints / 2)
                break;
            curve.ReplaceRandomBezierPoint(pointSpawner.GetRandomVector());
        }
        wayChanged.Invoke(curve.UpdateCurve());
    }

    public void OnPlayerFacedProjectile(ProjectileEffect effect, float effectValue)
    {
        if(effect == ProjectileEffect.HidePath)
            StartCoroutine(HidePath(effectValue));
    }

    IEnumerator HidePath(float timer)
    {
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(timer);
        lineRenderer.enabled = true;
    }
}