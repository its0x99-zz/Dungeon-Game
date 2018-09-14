using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public float[] fireBallSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public Transform[] fireBalls;
    public GameObject bossBarrier;

    private void Update()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            fireBalls[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireBallSpeed[i]) * distance, Mathf.Sin(Time.time * fireBallSpeed[i]) * distance, 0);
        }
    }

    protected override void Death()
    {
        base.Death();

        // open the boss barrier when he dies
        bossBarrier.SetActive(false);
    }

}
