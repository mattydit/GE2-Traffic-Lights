using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        StartCoroutine(LightTimings());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LightTimings()
    {
        while (true)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                Renderer r = lights[i].GetComponent<Renderer>();

                if (r.material.color == Color.yellow)
                {
                    StartCoroutine(YellowTime(lights[i]));
                }
                else if (r.material.color == Color.red)
                {
                    StartCoroutine(RedTime(lights[i]));
                }
                else if (r.material.color == Color.green)
                {
                    StartCoroutine(GreenTime(lights[i]));
                }
            }

            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator YellowTime(GameObject light)
    {
        Renderer r = light.GetComponent<Renderer>();

        yield return new WaitForSeconds(4);

        r.material.color = Color.red;
        
    }

    IEnumerator RedTime(GameObject light)
    {
        Renderer r = light.GetComponent<Renderer>();
        //Red lights stay red for a random time between 5 and 10
        int time = Random.Range(5, 11);

        yield return new WaitForSeconds(time);

        r.material.color = Color.green;
        Debug.Log("Red light changed after: " + (time) + "seconds");
    }

    IEnumerator GreenTime(GameObject light)
    {
        Renderer r = light.GetComponent<Renderer>();

        int time = Random.Range(5, 11);

        yield return new WaitForSeconds(time);

        r.material.color = Color.yellow;
        Debug.Log("green light changed after: " + time + "seconds");
        
        
    }
}
