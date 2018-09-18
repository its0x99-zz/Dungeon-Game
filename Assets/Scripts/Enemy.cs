using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool isChasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    //private BoxCollider2D hitbox;
    public ContactFilter2D filter;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        //hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // check if the player in range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                isChasing = true;
            }

            if (isChasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMovement((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMovement(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMovement(startingPosition - transform.position);
            isChasing = false;
        }

        // check for enemy colliding with player
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Figher" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }
            hits[i] = null;
        }

    }

    protected override void Death()
    {
        Destroy(gameObject);
    } 

}
