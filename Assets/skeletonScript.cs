using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonScript : MonoBehaviour
{
    public bool freeze = false;
    public float vida = 5;
    RaycastHit2D rc;
    Rigidbody2D rb;
    private GameObject playerFuego;
    private Rigidbody2D playerFuegoRB;
    private Rigidbody2D playerHieloRB;
    private GameObject playerHielo;
    readonly float moveSpeed = 2.45f;
    readonly List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    ContactFilter2D movementFilter;
    Animator animator;
    public float collisionOffset = 0f;
    SpriteRenderer sr;
    int cooldown = 0;
    bool burn = false;
    PlayerController player;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        playerFuego = GameObject.Find("PlayerFuego");
        playerHielo = GameObject.Find("PlayerHielo");
        playerFuegoRB = playerFuego.GetComponent<Rigidbody2D>();
        playerHieloRB = playerHielo.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = playerFuego.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    IEnumerator whitecolor(SpriteRenderer spriterender)
    {
        yield return new WaitForSeconds(.5f);
        spriterender.color = Color.white;
    }
    IEnumerator redColor(SpriteRenderer spriterender)
    {
        yield return new WaitForSeconds(0.35f);
        spriterender.color = Color.red;
    }

    private void FixedUpdate()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        if (burn && cooldown == 50)
        {
            vida -= .25f;
            player.Dmg += .25f;
            sr.color = Color.red;
            StartCoroutine(whitecolor(GetComponent<SpriteRenderer>()));
            cooldown = 0;
            if (vida <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (burn)
        {
            cooldown += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (freeze) return;
        float distanceFuego = Vector3.Distance(transform.position, playerFuego.transform.position);
        float distanceHielo = Vector3.Distance(playerHielo.transform.position, transform.position);
        if (distanceFuego > distanceHielo)
        {
            Vector2 mt = Vector2.MoveTowards(transform.position, playerHieloRB.position, moveSpeed * Time.deltaTime);
            if (transform.position.x > mt.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
            transform.position = mt;
        }
        else if (distanceHielo > distanceFuego)
        {
            Vector2 mt = Vector2.MoveTowards(transform.position, playerFuegoRB.position, moveSpeed * Time.deltaTime);
            if (transform.position.x > mt.x)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
            transform.position = mt;
        }
    }
    IEnumerator stopBurn()
    {
        yield return new WaitForSeconds(2f);
        burn = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("pelotaFuego"))
        {
            burn = true;
            player.Dmg += .75f;
            vida -= .75f;
            sr.color = Color.red;
            StartCoroutine(whitecolor(sr));
            if (vida <= 0)
            {
                Destroy(gameObject);
                return;
            }
            StartCoroutine(stopBurn());
            return;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("ultiFuego"))
        {
            player.Dmg += vida;
            Destroy(gameObject);
            return;
        }
        if (collision.transform.CompareTag("pelotaFuego"))
        {
            burn = true;
            player.Dmg += .25f;
            vida -= .25f;
            sr.color = Color.red;
            StartCoroutine(whitecolor(GetComponent<SpriteRenderer>()));
            if (vida <= 0)
            {
                Destroy(gameObject);
                return;
            }
            StartCoroutine(stopBurn());
            return;
        }
        if (freeze) return;
        animator.SetBool("attacking", true);
        if (collision.transform.GetComponent<PlayerController>().coolPlayer)
        {
            playerFuego.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor(playerFuego.GetComponent<SpriteRenderer>()));
            playerFuego.GetComponent<PlayerController>().vida -= .5f;
        }
        else if (!collision.transform.GetComponent<PlayerController>().coolPlayer)
        {
            playerHielo.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor(playerHielo.GetComponent<SpriteRenderer>()));
            playerHielo.GetComponent<PlayerController>().vida -= .5f;
        }
        i = 0;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("pelotaFuego"))
        {
            return;
        }
        i++;
        if (freeze) return;
        if (i == 60)
        {
            animator.SetBool("attacking", true);
            if (collision.gameObject == playerFuego)
            {
                playerFuego.GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(whitecolor(playerFuego.GetComponent<SpriteRenderer>()));
                playerFuego.GetComponent<PlayerController>().vida -= .5f;
            }
            else if(collision.gameObject == playerHielo)
            {
                playerHielo.GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(whitecolor(playerHielo.GetComponent<SpriteRenderer>()));
                playerHielo.GetComponent<PlayerController>().vida -= .5f;
            }
            i = 0;
        }
        else
        {
            animator.SetBool("attacking", false);
        }
    }
}
