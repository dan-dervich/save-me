using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameWon : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator youWon() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(12);
    }
    void Start()
    {
        StartCoroutine(youWon());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
