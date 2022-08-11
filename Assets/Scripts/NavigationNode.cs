using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationNode
{
    public Vector2 WorldCoordinates { get; set; }

    public NavigationNode(Vector2 point)
    {
        WorldCoordinates = point;
    }
}
