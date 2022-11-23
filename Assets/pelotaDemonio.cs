using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelotaDemonio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroy", 3f);
    }
    void destroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    IEnumerator whiteColor(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(.5f);
        sr.color = Color.white;
    }
    IEnumerator redColor(SpriteRenderer sr)
    {
        yield return new WaitForSeconds(.3f);
        sr.color = Color.red;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("ghost") || collision.transform.CompareTag("pocion") || collision.CompareTag("demonio"))
        {
            return;
        }
        else if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerController>().vida -= 1.5f;
            StartCoroutine(redColor(collision.GetComponent<SpriteRenderer>()));
            StartCoroutine(whiteColor(collision.GetComponent<SpriteRenderer>()));
            destroy();
            return;
        }
        destroy();
    }
}
