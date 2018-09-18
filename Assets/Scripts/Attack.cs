using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {


    public Transform firePoint;
    public GameObject[] projectilePrefabs;
	public float coolDown = 1;
	public float coolDownTimer;

	void Update () 
		{
		if (coolDownTimer > 0)
		{
			coolDownTimer -= Time.deltaTime;
		}

		if (coolDownTimer < 0)
		{
			coolDownTimer = 0;
		}

		if (Input.GetButtonDown("Jump") && coolDownTimer == 0)
        {
            Shoot();
			coolDownTimer = coolDown;
		}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.instance.OnWeaponChange();
        }
	}

    void Shoot()
    {
        Instantiate(projectilePrefabs[GameManager.instance.selectedWeapon], firePoint.position, firePoint.rotation);
    }
	

}
