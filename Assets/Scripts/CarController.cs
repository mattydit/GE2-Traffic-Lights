using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] lights;
    public List<GameObject> greens;
    public GameObject target;
    public Vector3 force = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float maxSpeed = 2.0f;
    public float slowingDistance = 3.0f;
    public float mass = 1;
    [Range(0.0f, 10.0f)]
    public float damping = 0.01f;

    [Range(0.0f, 1.0f)]
    public float banking = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        lights = new GameObject[10];
        lights = GameObject.FindGameObjectsWithTag("TrafficLight");
        greens = new List<GameObject>();

        for (int i = 0; i < lights.Length; i++)
        {
            Color c = lights[i].GetComponent<Renderer>().material.color;

            if (c == Color.green)
            {
                greens.Add(lights[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (target == null)
        {
            target = greens[Random.Range(0, greens.Count)];
        }

        force = Calculate();
        Vector3 newAcceleration = force / mass;
        acceleration = Vector3.Lerp(acceleration, newAcceleration, Time.deltaTime);
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        if (velocity.magnitude > float.Epsilon)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);

            transform.position += velocity * Time.deltaTime;
            velocity *= (1.0f - (damping * Time.deltaTime));
        }

    }

    public Vector3 ArriveForce(Vector3 target, float slowingDistance = 15.0f)
    {
        Vector3 toTarget = target - transform.position;

        float distance = toTarget.magnitude;
        if (distance < 0.1f)
        {
            return Vector3.zero;
        }
        float ramped = maxSpeed * (distance / slowingDistance);

        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / distance);

        return desired - velocity;
    }

    public Vector3 Calculate()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 force = ArriveForce(targetPos, slowingDistance);
        return force;
    }
}
