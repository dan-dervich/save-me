using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool coolPlayer;
    public float speed = 2.5f;
    Rigidbody2D rb;
    Vector2 movement;
    ContactFilter2D movementFilter;
    public float collisionOffset = 0f;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    SpriteRenderer sr;
    public float vida = 5f;
    Animator animator;
    public float Dmg = 0;
    public float DmgHielo = 0;
    Collider2D[] HitEnemies;
    Transform firepoint;
    public GameObject pelota;
    public GameObject ulti;
    float fireBallForce = 1.5f;
    public GameObject HieloAttackRadiusObject;
    public Image[] imgs;
    public Sprite halfHeart;
    Vector3 mousePos;
    Camera cam;
    private void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Cursor.visible = true;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        firepoint = GameObject.Find("firePoint").GetComponent<Transform>();
        HieloAttackRadiusObject.SetActive(false);
        vida = 5;
    }
    IEnumerator stopAttack()
    {
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("isAttacking", false);
        HieloAttackRadiusObject.SetActive(false);
    }
    public LayerMask enemyLayers;
    IEnumerator damageZombie(Collider2D enemy)
    {
        yield return new WaitForSeconds(1f);
        if (enemy.GetComponent<Zombie>().vida > 0) DmgHielo += enemy.GetComponent<Zombie>().vida;
        enemy.GetComponent<Zombie>().vida -= 3;
        DmgHielo += 3;
    }
    IEnumerator damageMago(Collider2D enemy)
    {
        yield return new WaitForSeconds(1f);
        if (enemy.GetComponent<Mago>().vida > 0) DmgHielo += enemy.GetComponent<Mago>().vida;
        enemy.GetComponent<Mago>().vida -= 3;
        DmgHielo += 3;
    }
    IEnumerator damageGhost(Collider2D enemy)
    {
        yield return new WaitForSeconds(1f);
        if (enemy.GetComponent<Ghost>().vida > 0) DmgHielo += enemy.GetComponent<Ghost>().vida;
        enemy.GetComponent<Ghost>().vida -= 3;
        DmgHielo += 3;
    }
    IEnumerator damageEsqueleto(Collider2D enemy)
    {
        yield return new WaitForSeconds(1f);
        if (enemy.GetComponent<skeletonScript>().vida > 0) DmgHielo += enemy.GetComponent<skeletonScript>().vida;
        enemy.GetComponent<skeletonScript>().vida -= 3;
        DmgHielo += 3;
    }
    IEnumerator damageDemon(Collider2D enemy)
    {
        yield return new WaitForSeconds(1f);
        if (enemy.GetComponent<demonio>().vida > 0) DmgHielo += enemy.GetComponent<skeletonScript>().vida;
        enemy.GetComponent<skeletonScript>().vida -= 3;
        DmgHielo += 3;
    }
    IEnumerator ZombieRed(Collider2D enemy)
    {
        yield return new WaitForSeconds(.3f);
        enemy.GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(whitecolor(enemy.GetComponent<SpriteRenderer>()));
    }
    IEnumerator whitecolor(SpriteRenderer spriterender)
    {
        yield return new WaitForSeconds(.3f);
        spriterender.color = Color.white;
    }
    IEnumerator StopUlti(Collider2D[] frozenEnemies)
    {
        yield return new WaitForSeconds(7f);
        foreach (Collider2D enemy in frozenEnemies)
        {
            if (enemy.transform.CompareTag("zombie"))
            {
                enemy.GetComponent<Zombie>().freeze = false;
                enemy.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (enemy.transform.CompareTag("mago"))
            {
                enemy.GetComponent<Mago>().freeze = false;
                enemy.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (enemy.transform.CompareTag("ghost"))
            {
                enemy.GetComponent<Ghost>().freeze = false;
                enemy.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (enemy.transform.CompareTag("skeleton"))
            {
                enemy.GetComponent<skeletonScript>().freeze = false;
                enemy.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (enemy.transform.CompareTag("demonio"))
            {
                enemy.GetComponent<demonio>().freeze = false;
                enemy.GetComponent<SpriteRenderer>().color = Color.white;
            }
            if (enemy.transform.CompareTag("bossFinal"))
            {
                enemy.GetComponent<bossFinal>().freeze = false;
                enemy.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    float prevVida;
    void UltiHielo()
    {
        // make a freeze radius
        Collider2D[] frozenEnemies;
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkingBack", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("ulti", true);
        prevVida = vida;
        HieloAttackRadiusObject.SetActive(true);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(HieloAttackRadiusObject.transform.position, 10f, enemyLayers);
        frozenEnemies = enemies;
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.transform.CompareTag("zombie"))
            {
                enemy.GetComponent<Zombie>().freeze = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(.78f, 1f, 1f, .8f);
            }
            if (enemy.transform.CompareTag("mago"))
            {
                enemy.GetComponent<Mago>().freeze = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(.78f, 1f, 1f, .8f);
            }
            if (enemy.transform.CompareTag("ghost"))
            {
                enemy.GetComponent<Ghost>().freeze = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(.78f, 1f, 1f, .8f);
            }
            if (enemy.transform.CompareTag("skeleton"))
            {
                enemy.GetComponent<skeletonScript>().freeze = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(.78f, 1f, 1f, .8f);
            }
            if (enemy.transform.CompareTag("demonio"))
            {
                enemy.GetComponent<demonio>().freeze = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(.78f, 1f, 1f, .8f);
            }
            if (enemy.transform.CompareTag("bossFinal"))
            {
                enemy.GetComponent<bossFinal>().freeze = true;
                enemy.GetComponent<SpriteRenderer>().color = new Color(.78f, 1f, 1f, .8f);
            }
        }
        StartCoroutine(StopUlti(frozenEnemies));
        HieloAttackRadiusObject.SetActive(false);
    }
    void AtaqueHielo()
    {
        prevVida = vida;
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkingBack", false);
        animator.SetBool("isAttacking", true);
        HieloAttackRadiusObject.SetActive(true);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(HieloAttackRadiusObject.transform.position, .6f, enemyLayers);
        HitEnemies = enemies;
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.transform.CompareTag("zombie"))
            {
                StartCoroutine(damageZombie(enemy));
                StartCoroutine(ZombieRed(enemy));
            }
            if (enemy.transform.CompareTag("mago"))
            {
                StartCoroutine(damageMago(enemy));
                StartCoroutine(ZombieRed(enemy));
            }
            if (enemy.transform.CompareTag("ghost"))
            {
                StartCoroutine(damageGhost(enemy));
                StartCoroutine(ZombieRed(enemy));
            }
            if (enemy.transform.CompareTag("skeleton"))
            {
                StartCoroutine(damageEsqueleto(enemy));
                StartCoroutine(ZombieRed(enemy));
            }
            if (enemy.transform.CompareTag("demonio"))
            {
                StartCoroutine(damageDemon(enemy));
                StartCoroutine(ZombieRed(enemy));
            }
        }
        StartCoroutine(stopAttack());
    }
    void ultimateAttack()
    {
        GameObject bulletInstance = Instantiate(ulti, firepoint.position, firepoint.rotation);
        Vector3 relative = firepoint.transform.InverseTransformPoint(mousePos);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        bulletInstance.transform.Rotate(0, 0, -angle + 90);
        Dmg = 0;
    }
    IEnumerator UltimateHielo()
    {
        yield return new WaitForSeconds(.8f);
        UltiHielo();
    }
    void PelotaFuego()
    {
        Rigidbody2D bulletInstance = Instantiate(pelota, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))).GetComponent<Rigidbody2D>();
        mousePos = mousePos - transform.position;
        Vector2 direction = new Vector2(mousePos.x * fireBallForce, mousePos.y * fireBallForce);
        bulletInstance.velocity = direction;
    }
    public int cooldownFuego = 0;
    int cooldownHielo = 0;
    bool up = false;
    public int CoolDownHielo;
    int CoolDownHieloRegen;
    private void FixedUpdate()
    {
        cooldownFuego += 1;
        cooldownHielo += 1;
        CoolDownHielo += 1;
        CoolDownHieloRegen += 1;
        for (int k = 0; k < 5; k++)
        {
            imgs[k].rectTransform.sizeDelta = new Vector2(25, 25);
        }
        if (vida == 4.5f)
        {
            imgs[4].sprite = halfHeart;
            imgs[4].rectTransform.sizeDelta = new Vector2(12, 25);
        }
        else if (vida == 4f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
        }
        else if (vida == 3.5f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
            imgs[3].sprite = halfHeart;
            imgs[3].rectTransform.sizeDelta = new Vector2(12, 25);
        }
        else if (vida == 3f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
            imgs[3].sprite = null;
            imgs[3].color = new Color(0, 0, 0, 0);
        }
        else if (vida == 2.5f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
            imgs[3].sprite = null;
            imgs[3].color = new Color(0, 0, 0, 0);
            imgs[2].sprite = halfHeart;
            imgs[2].rectTransform.sizeDelta = new Vector2(12, 25);
        }
        else if (vida == 2f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
            imgs[3].sprite = null;
            imgs[3].color = new Color(0, 0, 0, 0);
            imgs[2].sprite = null;
            imgs[2].color = new Color(0, 0, 0, 0);
        }
        else if (vida == 1.5f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
            imgs[3].sprite = null;
            imgs[3].color = new Color(0, 0, 0, 0);
            imgs[2].sprite = null;
            imgs[2].color = new Color(0, 0, 0, 0);
            imgs[1].sprite = halfHeart;
            imgs[1].rectTransform.sizeDelta = new Vector2(12, 25);
        }
        else if (vida == 1f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
            imgs[3].sprite = null;
            imgs[3].color = new Color(0, 0, 0, 0);
            imgs[2].sprite = null;
            imgs[2].color = new Color(0, 0, 0, 0);
            imgs[1].sprite = null;
            imgs[1].color = new Color(0, 0, 0, 0);
        }
        else if (vida == .5f)
        {
            imgs[4].sprite = null;
            imgs[4].color = new Color(0, 0, 0, 0);
            imgs[3].sprite = null;
            imgs[3].color = new Color(0, 0, 0, 0);
            imgs[2].sprite = null;
            imgs[2].color = new Color(0, 0, 0, 0);
            imgs[0].sprite = halfHeart;
            imgs[1].sprite = null;
            imgs[1].color = new Color(0, 0, 0, 0);
            imgs[0].rectTransform.sizeDelta = new Vector2(12, 25);
        }
    }
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 0;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        if (vida <= 0)
        {
            SceneManager.LoadScene(11);
        }
        // player fuego
        if (coolPlayer)
        {
            if (Input.GetMouseButton(0) && cooldownFuego >= 30)
            {
                PelotaFuego();
                cooldownFuego = 0;
            }
            if (Input.GetMouseButton(1) && Dmg >= 15)
            {
                ultimateAttack();
            }
            movement = new Vector2(Input.GetAxis("Keys Horizontal"), Input.GetAxis("Keys Vertical"));
            int move = rb.Cast(movement, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
            if (move == 0)
            {
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            }
            else
            {
                movement = new Vector2(Input.GetAxis("Keys Horizontal"), 0);
                move = rb.Cast(movement, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
                if (move == 0)
                {
                    rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                }
                else
                {
                    movement = new Vector2(0, Input.GetAxis("Keys Vertical"));
                    move = rb.Cast(movement, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
                    if (move == 0)
                    {
                        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                    }
                }
            }
            if (movement.y > 0)
            {
                up = true;
                animator.SetBool("isWalkingBack", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("idleUp", true);
            }
            else if (movement.x != 0 && movement.y < 0)
            {
                up = false;
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("idleUp", false);
                animator.SetBool("isWalking", true);
            }
            else if (movement.x > 0)
            {
                up = false;
                animator.SetBool("isWalking", true);
                animator.SetBool("idleUp", false);
                animator.SetBool("isWalkingBack", false);
                sr.flipX = false;
            }
            else if (movement.x < 0)
            {
                up = false;
                animator.SetBool("isWalking", true);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("idleUp", false);
                sr.flipX = true;
            }
            else if (up)
            {
                up = false;
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("idleUp", true);
            }
        }
        if (!coolPlayer && HieloAttackRadiusObject.active)
        {
            sr.color = Color.white;
            animator.SetBool("ulti", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isWalkingBack", false);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(HieloAttackRadiusObject.transform.position, .6f, enemyLayers);
            if (HitEnemies != null)
            {
                for (int i = 0; i < HitEnemies.Length; i++)
                {
                    for (int j = 0; j < enemies.Length; j++)
                    {
                        if (HitEnemies[i] != null) if (enemies[j].Equals(HitEnemies[i])) enemies[j] = null;
                    }
                }
            }
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    if (enemy.transform.CompareTag("zombie"))
                    {
                        StartCoroutine(damageZombie(enemy));
                        StartCoroutine(ZombieRed(enemy));
                    }
                    if (enemy.transform.CompareTag("mago"))
                    {
                        StartCoroutine(damageMago(enemy));
                        StartCoroutine(ZombieRed(enemy));
                    }
                    if (enemy.transform.CompareTag("ghost"))
                    {
                        StartCoroutine(damageGhost(enemy));
                        StartCoroutine(ZombieRed(enemy));
                    }
                    if (enemy.transform.CompareTag("skeleton"))
                    {
                        StartCoroutine(damageEsqueleto(enemy));
                        StartCoroutine(ZombieRed(enemy));
                    }
                    if (enemy.transform.CompareTag("demonio"))
                    {
                        StartCoroutine(damageDemon(enemy));
                        StartCoroutine(ZombieRed(enemy));
                    }
                }
            }
            vida = prevVida;
        }
        else if (!coolPlayer)
        {
            if (CoolDownHieloRegen >= 600)
            {
                vida += 1;
                CoolDownHieloRegen = 0;
            }
            if (Input.GetKey(KeyCode.K) && cooldownHielo >= 60)
            {
                cooldownHielo = 0;
                AtaqueHielo();
            }
            if (Input.GetKey(KeyCode.L) && CoolDownHielo >= 600)
            {
                CoolDownHielo = 0;
                StartCoroutine(UltimateHielo());
            }
            else
            {
                animator.SetBool("ulti", false);
            }
            movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            int move = rb.Cast(movement, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
            if (move == 0)
            {
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            }
            else
            {
                movement = new Vector2(Input.GetAxis("Horizontal"), 0);
                move = rb.Cast(movement, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
                if (move == 0)
                {
                    rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                }
                else
                {
                    movement = new Vector2(0, Input.GetAxis("Vertical"));
                    move = rb.Cast(movement, movementFilter, castCollisions, speed * Time.fixedDeltaTime + collisionOffset);
                    if (move == 0)
                    {
                        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
                    }
                }
            }
            if (movement.y > 0)
            {
                animator.SetBool("isWalkingBack", true);
                animator.SetBool("isWalking", false);
            }
            else if (movement.x != 0 || movement.y < 0)
            {
                animator.SetBool("isWalkingBack", false);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isWalkingBack", false);
            }
            if (movement.x > 0)
            {
                sr.flipX = false;
            }
            else if (movement.x < 0)
            {
                sr.flipX = true;
            }
        }


    }
}
