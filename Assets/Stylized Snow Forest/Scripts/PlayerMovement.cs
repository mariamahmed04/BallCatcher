using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents player rotation due to physics
    }

    void Update()
    {
        // Get WASD input
        float moveHorizontal = Input.GetAxisRaw("Vertical"); // A, D
        float moveVertical = Input.GetAxisRaw("Horizontal"); // W, S

        // Create direction vector relative to player's forward direction
        Vector3 direction = transform.forward * moveVertical + transform.right * moveHorizontal;

        // Apply movement
        rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);
    }
}
