using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class hpVidaFuego : MonoBehaviour
{
    public bool PlayerFuego;
    public GameObject playerController;
    GameObject playerFuego;
    GameObject playerHielo;
    // Start is called before the first frame update
    void Start()
    {
        playerFuego = GameObject.Find("PlayerFuego");
        playerHielo = GameObject.Find("PlayerHielo");
        if (PlayerFuego)
        {
            GetComponent<Slider>().maxValue = 15;
            GetComponent<Slider>().minValue = 0;
            return;
        }
        GetComponent<Slider>().maxValue = 600;
        GetComponent<Slider>().minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerFuego)
            GetComponent<Slider>().value = playerFuego.GetComponent<PlayerController>().Dmg;
        else
            GetComponent<Slider>().value = Convert.ToSingle(playerHielo.GetComponent<PlayerController>().CoolDownHielo);
    }
}
