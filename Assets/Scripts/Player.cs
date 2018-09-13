using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnimator.SetTrigger("Show");
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
            UpdateMovement(new Vector3(x, y, 0));
    }

    public void Respawn()
    {
        hitPoint = maxHitpoint;
        GameManager.instance.OnHitPointChange();
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    public void SwapSprite(int selectedCharacter)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[selectedCharacter];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitPoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }

    }
}

