using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    public float speed;
    private float lifeTime = 4;
    public int scoreToGive;
    private ScoreManager theScoreManager;
    private AudioSource coinSound;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("Collect Coin Audio").GetComponent<AudioSource>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            theScoreManager.AddScore(scoreToGive);
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

        Destroy(gameObject);
    }
}
