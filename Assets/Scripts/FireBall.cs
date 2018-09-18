using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // References & Configuration Settings
    public float speed = 20f;
    public Rigidbody2D rb;
    public int weaponDamage = 1;
    public AudioClip soundEffect;
    public AudioClip hitSound;
	public float coolDown = 1;
	public float coolDownTimer;

    private AudioSource audioSource;

    void Start()
    {
        // Rotate the projectile depending on which direction the player is facing
        if (!GameManager.instance.player.isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
        }

        // Add velocity to the projectile
        rb.velocity = transform.right * speed;


        // Play weapon sound effect
        audioSource = GameManager.instance.GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundEffect);
    }

	// Check for collisions with objects
	void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "Player")
            return;
        
        // Check if we hit an enemy
        Fighter enemy = hitInfo.GetComponent<Fighter>();
        if (enemy != null)
        {

            weaponDamage = 1;
            if (GameManager.instance.selectedWeapon == 1)
            {
                weaponDamage = 10;
            }

            Damage dmg = new Damage
            {
                damageAmount = weaponDamage,
                origin = transform.position,
                pushForce = 2.0f
            };
            enemy.SendMessage("ReceiveDamage", dmg);
            audioSource.PlayOneShot(hitSound);
            Destroy(gameObject);
        }

        // Check if we hit something else
        if (hitInfo.name == "Collision")
            Destroy(gameObject);
    }

    // Destory the projectile if it leaves the camera's view
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
