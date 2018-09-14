using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Collectable {

    public GameObject activatedSwitch;
    public GameObject barrier;
    public bool barrierActive = false;
    public bool showMessage = true;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            barrier.SetActive(barrierActive);

            if (activatedSwitch)
            {
                activatedSwitch.SetActive(true);
            }

            if (showMessage)
            {
                GameManager.instance.ShowText("Switch Activated", 25, Color.green, transform.position, Vector3.up * 25, 2.0f);
            }
            
        }
    }
}
