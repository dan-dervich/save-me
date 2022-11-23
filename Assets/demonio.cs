using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demonio : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    Rigidbody2D rb;
    GameObject PlayerFuego, PlayerHielo;
    public bool freeze;
    // Start is called before the first frame update
    float moveSpeed = .5f;
    float fireBallForce = 100;
    public GameObject pelota;
    int cooldownAttack = 0;
    public float vida = 5;
    bool burn = false;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlayerHielo = GameObject.Find("PlayerHielo");
        PlayerFuego = GameObject.Find("PlayerFuego");
    }
    int cooldown = 0;
    private void FixedUpdate()
    {
        if (vida <= 0) Destroy(gameObject);
        cooldownAttack += 1;
        if (burn && cooldown == 50)
        {
            vida -= .25f;
            PlayerFuego.GetComponent<PlayerController>().Dmg += .25f;
            sr.color = Color.red;
            StartCoroutine(whiteColor(sr));
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
    private void pelotaFuego(GameObject direc)
    {
        GetComponent<Animator>().SetBool("attacking", true);
        Rigidbody2D bulletInstance = Instantiate(pelota, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))).GetComponent<Rigidbody2D>();
        Vector2 dir = direc.transform.position - transform.position;
        var speed = dir * fireBallForce;
        print(speed);
        bulletInstance.AddForce(speed);
    }
    // Update is called once per frame
    void Update()
    {
        if (freeze) return;
        // attack
        float distanceH = Vector2.Distance(PlayerHielo.transform.position, transform.position);
        float distanceF = Vector2.Distance(PlayerFuego.transform.position, transform.position);
        if (distanceF < distanceH && distanceF < 7)
        {
            if (cooldownAttack >= 60)
            {
                if (distanceF < distanceH)
                {
                    pelotaFuego(PlayerFuego);
                    cooldownAttack = 0;
                }
                else if (distanceF > distanceH)
                {
                    pelotaFuego(PlayerHielo);
                    cooldownAttack = 0;
                }
            }
        }
        else if (distanceF > distanceH && distanceH < 7)
        {
            if (cooldownAttack >= 60)
            {
                if (distanceF < distanceH)
                {
                    pelotaFuego(PlayerFuego);
                    cooldownAttack = 0;
                }
                else if (distanceF > distanceH)
                {
                    pelotaFuego(PlayerHielo);
                    cooldownAttack = 0;
                }
            }
        }
        if (distanceF < distanceH && distanceF < 7)
        {
            // attack fuego
            float distanceX = Vector2.Distance(new Vector2(PlayerFuego.transform.position.x + 1.5f, PlayerFuego.transform.position.y), transform.position);
            float distanceY = Vector2.Distance(new Vector2(PlayerFuego.transform.position.x, PlayerFuego.transform.position.y + 1.5f), transform.position);
            float distanceYN = Vector2.Distance(new Vector2(PlayerFuego.transform.position.x, PlayerFuego.transform.position.y - 1.5f), transform.position);
            float distanceXN = Vector2.Distance(new Vector2(PlayerFuego.transform.position.x - 1.5f, PlayerFuego.transform.position.y), transform.position);
            float[] distances = { distanceX, distanceY, distanceXN, distanceYN };
            float minDistance = 1000;
            foreach (float distance in distances)
            {
                if (minDistance > distance)
                {
                    minDistance = distance;
                }
            }
            if (minDistance == distanceX)
            {
                Vector2 mtX = Vector2.MoveTowards(transform.position, new Vector2(PlayerFuego.transform.position.x + 1.5f, PlayerFuego.transform.position.y), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtX.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtX;
            }
            else if (minDistance == distanceY)
            {
                Vector2 mtY = Vector2.MoveTowards(transform.position, new Vector2(PlayerFuego.transform.position.x, PlayerFuego.transform.position.y + 1.5f), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtY.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtY;
            }
            else if (minDistance == distanceXN)
            {
                Vector2 mtX = Vector2.MoveTowards(transform.position, new Vector2(PlayerFuego.transform.position.x - 1.5f, PlayerFuego.transform.position.y), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtX.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtX;
            }
            else if (minDistance == distanceYN)
            {
                Vector2 mtY = Vector2.MoveTowards(transform.position, new Vector2(PlayerFuego.transform.position.x, PlayerFuego.transform.position.y - 1.5f), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtY.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtY;
            }
        }
        else if (distanceF > distanceH && distanceH < 7)
        {
            // attack hielo
            float distanceX = Vector2.Distance(new Vector2(PlayerHielo.transform.position.x + 1.5f, PlayerHielo.transform.position.y), transform.position);
            float distanceY = Vector2.Distance(new Vector2(PlayerHielo.transform.position.x, PlayerHielo.transform.position.y + 1.5f), transform.position);
            float distanceYN = Vector2.Distance(new Vector2(PlayerHielo.transform.position.x, PlayerHielo.transform.position.y - 1.5f), transform.position);
            float distanceXN = Vector2.Distance(new Vector2(PlayerHielo.transform.position.x - 1.5f, PlayerHielo.transform.position.y), transform.position);
            float[] distances = { distanceX, distanceY, distanceXN, distanceYN };
            float minDistance = 1000;
            foreach (float distance in distances)
            {
                if (minDistance > distance)
                {
                    minDistance = distance;
                }
            }
            if (minDistance == distanceX)
            {
                Vector2 mtX = Vector2.MoveTowards(transform.position, new Vector2(PlayerHielo.transform.position.x + 1.5f, PlayerHielo.transform.position.y), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtX.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtX;
            }
            else if (minDistance == distanceY)
            {
                Vector2 mtY = Vector2.MoveTowards(transform.position, new Vector2(PlayerHielo.transform.position.x, PlayerHielo.transform.position.y + 1.5f), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtY.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtY;
            }
            else if (minDistance == distanceXN)
            {
                Vector2 mtX = Vector2.MoveTowards(transform.position, new Vector2(PlayerHielo.transform.position.x - 1.5f, PlayerHielo.transform.position.y), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtX.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtX;
            }
            else if (minDistance == distanceYN)
            {
                Vector2 mtY = Vector2.MoveTowards(transform.position, new Vector2(PlayerHielo.transform.position.x, PlayerHielo.transform.position.y - 1.5f), moveSpeed * Time.fixedDeltaTime);
                if (transform.position.x > mtY.x)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
                transform.position = mtY;
            }
        }
    }
    IEnumerator whiteColor(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;
    }
    IEnumerator stopBurn()
    {
        yield return new WaitForSeconds(2f);
        burn = false;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("ultiFuego"))
        {
            PlayerFuego.transform.GetComponent<PlayerController>().Dmg += vida;
            Destroy(gameObject);
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("pelotaFuego"))
        {
            burn = true;
            PlayerFuego.GetComponent<PlayerController>().Dmg += .75f;
            vida -= .75f;
            sr.color = Color.red;
            StartCoroutine(whiteColor(sr));
            if (vida <= 0)
            {
                Destroy(gameObject);
                return;
            }
            StartCoroutine(stopBurn());
            return;
        }
        if (col.transform.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("attacking", true);
            col.transform.GetComponent<PlayerController>().vida -= 1;
            col.transform.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(whiteColor(col.transform.GetComponent<SpriteRenderer>()));
        }
        else
        {
            GetComponent<Animator>().SetBool("attacking", false);

        }
    }
}
