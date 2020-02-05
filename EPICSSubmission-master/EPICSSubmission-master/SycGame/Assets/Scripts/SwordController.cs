using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject player;
    public GameObject grassOb;
    public Animator playerAnimator;
    public AudioClip Swing;
    public AudioClip Ignite;
    private float swordDamage = 4f; //sword damage on hit (normal is 2)
    private float knockBack = 10f; //decides knockback strength (normal 10)
    private float fireEffect = 0f; //decides bool and length in half seconds (best 5)
    private float fireDPS = 0.1f; //decides fire DP half seconds
    private float iceEffect = 0f;//decides bool and length (best 3)
    public bool attacking = false;
    private bool regen = false;
    private bool grass = false;
    GameObject[] grassNum;
    GameObject[] enemyNum;
    Animator swordAnimator;
    public GameObject WeaponSelection;
    private string currentWeapon = "NoPower";
    private string previousWeapon;
    private bool swinging = true;

    void Start()
    {
        swordAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //sound
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingUp") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingDown") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingLeft") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingRight"))
        {
            if (swinging)
            {
            GetComponent<AudioSource>().clip = Swing;
            GetComponent<AudioSource>().Play();
                swinging = false;
            }
        }
        else
        {
            swinging = true;
        }

        enemyNum = GameObject.FindGameObjectsWithTag("Enemy");
        grassNum = GameObject.FindGameObjectsWithTag("grass");
        if (Input.GetKeyDown(KeyCode.Space) && grass)
        {
            if (grassNum.Length <= 3)
            {
                if(grassNum.Length == 3)
                {
                    Destroy(grassNum[0]);
                }
                float xV = player.transform.position.x;
                float yV = player.transform.position.y;
                Instantiate(grassOb, new Vector3(xV, yV, 1279.769f), Quaternion.identity);
            }
        }
        if(enemyNum.Length == 0 && grassNum.Length > 0)
        {
            for(int i = 0; i< grassNum.Length; i++)
            {
                Destroy(grassNum[i]);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingUp") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingDown") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingLeft") || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwingRight"))
        {
            if (collision.gameObject.tag == "Enemy")
            {
                float[] sendEnemy = { swordDamage, knockBack ,fireEffect, fireDPS, iceEffect};
                collision.gameObject.SendMessage("TakeDamage",sendEnemy);
                if (regen)
                {
                    player.SendMessage("HealDamage", swordDamage - (0.5*swordDamage));
                }
            }
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < WeaponSelection.transform.childCount; i++)
        {
            var child = WeaponSelection.transform.GetChild(i).gameObject;
            if (child.activeSelf)
            {
                previousWeapon = currentWeapon;
                 currentWeapon = child.name;
                if((currentWeapon != previousWeapon) && (currentWeapon != "NoPower"))
                {
                    GetComponent<AudioSource>().clip = Ignite;
                    GetComponent<AudioSource>().Play();
                }
            }
        }

        if (currentWeapon =="NoPower")
        {
            swordAnimator.SetBool("Air", false);
            swordAnimator.SetBool("Water", false);
            swordAnimator.SetBool("Fire", false);
            swordAnimator.SetBool("Earth", false);
            swordAnimator.SetBool("Life", false);
            swordAnimator.SetBool("Nova", false);
            swordDamage = 5f; //sword damage on hit (normal is 2)
            knockBack = 10f; //decides knockback strength (normal 10)
            fireEffect = 0f; //decides bool and length in half seconds (best 5)
            iceEffect = 0f;//decides bool and length (best 3)
            grass = false;
            regen = false;

        }
        if (currentWeapon == "WaterPower")
        {
            swordAnimator.SetBool("Air", false);
            swordAnimator.SetBool("Water", true);
            swordAnimator.SetBool("Fire", false);
            swordAnimator.SetBool("Earth", false);
            swordAnimator.SetBool("Life", false);
            swordAnimator.SetBool("Nova", false);
            swordDamage = 1f; //sword damage on hit (normal is 2)
            knockBack = 0f; //decides knockback strength (normal 10)
            fireEffect = 0f; //decides bool and length in half seconds (best 5)
            iceEffect = 4f;//decides bool and length (best 3)
            grass = false;
            regen = false;
        }
        if (currentWeapon == "AirPower")
        {
            swordAnimator.SetBool("Air", true);
            swordAnimator.SetBool("Water", false);
            swordAnimator.SetBool("Fire", false);
            swordAnimator.SetBool("Earth", false);
            swordAnimator.SetBool("Life", false);
            swordAnimator.SetBool("Nova", false);
            swordDamage = 9f; //sword damage on hit (normal is 2)
            knockBack = 60f; //decides knockback strength (normal 10)
            fireEffect = 0f; //decides bool and length in half seconds (best 5)
            iceEffect = 0f;//decides bool and length (best 3)
            grass = false;
            regen = false;
        }
        if (currentWeapon == "FirePower")
        {
            swordAnimator.SetBool("Air", false);
            swordAnimator.SetBool("Water", false);
            swordAnimator.SetBool("Fire", true);
            swordAnimator.SetBool("Earth", false);
            swordAnimator.SetBool("Life", false);
            swordAnimator.SetBool("Nova", false);
            swordDamage = 9f; //sword damage on hit (normal is 2)
            knockBack = 10f; //decides knockback strength (normal 10)
            fireEffect = 7f; //decides bool and length in half seconds (best 5)
            iceEffect = 0f;//decides bool and length (best 3)
            grass = false;
            regen = false;
        }
        if (currentWeapon == "EarthPower")
        {
            swordAnimator.SetBool("Air", false);
            swordAnimator.SetBool("Water", false);
            swordAnimator.SetBool("Fire", false);
            swordAnimator.SetBool("Earth", true);
            swordAnimator.SetBool("Life", false);
            swordAnimator.SetBool("Nova", false);
            swordDamage = 5; //sword damage on hit (normal is 2)
            knockBack = 10; //decides knockback strength (normal 10)
            fireEffect = 0f; //decides bool and length in half seconds (best 5)
            iceEffect = 0f;//decides bool and length (best 3)
            grass = true;
            regen = false;
        }
        if (currentWeapon == "LifePower")
        {
            swordAnimator.SetBool("Air", false);
            swordAnimator.SetBool("Water", false);
            swordAnimator.SetBool("Fire", false);
            swordAnimator.SetBool("Earth", false);
            swordAnimator.SetBool("Life", true);
            swordAnimator.SetBool("Nova", false);
            swordDamage = 8; //sword damage on hit (normal is 2)
            knockBack = 10; //decides knockback strength (normal 10)
            fireEffect = 0f; //decides bool and length in half seconds (best 5)
            iceEffect = 0f;//decides bool and length (best 3)
            grass = false;
            regen = true;
        }
        if (currentWeapon == "NovaPower")
        {
            swordAnimator.SetBool("Air", false);
            swordAnimator.SetBool("Water", false);
            swordAnimator.SetBool("Fire", false);
            swordAnimator.SetBool("Earth", false);
            swordAnimator.SetBool("Life", false);
            swordAnimator.SetBool("Nova", true);
            swordDamage = 999; //sword damage on hit (normal is 2)
            knockBack = 0; //decides knockback strength (normal 10)
            fireEffect = 0f; //decides bool and length in half seconds (best 5)
            iceEffect = 0f;//decides bool and length (best 3)
            grass = false;
            regen = false;
        }
    }
}

