using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSShow : MonoBehaviour
{
    Text text;
    float lastUpdate = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastUpdate >1)
        {
            int fps = (int)(1 / Time.deltaTime);
            text.text = fps.ToString();
            lastUpdate = Time.time;
        }

    }
}
