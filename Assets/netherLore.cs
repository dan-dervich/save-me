using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class netherLore : MonoBehaviour
{
    public Sprite[] imgs;
    public float[] imgWait = {
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
        3f,
    };
    bool called;
    IEnumerator WaitForDoneProcess(float timeout)
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Escape)) break;
            timeout -= Time.deltaTime;
            if (timeout <= 0f) break;
        }
    }
    IEnumerator changeImage()
    {
        int i = 0;
        foreach (Sprite img in imgs)
        {
            GetComponent<Image>().sprite = img;
            i++;
            GameObject.Find("Main Camera").GetComponent<pause>().lore = true;
            yield return StartCoroutine(WaitForDoneProcess(imgWait[i - 1]));
            //if (Input.GetKey(KeyCode.Escape)) continue;
        }
        Time.timeScale = 1;
        GameObject.Find("Main Camera").GetComponent<pause>().lore = false;
        GameObject.FindGameObjectWithTag("bossFinal").GetComponent<bossFinal>().LifeMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("bossFinal").GetComponent<bossFinal>().pauseMenu.SetActive(false);
        GameObject.FindGameObjectWithTag("bossFinal").GetComponent<bossFinal>().youWonMenu.SetActive(false);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (!called)
        {
            StartCoroutine(changeImage());
            called = true;
        }
    }
}
