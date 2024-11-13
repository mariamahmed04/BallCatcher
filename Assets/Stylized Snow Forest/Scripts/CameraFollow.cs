using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Drag the player object here in the Inspector
    public Vector3 offset;    // Offset to maintain relative distance

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update camera position to follow player
        transform.position = player.position + offset;
    }
}
