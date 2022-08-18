using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    void LateUpdate()
    {
        Vector2 pos = player.transform.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
