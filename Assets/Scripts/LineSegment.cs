using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSegment : MonoBehaviour
{
    public List<LineSegment> nextSegments = new List<LineSegment>(); // Список ссылок на следующие сегменты линии
    public Color lineColor = Color.white; // Параметр цвета линии

    private void OnDrawGizmos()
    {
        // Отрисовка линий
        foreach (LineSegment nextSegment in nextSegments)
        {
            if (nextSegment != null)
            {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(transform.position, nextSegment.transform.position);
            }
        }

        // Отрисовка прозрачного wire cube на текущей позиции
        Gizmos.color = new Color(lineColor.r, lineColor.g, lineColor.b, 0.5f);
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
