using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extension
{
    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public static Quaternion RotateToDirection(this Vector2 v)
    {
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Vector2 RandomVectorInRadius(this Vector2 pos, float rand)
    {
        float x, y;

        if (rand > 0)
        {
            x = Random.Range(pos.x - rand / 2, pos.x + rand / 2);
            y = Random.Range(pos.y - rand / 2, pos.y + rand / 2);
        }
        else
        {
            x = pos.x;
            y = pos.y;
        }

        return new Vector2(x, y);
    }

}
