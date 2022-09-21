using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Bezier 
{
    private Vector2 upPoint;
    private Vector2 downPoint;

    // ������ ��� ��������� ������
    private LineRenderer lineRenderer;

    private int fragment = 1000;
    private int fragmentNormalize = 1000;

    // ������ ����� �� ������� �������� ������ ����� 
    private List<Vector2> pointsBezier = new List<Vector2>();

    public Bezier(Vector2 upPoint, Vector2 downPoint, LineRenderer lineRenderer)
    {
        this.upPoint = upPoint;
        this.downPoint = downPoint;
        this.lineRenderer = lineRenderer;
    }

    public void AddPointBezier(Vector2 position)
    {
        // ��������� ����� ����� �� ������������� �����
        pointsBezier.Add(position);
    }

    public Dictionary<int, Vector2> UpdateCurve()
    {
        Dictionary<float, Vector2> way = CalculateBezier();

        RenderLine(way);

        return CalculateNormalizeBezier(way);
    }

    public void ReplaceRandomBezierPoint(Vector2 newPos)
    {
        pointsBezier[UnityEngine.Random.Range(0, pointsBezier.Count)] = newPos;
    }

    public void SortBezierPoints()
    {
        for (int i = 0; i < pointsBezier.Count; i++)
        {
            for (int j = i + 1; j < pointsBezier.Count; j++)
            {
                if(pointsBezier[i].y > pointsBezier[j].y)
                {
                    Vector2 tmp = pointsBezier[i];
                    pointsBezier[i] = pointsBezier[j];
                    pointsBezier[j] = tmp;
                }
            }
        }
    }

    private Dictionary<float, Vector2> CalculateBezier()
    {
        // ������������ ������ ����� ��� ��������� ����� � ��� ���������� �������� � way

        // �������� ������� "����"
        Dictionary<float, Vector2> way = new Dictionary<float, Vector2>();

        List<Vector2> points = new List<Vector2>();

        points.Add(downPoint);
 
        for (int i = 0; i < pointsBezier.Count; i++)
        {
            points.Add(pointsBezier[i]);
        }

        points.Add(upPoint);


        // ������ ����������� �� ��������� ������� � �������������� ���������� ��� �������
        for (int i = 0; i <= fragment; i++)
        {
            float t = (1f / fragment) * i;
            CalcBezier(t, points, way);
        }

        return way;
    }

    private Vector3 CalcBezier(float t, List<Vector2> templateCurveBezier, Dictionary<float, Vector2> way)
    {
        // ����������� ������ �����

        if (templateCurveBezier.Count < 2)
        {
            way[t] = templateCurveBezier[0];
            return templateCurveBezier[0];
        }

        List<Vector2> newTemplateCurveBezier = new List<Vector2>();

        for (int i = 0; i < templateCurveBezier.Count - 1; i++)
        {
            Vector2 midlePoints = (1 - t) * templateCurveBezier[i] + t * templateCurveBezier[i + 1];
            newTemplateCurveBezier.Add(midlePoints);
        }

        return CalcBezier(t, newTemplateCurveBezier, way);
    }

    private Vector2 BinareSearchNearestValue(float progress, List<Tuple<float, Vector2>> PairArray)
    {
        // �������� �����, ������� ���������� ��������� ������ � ����������� �� ��������� 

        int left = 0;
        int right = PairArray.Count - 1;

        int middle = 0;

        while (true)
        {
            middle = left + (right - left) / 2;

            if (left == right) return PairArray[left].Item2;

            if ((right - left) == 1)
            {
                float lerpStep = ((progress - left) * 100) / (right - left);
                return Vector2.Lerp(PairArray[left].Item2, PairArray[right].Item2, lerpStep);
            }

            if (PairArray[middle].Item1 > progress)
            {
                right = middle;
            }
            else if (PairArray[middle].Item1 < progress)
            {
                left = middle;
            }
            else
            {
                return PairArray[middle].Item2;
            }
        }

    }

    private void RenderLine(Dictionary<float, Vector2> way)
    {
        // ������������ "������" �� ������
        lineRenderer.positionCount = way.Count;
        int counter = 0;

        foreach (var item in way)
        {
            lineRenderer.SetPosition(counter, item.Value);
            counter += 1;
        }
    }

    private Dictionary<int, Vector2> CalculateNormalizeBezier(Dictionary<float, Vector2> way)
    {
        // ������������� ������ ����� ���, ����� ��� ���� �����������

        Dictionary<int, Vector2> wayNormalize = new Dictionary<int, Vector2>();

        // ������ ��� "����� �� ������ ���������" - "������� ����������"
        List<Tuple<float, Vector2>> cumulativeLenght = new List<Tuple<float, Vector2>>()
        {
            new Tuple<float, Vector2>(0f, new Vector2())
        };
    
        // ����� �����
        float totlaLenght = 0;

        // ���������� �����
        Vector2 prevPoint = new Vector2();

        // � ����� ����� ����� ���������� �����, ������� ������ ��������� ����� ���������� 
        bool flag = false;

        // ������������ ������������� ����� 
        foreach (var item in way)
        {
            if (flag == true)
            {
                float dist = Vector2.Distance(item.Value, prevPoint);
                cumulativeLenght.Add(
                    new Tuple<float, Vector2>(cumulativeLenght[cumulativeLenght.Count - 1].Item1 + dist, item.Value)
                    );
                totlaLenght += dist;
            }
            else 
            { 
                //cumulativeLenght.Add(new Tuple<float, Vector2>(0,item.Value));
                flag = true; 
            }
            
            prevPoint = item.Value;
        }

        // ������� �����
        float averageLenght = totlaLenght / fragmentNormalize;

        // ��������� ������� ��� ����
        for (int i = 1; i < fragmentNormalize; i++)
        {
            wayNormalize.Add(i, BinareSearchNearestValue(i * averageLenght, cumulativeLenght));
        }

        // ������� �������� ����������
        cumulativeLenght.Clear();
        way.Clear();

        return wayNormalize;
    }
}

