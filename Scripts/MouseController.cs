using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseController
{

    /// <summary>
    /// Mouse Position
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Direction from the main camera to the mouse position.
    /// </summary>
    public static Vector2 GetTargetpoint()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(5);
    }

    /// <summary>
    /// Direction to the mouse position from a specific point.
    /// </summary>
    public static Vector2 GetDirectionToMouse(Vector2 from)
    {
        return GetDirection(from, GetMousePosition());
    }

    /// <summary>
    /// Returns the direction between two points.
    /// </summary>
    public static Vector2 GetDirection(Vector2 from, Vector2 to)
    {
        return (to - from).normalized;
    }

    /// <summary>
    /// Returns the point to the mouse position, but limited in distance.
    /// </summary>
    public static Vector2 GetDistanceMousePoint(Vector2 from, float distance = 5)
    {
        if ((GetMousePosition() - from).magnitude < distance)
        {
            return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - from;// Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            return GetDirectionToMouse(from) * distance;
        }

    }

}
