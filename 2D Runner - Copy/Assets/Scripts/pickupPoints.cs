using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPoints : MonoBehaviour
{
    public int scoreToGive;
    private ScoreManager theScoreManager;

    private AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();

        coinSound = GameObject.Find("Collect Coin Audio").GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            theScoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);

            if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }

            
        }
    }
}
