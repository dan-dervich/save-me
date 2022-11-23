using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour
{
    float moveSpeed = 1.5f;
    public float vida = 20;
    bool burn = false;
    int cooldown = 0;
    GameObject PlayerFuego, PlayerHielo;
    public bool freeze = false;
    SpriteRenderer sr;
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
        PlayerFuego = GameObject.Find("PlayerFuego");
        PlayerHielo = GameObject.Find("PlayerHielo");
        sr = GetComponent<SpriteRenderer>();
    }
    public GameObject pocion;
    float fireBallForce = 80f;
    void PelotaFuego(GameObject direc)
    {
        GetComponent<Animator>().SetBool("attacking", true);
        Rigidbody2D bulletInstance = Instantiate(pocion, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))).GetComponent<Rigidbody2D>();
        Vector2 dir = direc.transform.position - transform.position;
        bulletInstance.AddForce(dir * fireBallForce);
    }
    int cooldownAttack = 0;
    void FixedUpdate()
    {
        if (vida <= 5)
        {
            Vector2 mt = Vector2.MoveTowards(transform.position, GameObject.Find("Portal").transform.position, moveSpeed * Time.deltaTime);
            transform.position = mt;
            return;
        }
        if (freeze) return;
        float distanceFuego = Vector3.Distance(transform.position, PlayerFuego.transform.position);
        float distanceHielo = Vector3.Distance(PlayerHielo.transform.position, transform.position);
        if (cooldownAttack > 60 && distanceFuego < 4)
        {
            cooldownAttack = 0;
            PelotaFuego(PlayerFuego);
            GetComponent<Animator>().SetBool("attacking", false);
        }
        else if (cooldownAttack > 120 && distanceHielo < 4)
        {
            cooldownAttack = 0;
            PelotaFuego(PlayerHielo);
            GetComponent<Animator>().SetBool("attacking", false);
        }
        cooldownAttack += 1;
        if (burn && cooldown == 50)
        {
            GameObject.Find("Portal").GetComponent<Portal>().bossDmg += .25f;
            vida -= .25f;
            PlayerFuego.GetComponent<PlayerController>().Dmg += .25f;
            sr.color = Color.red;
            StartCoroutine(whitecolor(sr));
            cooldown = 0;
            if (vida <= 5)
            {
                Vector2 mt = Vector2.MoveTowards(transform.position, GameObject.Find("Portal").transform.position, moveSpeed * Time.deltaTime);
                transform.position = mt;
                return;
            }
        }
        else if (burn)
        {
            cooldown += 1;
        }
    }
    void Update()
    {
        if (vida <= 5)
        {
            Vector2 mt = Vector2.MoveTowards(transform.position, GameObject.Find("Portal").transform.position, moveSpeed * Time.deltaTime);
            transform.position = mt;
            return;
        }
        // movement
        if (freeze) return;
        float distanceFuego = Vector3.Distance(transform.position, PlayerFuego.transform.position);
        float distanceHielo = Vector3.Distance(PlayerHielo.transform.position, transform.position);
        if (distanceFuego > distanceHielo)
        {
            // attack hielo
            Rigidbody2D PlayerHieloRB = PlayerHielo.GetComponent<Rigidbody2D>();
            float awayX = Vector3.Distance(new Vector2(PlayerHielo.transform.position.x + 2, PlayerHielo.transform.position.y), transform.position);
            float awayY = Vector3.Distance(new Vector2(PlayerHielo.transform.position.x, PlayerHielo.transform.position.y + 2), transform.position);
            if (awayX > awayY)
            {
                Vector2 mt = Vector2.MoveTowards(transform.position, new Vector2(PlayerHieloRB.position.x, PlayerHieloRB.position.y + 2), moveSpeed * Time.deltaTime);
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
            else
            {
                Vector2 mt = Vector2.MoveTowards(transform.position, new Vector2(PlayerHieloRB.position.x + 2, PlayerHieloRB.position.y), moveSpeed * Time.deltaTime);
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
        else if (distanceFuego < distanceHielo)
        {
            // attack fuego
            Rigidbody2D PlayerFuegoRB = PlayerFuego.GetComponent<Rigidbody2D>();
            float awayX = Vector3.Distance(new Vector2(PlayerFuego.transform.position.x + 2, PlayerFuego.transform.position.y), transform.position);
            float awayY = Vector3.Distance(new Vector2(PlayerFuego.transform.position.x, PlayerFuego.transform.position.y + 2), transform.position);
            if (awayX > awayY)
            {
                Vector2 mt = Vector2.MoveTowards(transform.position, new Vector2(PlayerFuegoRB.position.x, PlayerFuegoRB.position.y + 2), moveSpeed * Time.deltaTime);
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
            else
            {
                Vector2 mt = Vector2.MoveTowards(transform.position, new Vector2(PlayerFuegoRB.position.x + 2, PlayerFuegoRB.position.y), moveSpeed * Time.deltaTime);
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
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("pelotaFuego"))
        {
            burn = true;
            PlayerFuego.GetComponent<PlayerController>().Dmg += .75f;
            vida -= .75f;
            GameObject.Find("Portal").GetComponent<Portal>().bossDmg += .75f;
            sr.color = Color.red;
            StartCoroutine(whitecolor(sr));
            if (vida <= 5)
            {
                Vector2 mt = Vector2.MoveTowards(transform.position, GameObject.Find("Portal").transform.position, moveSpeed * Time.deltaTime);
                transform.position = mt;
                return;
            }
            StartCoroutine(stopBurn());
            return;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("ultiFuego"))
        {
            PlayerFuego.transform.GetComponent<PlayerController>().Dmg += 10;
            // send mage to the next dimension
            GameObject.Find("Portal").GetComponent<Portal>().bossDmg += 10;
            vida -= 10;
            return;
        }
    }
}