using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProgressBar Pb;
    public GameObject player;
    Rigidbody2D body;
    private Vector3 offset;
    private bool attackMode = true;
    float horizontal;
    float vertical;
    public float runSpeed = 20.0f;
    public float maxX = 5f;
    public float minX = -5f;
    public float maxY = 5f;
    public float minY = -5f;
    public Animator playerAnimator;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        offset = transform.position - player.transform.position;
        playerAnimator = GetComponent<Animator>();
    }

    private void TakeDamage(float damage)
    {
        Pb.BarValue -= damage;
    }

    private void HealDamage(float damage)
    {
        Pb.BarValue += damage;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        renderer.sortingOrder = -(int)(renderer.transform.position.y * 32);

        //limit walk
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

        //dabing animation VERY IMPORTANT NEVER DELETE
        playerAnimator.SetBool("Dab", Input.GetButton("Dab"));

        //attack
        if (attackMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetTrigger("Attack");
            }
        }
    }
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }

    void FixedUpdate()
    {
        //change animation parameters
        if (horizontal != 0)
        {
            vertical = 0;       //can only move 1 direction at once. For now horizontal movement takes precedence
            if (horizontal > 0)
            {
                playerAnimator.SetBool("Right", true);

                playerAnimator.SetBool("Up", false);
                playerAnimator.SetBool("Down", false);
                playerAnimator.SetBool("Left", false);
            }
            else
            {
                playerAnimator.SetBool("Left", true);

                playerAnimator.SetBool("Up", false);
                playerAnimator.SetBool("Down", false);
                playerAnimator.SetBool("Right", false);
            }
        }
        else if (vertical != 0)
        {
            
            if (vertical > 0) {
                playerAnimator.SetBool("Up", true);

                playerAnimator.SetBool("Down", false);
                playerAnimator.SetBool("Left", false);
                playerAnimator.SetBool("Right", false);
            } else {
                playerAnimator.SetBool("Down", true);

                playerAnimator.SetBool("Up", false);
                playerAnimator.SetBool("Left", false);
                playerAnimator.SetBool("Right", false);
            }
        }
        else
        {
            playerAnimator.SetBool("Up", false);
            playerAnimator.SetBool("Down", false);
            playerAnimator.SetBool("Left", false);
            playerAnimator.SetBool("Right", false);
        }


        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

    }
}
