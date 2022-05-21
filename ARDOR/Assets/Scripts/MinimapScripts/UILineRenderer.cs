using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    public float thickness = 10f;

    private List<Vector2> points;
    float width;
    float height;

    public List<Vector2> Points { get { return points; }
        set { 
            points = value;
            Debug.Log("SetVerticesDirty YEEPEE");

            SetVerticesDirty();
            } 
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Debug.Log("OnPopulateMesh YEEPEE");
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;


        if(Points.Count < 2)
        {
            return;
        }
        float angle = 0;
        for (int i = 0; i < Points.Count-1; i++)
        {
            Vector2 point = Points[i];
            Vector2 nextPoint = Points[i+1];
            angle = 0;
            if (i < Points.Count - 1)
            {
                // add 270 to convert normal x-y cartesian angle to z-oriented angle
                angle =270+ GetAngle(Points[i], Points[i + 1]);
            }
            DrawVerticesForPoint(point, nextPoint, vh, angle);
        }

       for(int i = 0;i< Points.Count - 1; i++)
        {
            int index = i * 4;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }


    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    private void DrawVerticesForPoint(Vector2 point,Vector2 nextPoint, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0,0,angle)* new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3( point.x, point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(nextPoint.x, nextPoint.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(nextPoint.x, nextPoint.y);
        vh.AddVert(vertex);
    }


}
