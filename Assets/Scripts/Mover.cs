using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected Vector3 originalSize;
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;

    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    public bool isFacingRight = true;

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMovement(Vector3 input)
    {
        // reset move delta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        // flip the sprite depending on players direction
        if (moveDelta.x > 0)
        {
            transform.localScale = originalSize;
            isFacingRight = true;            
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
            isFacingRight = false;
        }

        // add push vector if any
        moveDelta += pushDirection;

        // reduce push force every update until it reaches zero based on the recovery speed values
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // check if the player can move in the new direction by casting a box there first, if the box return null its clear to move there
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // move the player y position
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // move the player x position
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
