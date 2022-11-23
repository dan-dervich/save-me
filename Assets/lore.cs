using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lore : MonoBehaviour
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
            yield return StartCoroutine(WaitForDoneProcess(imgWait[i - 1]));
            //if (Input.GetKey(KeyCode.Escape)) continue;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
