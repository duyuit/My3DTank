using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateShootControl : MonoBehaviour
{
    public GameObject laserBeam;
    public GameObject camera;
    void OnDisable()
    {
        camera.GetComponent<CameraShake>().enabled = false;
        foreach (var particle in GetComponentsInChildren<ParticleSystem>())
        {
            particle.Stop();
        }
        laserBeam.SetActive(false);
    }

    void OnEnable()
    {
        camera.GetComponent<CameraShake>().enabled = true;
        foreach (var particle in GetComponentsInChildren<ParticleSystem>())
        {
            particle.Play();
        }
        laserBeam.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
