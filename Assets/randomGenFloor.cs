using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomGenFloor : MonoBehaviour
{
    public GameObject lavaPit;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Random.Range(6, 20); i++)
        {
            Instantiate(lavaPit, new Vector2(Random.Range(-20, 20), Random.Range(-20, 20)), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
