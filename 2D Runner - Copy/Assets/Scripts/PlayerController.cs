using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float movespeed;
    private float moveSpeedStore;
    public float speedMultiplier;

    public float speedIncreaseMilestone;
    public float speedIncreaseMilestoneStore;

    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    public float jumpforce;

    public float jumpTime;
    private float jumpTimeCounter;

    private bool stoppedJumping;
    private bool canDoubleJump;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    //private Collider2D myCollider;

    private Animator myAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;
    public AudioSource shootSound;

    public Transform firePoint;
    public GameObject asteroid;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //myCollider = GetComponent<Collider2D>();

        myAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;

        speedMilestoneCount = speedIncreaseMilestone;

        moveSpeedStore = movespeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

        stoppedJumping = true;

    }

    public void shootAsteroid()
    {
        Instantiate(asteroid, firePoint.position, firePoint.rotation);
        shootSound.Play();
    }




    private void Update()
    {

        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            movespeed = movespeed + speedMultiplier;
        }

        myRigidbody.velocity = new Vector2(movespeed, myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == null) return;
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) return;
            }

            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpforce);
                stoppedJumping = false;
                jumpSound.Play();
            }

            if (!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpforce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == null) return;
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) return;
            }

            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpforce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject == null) return;
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) return;
            }

            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(asteroid, firePoint.position, firePoint.rotation);
            shootSound.Play();
        }

        if (Input.GetButton("Shoot"))
        {
            shootAsteroid();
            shootSound.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            movespeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            deathSound.Play();
        }

        if (other.gameObject.tag == "Enemy")
        {
            theGameManager.RestartGame();
            movespeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            deathSound.Play();
        }
    }
}

    
