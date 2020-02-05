using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talk : MonoBehaviour
{
    public Transform player;
    public BasicInkExample BInk;
    public Animator anim;
    public GameObject talkToMe;
    public bool hasTalked = false;
    private bool talkable = false;
    private bool nocombat;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            talkable = true;
            talkToMe.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkable = false;
            talkToMe.SetActive(false);
        }
    }


    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            nocombat = false;
        }
        else
        {
            nocombat = true;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))
        { 
            if (talkable && nocombat)
            {
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                direction.Normalize();
                anim.SetBool("Animate", false);
                if ((direction.x > -0.5 && direction.x < 0.5) && (direction.y < -0.5))
                {
                    anim.SetTrigger("Down");
                }
                else if ((direction.x > -0.5 && direction.x < 0.5) && (direction.y > 0.5))
                {
                    anim.SetTrigger("Up");
                }
                else if ((direction.y >= -0.5 && direction.y <= 0.5) && (direction.x >= 0.5))
                {
                    anim.SetTrigger("Right");
                }
                else if ((direction.y >= -0.5 && direction.y <= 0.5) && (direction.x <= -0.5))
                {
                    anim.SetTrigger("Left");
                }
                StartStory();
            }
        }
    }



    void StartStory()
    {
        BInk.StartStory(this.gameObject);
    }
}
