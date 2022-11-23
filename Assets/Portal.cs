using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject dmgFinalBoss;
    public int kills = 0;
    public float bossDmg = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator dmgFinal()
    {
        yield return new WaitForSeconds(2.5f);
        dmgFinalBoss.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("mago") && bossDmg >= 15)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (col.transform.CompareTag("Player") && bossDmg >= 15)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (bossDmg < 20 && col.transform.CompareTag("Player"))
        {
            dmgFinalBoss.SetActive(true);
            StartCoroutine(dmgFinal());
        }
    }
}
