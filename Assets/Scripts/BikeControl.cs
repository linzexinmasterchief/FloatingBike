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
    // main engine flare
    //[SerializeField] GameObject main_engine_flare;
    //[SerializeField] GameObject flfp_engine_flare;
    //[SerializeField] GameObject frfp_engine_flare;

    private float main_engine_output;
    private float flfp_engine_output;
    private float frfp_engine_output;
    private float bfp_engine_output;

    public float preset_flight_height;
    private float delta_height;
    public float main_engine_max_output;
    private float turning_force;

    // Start is called before the first frame update
    void Start()
    {
        delta_height = 0;
        main_engine_output = 0;
        flfp_engine_output = 0;
        frfp_engine_output = 0;
        bfp_engine_output = 0;
        // + for right turn, - for left
        turning_force = 0;
    }

    // Update is called once per frame
    void Update()
    {
        main_engine_output = (main_engine_max_output - gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        if (main_engine_output < 0)
        {
            main_engine_output = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * main_engine_output);
            
        }
        Debug.Log(1.5f - main_engine_output / 60);
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

        // rotate force (no engine)
        turning_force = 0;
        if (Input.GetKey(KeyCode.D))
        {
            //gameObject.GetComponent<Rigidbody>().AddForceAtPosition(8f * transform.right, transform.position + 2 * transform.forward - 0.2f * transform.up);
            turning_force += 10f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-8f * transform.right, transform.position + 2 * transform.forward - 0.2f * transform.up);
            turning_force -= 10f;
        }
        
        // adjust engine flare effect by resizing them
        //main_engine_flare.transform.localScale = new Vector3(1, 1.5f - main_engine_output / main_engine_max_output, 1);
        //flfp_engine_flare.transform.localScale = new Vector3(1, 0.8f + flfp_engine_output / 15, 1);
        //frfp_engine_flare.transform.localScale = new Vector3(1, 0.8f + frfp_engine_output / 15, 1);
    }

    private void FixedUpdate()
    {
        delta_height = preset_flight_height - transform.position.y;
        // front left engine
        flfp_engine_output = 14f + 5f * (preset_flight_height - flfp.transform.position.y) + turning_force;
        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-flfp.transform.right * flfp_engine_output, flfp.transform.position);

        // front right engine
        frfp_engine_output = 14f + 5f * (preset_flight_height - frfp.transform.position.y) - turning_force;
        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-frfp.transform.right * frfp_engine_output, frfp.transform.position);
        
        // back engine
        bfp_engine_output = 10f + 2.5f * (preset_flight_height - bfp.transform.position.y);

        gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, 10, 0) * bfp_engine_output, bfp.transform.position);
    }
}
