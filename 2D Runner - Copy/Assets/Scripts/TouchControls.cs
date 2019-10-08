using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    public void Shoot()
    {
        thePlayer.shootAsteroid();
    }
}
