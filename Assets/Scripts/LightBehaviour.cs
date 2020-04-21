using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    public GameObject[] lights;
    public Color[] colours;

    // Start is called before the first frame update
    void Start()
    {
        lights = new GameObject[10];
        colours = new Color[3];

        lights = GameObject.FindGameObjectsWithTag("TrafficLight");
        colours[0] = Color.green;
        colours[1] = Color.yellow;
        colours[2] = Color.red;

        for (int i = 0; i < lights.Length; i++)
        {
            //Set the colour of the lights randomly on start.
            lights[i].GetComponent<Renderer>().material.color = colours[Random.Range(0,3)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
