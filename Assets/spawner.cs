using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] zombieGO;
    // Start is called before the first frame update
    void Start()
    {
    }
    GameObject spawnZombie(Vector2 pos, GameObject GO)
    {
        return Instantiate(GO, pos, transform.rotation);
    }
    // Update is called once per frame
    public int roundCounter = 0;
    bool nextRound = true;
    GameObject[] spawned;
    int cooldown = 0;
    void FixedUpdate()
    {
        if (nextRound)
        {
            cooldown += 120;
        }
        if (nextRound && cooldown > 40)
        {
            cooldown = 0;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerController>().vida = 5;
            }
            int rand = Random.Range((roundCounter > 0 ? 6 : 1), (roundCounter > 6 ? roundCounter : 6));
            spawned = new GameObject[rand];
            for (int j = 0; j < rand; j++)
            {
                spawned[j] = spawnZombie(new Vector2(Random.Range(transform.position.x - 6, transform.position.x + 6), Random.Range(transform.position.y - 6, transform.position.y + 6)), zombieGO[Random.Range(0, zombieGO.Length)]);
            }
            roundCounter++;
            nextRound = false;
        }
        else if (roundCounter < 5)
        {
            bool empty = true;
            for (int i = 0; i < spawned.Length; i++)
            {
                print(spawned[i]);
                if (spawned[i] != null) empty = false;
            }
            if (empty) nextRound = true;
        }
    }
}
