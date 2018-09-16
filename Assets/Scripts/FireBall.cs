using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        if (!GameManager.instance.player.isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
        }
        rb.velocity = transform.right * speed;
    }
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Player")
            return;
        
        Fighter enemy = hitInfo.GetComponent<Fighter>();
        if (enemy != null)
        {
            Damage dmg = new Damage
            {
                damageAmount = 1,
                origin = transform.position,
                pushForce = 2.0f
            };
            enemy.SendMessage("ReceiveDamage", dmg);
        }
        Destroy(gameObject);
    }


}
