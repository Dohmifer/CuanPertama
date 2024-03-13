using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public enum ShipDirection { Neutral, AHead, Astern }
    public enum ShipSpeed { Neutral, DeadSlow, Slow , Half, Full }

    public ShipDirection shipDirection = ShipDirection.Neutral;
    public ShipSpeed shipSpeed = ShipSpeed.Neutral;

    public bool shipEngine = false;
    public float maxSpeed = 1f;

    private float targetAngle;
    private float currentAngle;
    private float rotationSpeed = 1f;

    private Rigidbody2D rb2d;
    private Animator animator;
    private Coroutine rotationCoroutine;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        ShipUpdateSpeed();
    }

    void FixedUpdate()
    {
        Vector2 engineForceVector = transform.up * maxSpeed;
        rb2d.AddForce(engineForceVector, ForceMode2D.Force);

        // rotationAngle -= movement.x * turnFactor;
        // rb2d.MoveRotation(rotationAngle);
    }

    public void ShipUpdateSpeed()
    {
        if (!shipEngine)
        {
            StopEngine();
        }

        float speedMultiplier = 0f;
        switch (shipSpeed)
        {
            case ShipSpeed.Full:
                speedMultiplier = 2f;
                break;
            case ShipSpeed.Half:
                speedMultiplier = 1f;
                break;
            case ShipSpeed.Slow:
                speedMultiplier = 0.5f;
                break;
            case ShipSpeed.DeadSlow:
                speedMultiplier = 0.25f;
                break;
        }

        maxSpeed = (shipDirection == ShipDirection.AHead) ? speedMultiplier : -speedMultiplier;
    }

    public void SetShipRotation(float angle)
    {
        targetAngle += angle;
        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
        }
        rotationCoroutine = StartCoroutine(RotateShip());
    }

    private IEnumerator RotateShip()
    {
        float elapsedTime = 0f;
        float startAngle = transform.rotation.eulerAngles.z;
        
        while (elapsedTime < 1f)
        {
            currentAngle = Mathf.LerpAngle(startAngle, targetAngle, elapsedTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    }

    public void FinishWithEngine()
    {
        shipEngine = false;
    }

    public void StopEngine()
    {
        maxSpeed = 0f;
        shipDirection = ShipDirection.Neutral;
        shipSpeed = ShipSpeed.Neutral;
    }

    public void StandByEngine()
    {
        shipEngine = true;
    }

    public void AHeadDeadSlow()
    {
        shipDirection = ShipDirection.AHead;
        shipSpeed = ShipSpeed.DeadSlow;
    }

    public void AHeadSlow()
    {
        shipDirection = ShipDirection.AHead;
        shipSpeed = ShipSpeed.Slow;
    }

    public void AHeadHalf()
    {
        shipDirection = ShipDirection.AHead;
        shipSpeed = ShipSpeed.Half;
    }

    public void AHeadFull()
    {
        shipDirection = ShipDirection.AHead;
        shipSpeed = ShipSpeed.Full;
    }

    public void AsternDeadSlow()
    {
        shipDirection = ShipDirection.Astern;
        shipSpeed = ShipSpeed.DeadSlow;
    }

    public void AsternSlow()
    {
        shipDirection = ShipDirection.Astern;
        shipSpeed = ShipSpeed.Slow;
    }

    public void AsternHalf()
    {
        shipDirection = ShipDirection.Astern;
        shipSpeed = ShipSpeed.Half;
    }

    public void AsternFull()
    {
        shipDirection = ShipDirection.Astern;
        shipSpeed = ShipSpeed.Full;
    }
}
