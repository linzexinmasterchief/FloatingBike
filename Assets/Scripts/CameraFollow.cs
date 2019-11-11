using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject target;
    private Vector3 delta_pos;
    // Start is called before the first frame update
    void Start()
    {
        delta_pos = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position - delta_pos;
    }
}
