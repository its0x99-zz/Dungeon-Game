using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{

    // Fireball settings
    public float[] fireBallSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public Transform[] fireBalls;

    // References to other game objects
    public GameObject bossBarrier;
    public Image healthBar;

    private void Update()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            fireBalls[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireBallSpeed[i]) * distance, Mathf.Sin(Time.time * fireBallSpeed[i]) * distance, 0);
        }
    }

    // Updates the boss health bar
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        healthBar.fillAmount = (float)hitPoint / (float)maxHitpoint;
    }

    // Killing the boss opens the barrier
    protected override void Death()
    {
        base.Death();
        bossBarrier.SetActive(false);
    }

    protected override void UpdateMovement(Vector3 input)
    {
        base.UpdateMovement(input);

        // prevent the healthbar from flipping when facing opposite direction
        if (transform.localScale.x != originalSize.x)
        {
            healthBar.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
        else
        {
            healthBar.fillOrigin = (int)Image.OriginHorizontal.Right;
        }

    }

}
