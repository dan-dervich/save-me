using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class youDied : MonoBehaviour
{
    public Sprite[] imgs;
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
    // Start is called before the first frame update
    IEnumerator changeScene()
    {
        yield return StartCoroutine(WaitForDoneProcess(4f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Start()
    {
        GetComponent<Image>().sprite = imgs[Random.Range(0, 10)];
        StartCoroutine(changeScene());
        // img = Resources.Load<Sprite>("Assets/Art/Tilesets/unknown" + Random.Range(0, 10) + ".png");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
