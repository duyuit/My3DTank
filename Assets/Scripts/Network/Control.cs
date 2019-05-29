using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Control : NetworkBehaviour
{
    Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
           // transform.position = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal1"), 0, Input.GetAxis("Vertical1")) * 10;
        }
    }

}
