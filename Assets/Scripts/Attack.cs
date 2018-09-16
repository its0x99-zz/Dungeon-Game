using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {


    public Transform firePoint;
    public GameObject[] projectilePrefabs;

	void Update () {
		if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
