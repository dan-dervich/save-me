using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelotaFuego : MonoBehaviour
{
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") || collision.CompareTag("ghost") || collision.CompareTag("pocion"))
        {
            return;
        }
        StartCoroutine(redColor(collision.GetComponent<SpriteRenderer>()));
        StartCoroutine(whiteColor(collision.GetComponent<SpriteRenderer>()));
        destroy();
    }
}
