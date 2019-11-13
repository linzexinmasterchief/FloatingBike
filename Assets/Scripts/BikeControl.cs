using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeControl : MonoBehaviour
{
    // front left floating point
    [SerializeField] GameObject flfp;
    // front right floating point
    [SerializeField] GameObject frfp;
    // back floating point
    [SerializeField] GameObject bfp;

    private float main_engine_output;
    private float flfp_engine_output;
    private float frfp_engine_output;
    private float bfp_engine_output;

    public float preset_flight_height;
    private float delta_height;
    public float max_speed;
    private float ship_z_rotation;

    // Start is called before the first frame update
    void Start()
    {
        delta_height = 0;
        main_engine_output = 0;
        flfp_engine_output = 0;
        frfp_engine_output = 0;
        bfp_engine_output = 0;
    }

    // Update is called once per frame
    void Update()
    {
        main_engine_output = (60 - gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        if (main_engine_output < 0)
        {
            main_engine_output = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * main_engine_output);
        }
        if (Input.GetKey(KeyCode.S))
        {
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            preset_flight_height += 0.1f;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            preset_flight_height -= 0.1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(8f * transform.right, transform.position + 2 * transform.forward - 0.1f * transform.up);
            //gameObject.GetComponent<Rigidbody>().AddForce(0.05f * transform.right, ForceMode.Impulse);

        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-8f * transform.right, transform.position + 2 * transform.forward - 0.1f * transform.up);
            //gameObject.GetComponent<Rigidbody>().AddForce(-0.05f * transform.right, ForceMode.Impulse);

        }
    }

    private void FixedUpdate()
    {
        delta_height = preset_flight_height - transform.position.y;
        // gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 9.81f + 20 * delta_height, 0), transform.position + transform.up * 1f - transform.forward * 2f);

        // front left engine
        flfp_engine_output = 4f + 5f * (preset_flight_height - flfp.transform.position.y + 0.8f);
        if (flfp_engine_output < 0)
        {
            flfp_engine_output = 0;
        }
        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-flfp.transform.right * flfp_engine_output, flfp.transform.position);

        // front right engine
        frfp_engine_output = 4f + 5f * (preset_flight_height - frfp.transform.position.y + 0.8f);
        if (frfp_engine_output < 0)
        {
            frfp_engine_output = 0;
        }
        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-frfp.transform.right * frfp_engine_output, frfp.transform.position);
        Debug.Log(frfp.transform.position);
        // back engine
        bfp_engine_output = 9.8f + 5 * (preset_flight_height - bfp.transform.position.y - 2f);
        if (bfp_engine_output < 0)
        {
            bfp_engine_output = 0;
        }
        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 10, 0) * bfp_engine_output, bfp.transform.position);
    }
}
