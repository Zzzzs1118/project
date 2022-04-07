using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    private GameObject[] light1;
    private float time1;
    bool change;
    int index;
   
    // Start is called before the first frame update
    void Start()
    {
        light1 = GameObject.FindGameObjectsWithTag("Light");
        change = false;
        time1 = 10.0f;
        ChangeLight();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (change)
        {
            ChangeLight();
            change = false;
            Debug.Log("1");
        }
        if (!change)
        {
            time1 -= Time.deltaTime;
        }
        if(time1 <= 0)
        {
            time1 = 10.0f;
            change = true;
        }
    }

    void ChangeLight()
    {
        for (int i = 0; i < light1.Length; i++)
        {
            light1[i].GetComponent<Light>().enabled = false;
        }
        index = Random.Range(0, light1.Length);
        light1[index].GetComponent<Light>().enabled = true;
    }
}
