using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ultiFuego : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerFuego;
    PlayerController playerCont;
    GameObject[] player;
    void Start()
    {
        Invoke("destroy", 2.5f);
        playerFuego = GameObject.Find("PlayerFuego");
        playerCont = playerFuego.GetComponent<PlayerController>();
        player = GameObject.FindGameObjectsWithTag("Player");
    }
    void destroy()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerCont.Dmg += 1;
    }
}
