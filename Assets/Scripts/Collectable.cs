using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable {

    protected bool collected;
    protected override void OnCollide(Collider2D coll)
    {
        // only player should collide with object to collect it, ignore other collisions
        if (coll.name == "Player")
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
