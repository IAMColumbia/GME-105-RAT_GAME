using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShooting : MonoBehaviour
{
    /// <summary>
    ///                spriteTransform - transforms the sprite.
    ///                lastInput - checks for the last key pressed.
    /// </summary>
    private Transform spriteTransform;
    private Vector2 lastInput;

    void Start()
    {
        spriteTransform = transform;
    }

    void Update()
    {
        // gets horizontal and vertical buttons.
        Vector2 inputVector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        // if the player doesn't press a key, then just use the last key.
        if (inputVector != Vector2.zero)
            lastInput = inputVector;

        if (lastInput == Vector2.zero)
            return;
        // shifts and rotates sprite 45 degrees to point at last direction.
        float targetAngle = Mathf.Atan2(lastInput.y, lastInput.x) * Mathf.Rad2Deg;
        float snappedAngle = Mathf.Round(targetAngle / 45f) * 45f;

        spriteTransform.rotation = Quaternion.Euler(0, 0, snappedAngle);
    }
}

