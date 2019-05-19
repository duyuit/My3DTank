﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("FTOn", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("FTOn", false);
        }
      
    }
}
