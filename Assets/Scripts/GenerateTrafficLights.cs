using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrafficLights : MonoBehaviour
{
    public GameObject[] lights;
    public float radius = 10;

    // Start is called before the first frame update
    void Awake()
    {
        lights = new GameObject[10];

        InstantiateTrafficLights();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateTrafficLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            lights[i].transform.SetParent(this.transform);
            lights[i].tag = "TrafficLight";
        }

        for (int i = 0; i < lights.Length; i++)
        {
            float theta = i * 2 * Mathf.PI / lights.Length;
            float x = Mathf.Sin(theta) * radius;
            float z = Mathf.Cos(theta) * radius;

            Vector3 pos = new Vector3(x, 0, z);

            lights[i].transform.position = pos;
        }
    }
}
