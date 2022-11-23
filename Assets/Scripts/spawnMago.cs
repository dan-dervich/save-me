using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMago : MonoBehaviour
{
    public GameObject Mago;
    GameObject spawner;
    void Start()
    {
        spawner = GameObject.Find("Spawner");
    }
    bool magoSpawned = false;
    public int finalRound = 5;
    void Update()
    {
        if (spawner.GetComponent<spawner>().roundCounter == finalRound && !magoSpawned)
        {
            // spawn mago
            Instantiate(Mago, transform.position, transform.rotation);
            magoSpawned = true;
        }
    }
}
