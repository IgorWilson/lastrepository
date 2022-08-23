using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsMovement : MonoBehaviour
{
    public Transform position;

    private void LateUpdate()
    {
        transform.position = position.position;
    }
}
