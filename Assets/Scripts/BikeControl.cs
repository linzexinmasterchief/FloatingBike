using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeControl : MonoBehaviour
{

    public float preset_flight_height;
    private float delta_height;
    public float max_speed;

    // Start is called before the first frame update
    void Start()
    {
        preset_flight_height = transform.position.y;
        delta_height = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * (60 - gameObject.GetComponent<Rigidbody>().velocity.magnitude));
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position + new Vector3(0, 0, 1), new Vector3(0, 1, 0), 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position + new Vector3(0, 0, 1), new Vector3(0, 1, 0), -1);
        }
    }
}
