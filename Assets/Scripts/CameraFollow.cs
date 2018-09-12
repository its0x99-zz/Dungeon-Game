using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform lookAt;        // player object
    public float boundX = 0.15f;    // distance in X before camera is adjusted
    public float boundY = 0.05f;    // distance in Y before camera is adjusted

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // check bounds on the X axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            } else
            {
                delta.x = deltaX + boundX;
            }
        }

        // check bounds on the Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        // update the position of the camera to follow the player
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
