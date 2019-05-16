using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkShield : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float shieldLight = 0;
    float delta = 0.05f;
    // Update is called once per frame
    void Update()
    {
        material.SetFloat("_Glossiness", shieldLight);
        if (shieldLight > 1)
            delta = -0.05f;
        if (shieldLight < 0)
            delta = 0.05f;
        shieldLight += delta;
    }
}
