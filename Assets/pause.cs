using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public GameObject pauseMenu;
    public GameObject LifeMenu;
    // Update is called once per frame
    int cooldown = 0;
    bool paused = false;
    public bool lore = false;
    void Update()
    {
        cooldown += 1;
        if (Input.GetKey(KeyCode.Escape) && !lore)
        {
            if (paused && cooldown > 120)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                LifeMenu.SetActive(true);
                cooldown = 0;
                paused = !paused;
            }
            else if (!paused && cooldown > 120)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                LifeMenu.SetActive(false);
                paused = !paused;
                cooldown = 0;
            }
            if (Time.timeScale == 0)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                LifeMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                LifeMenu.SetActive(true);
            }
        }
    }
    public void ResumeGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        players[0].GetComponent<PlayerController>().cooldownFuego = 0;
        players[1].GetComponent<PlayerController>().cooldownFuego = 0;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        LifeMenu.SetActive(true);
        cooldown = 0;
        paused = !paused;
    }
}
