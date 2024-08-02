using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 Position)
    {
        Position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(Position);
    }
}
