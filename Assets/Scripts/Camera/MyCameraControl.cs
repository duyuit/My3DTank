using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCameraControl : MonoBehaviour
{
    Camera camera;
    public Transform player;
    Vector3 distance;
    CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        cameraShake = GetComponent<CameraShake>();
       // distance = transform.position - player.transform.position ;
    }
    public void SetPlayer(Transform newplayer)
    {
        transform.position = new Vector3(newplayer.position.x, transform.position.y, newplayer.position.z - 10);
        player = newplayer;
        distance = transform.position - player.position;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(player!=null)  
            transform.position = player.transform.position + distance;
      
        if (cameraShake.enabled)
            cameraShake.Shake();
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * 5, 4.7f, 13.27f);
        }
        //transform.rotation = Quaternion.Euler()
    }
}
