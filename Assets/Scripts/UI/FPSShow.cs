using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSShow : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        int fps =(int)( 1 / Time.deltaTime);
        text.text = fps.ToString();
    }
}
