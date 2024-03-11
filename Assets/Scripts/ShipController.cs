using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float speedAcceleration = 30f;
    public float turnFactor = 3.5f;

    float rotationAngle = 0f;

    private Rigidbody2D rb2d;
    Vector2 movement;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Vector2.zero;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 engineForceVector = transform.up * movement.y * speedAcceleration;
        rb2d.AddForce(engineForceVector, ForceMode2D.Force);

        rotationAngle -= movement.x * turnFactor;
        rb2d.MoveRotation(rotationAngle);
    }
}
