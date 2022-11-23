using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pocion : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator destroyPotion()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    IEnumerator colorWhite(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(.5f);
        sr.color = Color.white;
    }
    IEnumerator stopPotion()
    {
        yield return new WaitForSeconds(.7f);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    void Start()
    {
        StartCoroutine(destroyPotion());
        StartCoroutine(stopPotion());
        GetComponent<Rigidbody2D>().gravityScale = .1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    int cooldown = 100;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && cooldown >= 100)
        {
            cooldown = 0;
            col.GetComponent<PlayerController>().vida -= 1f;
            col.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(colorWhite(col.GetComponent<SpriteRenderer>()));
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        cooldown += 1;
        if (col.CompareTag("Player") && cooldown >= 100)
        {
            cooldown = 0;
            col.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(colorWhite(col.GetComponent<SpriteRenderer>()));
            col.GetComponent<PlayerController>().vida -= 1f;
        }
    }
}
