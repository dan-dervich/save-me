using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossFinal : MonoBehaviour
{
    int cooldown = 0;
    public float vida = 60;
    int attackCooldown = 0;
    SpriteRenderer sr;
    Animator animator;
    public LayerMask enemyLayers;
    GameObject PlayerFuego, PlayerHielo;
    float moveSpeed = .25f;
    bool burn = false;
    public bool freeze = false;
    public GameObject maskLore, LifeMenu, pauseMenu, youWonMenu;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        PlayerFuego = GameObject.Find("PlayerFuego");
        PlayerHielo = GameObject.Find("PlayerHielo");
    }
    bool unmasked = true;
    private void FixedUpdate()
    {
        print(vida);
        if (!unmasked)
        {
            // unmasked
            animator.SetBool("unmasked", true);
        }
        if (vida <= 20 && unmasked)
        {
            // start lore and take mask off
            maskLore.SetActive(true);
            LifeMenu.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 0;
            unmasked = false;
        }
        if (vida <= .5f)
        {
            maskLore.SetActive(false);
            LifeMenu.SetActive(false);
            pauseMenu.SetActive(false);
            youWonMenu.SetActive(true);
            Time.timeScale = 0;
        }
        attackCooldown += 1;
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
    IEnumerator whiteColor(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;
    }
    private void attack()
    {
        animator.SetBool("attacking", true);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, .25f, enemyLayers);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.transform.CompareTag("Player"))
            {
                enemy.GetComponent<PlayerController>().vida -= 3;
                enemy.GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(whiteColor(enemy.GetComponent<SpriteRenderer>()));
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("attacking", false);
        if (vida <= 0)
        {
            // send to YOU WON scene
        }
        if (vida <= 10)
        {
            // take mask off
        }
        if (freeze) return;
        float distanceH = Vector2.Distance(PlayerHielo.transform.position, transform.position);
        float distanceF = Vector2.Distance(PlayerFuego.transform.position, transform.position);
        if (attackCooldown >= 120)
        {
            if (distanceF <= .25f)
            {
                attackCooldown = 0;
                attack();
            }
            else if (distanceH <= .25f)
            {
                attackCooldown = 0;
                attack();
            }
        }
        if (distanceF > distanceH)
        {
            Vector2 mt = Vector2.MoveTowards(transform.position, PlayerHielo.transform.position, moveSpeed * Time.deltaTime);
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
        else if (distanceH > distanceF)
        {
            Vector2 mt = Vector2.MoveTowards(transform.position, PlayerFuego.transform.position, moveSpeed * Time.deltaTime);
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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("ultiFuego"))
        {
            PlayerFuego.transform.GetComponent<PlayerController>().Dmg += 10;
            vida -= 10;
            return;
        }
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
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("ultiFuego"))
        {
            PlayerFuego.transform.GetComponent<PlayerController>().Dmg += 10;
            vida -= 10;
            return;
        }
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
