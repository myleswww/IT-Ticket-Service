using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ProgressBar Pb;
    private Transform player;
    private PlayerController bounds;
    public Animator anim;
    private GameObject pauseMenu;
    private PauseMenu pausedmenumenu;
    private bool paused = false;
    public GameObject fire;
    public GameObject ice;
    public AudioClip[] normal;
    public AudioClip[] attacking;
    public AudioClip[] damageAudio;
    private AudioClip Clip;
    private float maxX = 5f;
    private float minX = -5f;
    private float maxY = 5f;
    private float minY = -5f;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool onFire;
    private float onFireDuration = 0f;
    private float onFireDPS;
    private float damage = 2;
    private bool frozen;
    private float frozenDuration;
    private float delay = 1.5f;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Pb.BarValue = 100;
        onFire = false;
        frozen = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bounds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        pausedmenumenu = GetComponent<PauseMenu>();
        maxX = bounds.maxX;
        maxY = bounds.maxY;
        minX = bounds.minX;
        minY = bounds.minY;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
        if (Pb.BarValue <= 0)
        {
            dead = true;
            GetComponent<AudioSource>().Stop();
            Destroy(this.gameObject);
        }

        //audio
        if (GetComponent<AudioSource>().isPlaying == false && !dead && !paused)
        {
        int index = Random.Range(0, normal.Length);
        Clip = normal[index];
        GetComponent<AudioSource>().clip = Clip;
        GetComponent<AudioSource>().PlayDelayed(delay);
        }   

            //effects
            if (onFire &&!paused)
        {
            damage = 8;
            Pb.BarValue -= onFireDPS;
            onFireDuration -= Time.deltaTime;
            if(onFireDuration <= 0)
            {
                onFire = false;
                fire.SetActive(false);
                onFireDuration = 0f;
                damage = 2;
            }
        }

       if (frozen &&!paused)
        {
            frozenDuration -= Time.deltaTime;
            if (frozenDuration <= 0)
            {
                frozen = false;
                ice.SetActive(false);
                frozenDuration = 0f;
            }
        }


        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
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
        movement = direction;
    }


    private void TakeDamage(float[] sentEnemy)
    {
        if (!paused)
        {
        Pb.BarValue -= sentEnemy[0];
        if (!dead)
        {
        int index = Random.Range(0, damageAudio.Length);
        Clip = damageAudio[index];
        GetComponent<AudioSource>().clip = Clip;
        GetComponent<AudioSource>().Play();
        }
        StartCoroutine(this.KnockBack(4f, sentEnemy[1], movement));
        if(sentEnemy[2] > 0f)
        {
            if (frozen)
            {
                frozen = false;
                ice.SetActive(false);
                frozenDuration = 0f;
            }
            onFireDuration += sentEnemy[2];
            onFireDPS = sentEnemy[3];
            fire.SetActive(true);
            onFire = true;
        }
        if (sentEnemy[4] > 0f)
        {
            if (onFire)
            {
                onFire = false;
                fire.SetActive(false);
                onFireDuration = 0f;
                damage = 2;
            }
            frozenDuration = sentEnemy[4];
            ice.SetActive(true);
            frozen = true;
        }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GetComponent<AudioSource>().isPlaying == false &&!dead &&!paused)
            {
                int index = Random.Range(0, attacking.Length);
                Clip = attacking[index];
                GetComponent<AudioSource>().clip = Clip;
                GetComponent<AudioSource>().Play();
            }
            collision.gameObject.SendMessage("TakeDamage", Time.deltaTime * damage);
        }
    }

    private void FixedUpdate()
    {
        if (!frozen)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            moveCharacter(movement);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public IEnumerator KnockBack(float knockDur, float knockbackFwr, Vector3 knockbackDir)
    {
        float timer = 0;
        while(knockDur > timer)
        {
            timer += Time.deltaTime;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector3(-knockbackDir.x * knockbackFwr, -knockbackDir.y * knockbackFwr, transform.position.z));
        }
        yield return 0;
    }
}
