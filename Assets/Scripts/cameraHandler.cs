using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandler : MonoBehaviour
{
    private GameObject playerFuego;
    private GameObject playerHielo;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        playerFuego = GameObject.Find("PlayerFuego");
        playerHielo = GameObject.Find("PlayerHielo");
        camera = GetComponent<Camera>();
        camera.enabled = true;
        camera.orthographic = true;
        camera.orthographicSize = 2f;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerHielo.transform.position, playerFuego.transform.position);
        if (distance >= 4 && distance / 2f >= 2 && distance / 2f < 10f)
        {
                camera.orthographicSize = distance / 2f;
        }
        camera.transform.position = new Vector3((playerFuego.transform.position.x + playerHielo.transform.position.x) / 2, (playerFuego.transform.position.y + playerHielo.transform.position.y) / 2, -10);
    }
}
