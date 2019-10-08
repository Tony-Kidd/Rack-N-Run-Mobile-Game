using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{

    public bool doublePoints;
    public bool safeMode;

    public float powerupLength;

    private PowerupManager thePowerupManager;

    public Sprite[] powerupSprites;


    // Start is called before the first frame update
    void Start()
    {
        thePowerupManager = FindObjectOfType<PowerupManager>();
    }

    private void Awake()
    {
        int powerupSelector = Random.Range(0, 2);

        switch (powerupSelector)
        {
            case 0: doublePoints = true;
                break;
            case 1: safeMode = true;
                break;
        }

        GetComponent<SpriteRenderer>().sprite = powerupSprites[powerupSelector];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            thePowerupManager.ActivatePowerup(doublePoints, safeMode, powerupLength);
        }

        gameObject.SetActive(false);
    }
}
