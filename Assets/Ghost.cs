using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float vida = 3;
    bool burn = false;
    public bool freeze = false;
    public SpriteRenderer sr;
    GameObject playerHielo, playerFuego;
    float moveSpeed = 1.5f;
    // Start is called before the first frame update
    IEnumerator stopBurn()
    {
        yield return new WaitForSeconds(2f);
        burn = false;
    }
    IEnumerator whitecolor(SpriteRenderer spriterender)
    {
        yield return new WaitForSeconds(.3f);
        spriterender.color = Color.white;
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerHielo = GameObject.Find("PlayerHielo");
        playerFuego = GameObject.Find("PlayerFuego");
    }
    int cooldown = 0;
    private void FixedUpdate()
    {
        i++;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        if (burn && cooldown == 50)
        {
            vida -= .25f;
            playerFuego.GetComponent<PlayerController>().Dmg += .25f;
            sr.color = Color.red;
            StartCoroutine(whitecolor(sr));
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
        Rigidbody2D playerHieloRB = playerHielo.GetComponent<Rigidbody2D>();
        Rigidbody2D playerFuegoRB = playerFuego.GetComponent<Rigidbody2D>();
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
    int i = 0;
    private void OnTriggerEnter2D(Collider2D col)
    {


        if (col.transform.CompareTag("ultiFuego"))
        {
            playerFuego.transform.GetComponent<PlayerController>().Dmg += vida;
            Destroy(gameObject);
            return;
        }
        if (col.transform.CompareTag("pelotaFuego"))
        {
            burn = true;
            playerFuego.GetComponent<PlayerController>().Dmg += .75f;
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
        if (freeze) return;
        if (col.transform.CompareTag("Player") && i >= 60)
        {
            GetComponent<Animator>().SetBool("attacking", true);
            col.transform.GetComponent<PlayerController>().vida -= 1;
            col.transform.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor(col.transform.GetComponent<SpriteRenderer>()));
            i = 0;
        }
        else
        {
            GetComponent<Animator>().SetBool("attacking", false);

        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        i++;
        if (freeze) return;
        if (col.transform.CompareTag("Player") && i >= 60)
        {
            GetComponent<Animator>().SetBool("attacking", true);
            col.transform.GetComponent<PlayerController>().vida -= 1;
            col.transform.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whitecolor(col.transform.GetComponent<SpriteRenderer>()));
            i = 0;
        }
        else
        {
            GetComponent<Animator>().SetBool("attacking", false);
        }
    }
}
