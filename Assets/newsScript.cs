using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newsScript : MonoBehaviour
{
    public GameObject canvas;
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        canvas.SetActive(true);
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(changeScene());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
