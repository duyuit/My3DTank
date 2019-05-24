using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float magnitute = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Shake()
    {
        float x = Random.Range(-1.0f, 1.0f) * magnitute;
        float y = Random.Range(-1.0f, 1.0f) * magnitute;
        transform.position = transform.position + new Vector3(x, y, 0);
    }
}
