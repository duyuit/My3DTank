using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraControl : MonoBehaviour
{

    Camera camera;
    public GameObject player;
    Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        distance = transform.position - player.transform.position ;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + distance;
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * 5, 4.7f, 10f);
        }
        //transform.rotation = Quaternion.Euler()
    }
}
